using Microsoft.Extensions.Configuration;
using Pugster.Bridge.Discord;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Wumpus.Bot;

namespace Pugster.Bridge
{
    public class DiscordManager
    {
        private readonly IConfiguration _config;
        private readonly WumpusBotClient _bot;

        private readonly MessageEventHandler _messages;
        private readonly GuildEventHandler _guilds;

        public DiscordManager(IConfiguration config)
        {
            _config = config;

            var token = _config["discord:token"];
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentNullException("Discord token is missing in the configuration file");

            _bot = new WumpusBotClient();
            _bot.Authorization = new AuthenticationHeaderValue("Authorization", "Bot " + _config["discord:token"]);

            _messages = new MessageEventHandler(_bot.Gateway, _config["discord:hub_url"]);
            _guilds = new GuildEventHandler(_bot.Gateway, _config["discord:hub_url"]);
        }
        
        public async Task RunAsync()
        {
            await _bot.RunAsync();
        }

        public async Task StopAsync()
        {
            await _bot.StopAsync();
        }
    }
}
