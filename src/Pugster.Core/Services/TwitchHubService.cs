using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Pugster
{
    public class TwitchHubService
    {
        public bool IsConnected = false;

        private readonly IConfiguration _config;
        private readonly BaseLoggingService _logger;
        private readonly HubConnection _hub;

        public TwitchHubService(IConfiguration config, BaseLoggingService logger)
        {
            _config = config;
            _logger = logger;

            _hub = new HubConnectionBuilder()
                .WithUrl(Path.Combine(_config["website:url"], "hubs/twitch"))
                .Build();
        }

        public Task ListenAsync()
        {
            _hub.On<ulong, TwitchStream>("stream_status", OnStreamStatus);
            _hub.On<ulong, TwitchFollow>("follower", OnFollower);
            _hub.On<object>("subscriber", OnSubscriber);

            return StartAsync();
        }

        public async Task StartAsync()
        {
            try
            {
                await _hub.StartAsync();
                IsConnected = true;
                await _logger.LogAsync("Info", "TwitchHubService", "Connected");
            }
            catch (Exception ex)
            {
                await _logger.LogAsync("Error", "TwitchHubService", ex.ToString());
                await StopAsync();
            }
        }

        public async Task StopAsync()
        {
            await _hub.StopAsync();
            IsConnected = false;
        }

        public Task SendStreamStatusAsync(ulong userId, TwitchStream stream)
            => _hub.InvokeAsync("SendStreamStatusAsync", userId, stream);

        private void OnStreamStatus(ulong userId, TwitchStream stream)
            => OnStreamStatusAsync(userId, stream).GetAwaiter().GetResult();
        private async Task OnStreamStatusAsync(ulong userId, TwitchStream stream)
        {
            await _logger.LogAsync("Verbose", "TwitchHubService", $"Received stream status for user `{userId}`");
        }

        public Task SendFollowerAsync(ulong userId, TwitchFollow follow)
            => _hub.InvokeAsync("SendFollowerAsync", userId, follow);

        private void OnFollower(ulong userId, TwitchFollow follow)
            => OnFollowerAsync(userId, follow).GetAwaiter().GetResult();
        private async Task OnFollowerAsync(ulong userId, TwitchFollow twitchFollow)
        {
            await _logger.LogAsync("Verbose", "TwitchHubService", $"Received follower for user `{userId}`");
        }

        public Task SendSubscriberAsync(object obj)
            => _hub.InvokeAsync("SendSubscriberAsync", obj);

        private void OnSubscriber(object obj)
            => OnSubscriberAsync(obj).GetAwaiter().GetResult();
        private async Task OnSubscriberAsync(object obj)
        {
            await _logger.LogAsync("Verbose", "TwitchHubService", $"Received subscriber for user `{obj}`");
        }
    }
}
