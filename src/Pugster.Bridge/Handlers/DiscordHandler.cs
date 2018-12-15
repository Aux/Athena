using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Voltaic.Serialization.Json;
using Wumpus;
using Wumpus.Bot;
using Wumpus.Events;

namespace Pugster.Bridge
{
    public class DiscordHandler
    {
        private readonly IConfiguration _config;
        private readonly WumpusBotClient _bot;
        private readonly HubConnection _hub;
        private readonly JsonSerializer _serializer;

        public DiscordHandler(IConfiguration config)
        {
            _config = config;
            _serializer = new JsonSerializer();

            _hub = new HubConnectionBuilder()
                .WithUrl(Path.Combine(_config["url"], _config["discord:hub_url"]))
                .Build();

            var token = _config["discord:token"];
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentNullException("Discord token is missing in the configuration file");

            _bot = new WumpusBotClient();
            _bot.Authorization = new AuthenticationHeaderValue("Authorization", "Bot " + _config["discord:token"]);

            _bot.Gateway.Connected += OnConnected;
            _bot.Gateway.Disconnected += OnDisconnected;
            _bot.Gateway.ReceivedPayload += OnPayloadReceived;
        }

        private void OnPayloadReceived(GatewayPayload payload, PayloadInfo info)
        {
            Console.WriteLine($"Received ${payload.DispatchType}");
            string methodName;

            switch (payload.DispatchType)
            {
                case GatewayDispatchType.MessageCreate:
                    methodName = "RelayMessageCreated";
                    break;
                case GatewayDispatchType.MessageUpdate:
                    methodName = "RelayMessageUpdated";
                    break;
                case GatewayDispatchType.MessageDelete:
                    methodName = "RelayMessageDeleted";
                    break;
                case GatewayDispatchType.MessageDeleteBulk:
                    methodName = "RelayMessageDeletedBulk";
                    break;
                case GatewayDispatchType.MessageReactionAdd:
                    methodName = "RelayReactionAdded";
                    break;
                case GatewayDispatchType.MessageReactionRemove:
                    methodName = "RelayReactionRemoved";
                    break;
                case GatewayDispatchType.MessageReactionRemoveAll:
                    methodName = "RelayReactionRemovedBulk";
                    break;
                case GatewayDispatchType.GuildMemberAdd:
                    methodName = "RelayUserJoined";
                    break;
                case GatewayDispatchType.GuildMemberRemove:
                    methodName = "RelayUserLeft";
                    break;
                default:
                    return;
            }

            var json = _serializer.WriteUtf8String(payload.Data);
            _hub.InvokeAsync(methodName, json).GetAwaiter().GetResult();
        }

        public async Task RunAsync()
        {
            await _hub.StartAsync();
            await _bot.RunAsync();
        }

        public async Task StopAsync()
        {
            await _bot.StopAsync();
        }

        private void OnConnected()
        {
            Console.WriteLine("[Discord] Gateway connected");
        }

        private void OnDisconnected(Exception ex)
        {
            Console.WriteLine($"[Discord] Gateway disconnected with exception: {ex}");
        }
    }
}
