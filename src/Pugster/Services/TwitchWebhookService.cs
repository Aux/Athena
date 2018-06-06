using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Pugster
{
    public class TwitchWebhookService
    {
        private readonly IConfiguration _config;
        private readonly RestClient _rest;

        private CancellationTokenSource _cancelTokenSource;

        public TwitchWebhookService(IConfiguration config)
        {
            _config = config;
            _rest = new RestClient("https://api.twitch.tv/helix/");
            _cancelTokenSource = new CancellationTokenSource();
        }

        public async Task SubscribeStreamStatusAsync(ulong userId)
        {
            var hub = new TwitchHub
            {
                CallbackUrl = Path.Combine(_config["website:url"], $"webhooks/twitch/streamstatus/{userId}"),
                Topic = $"https://api.twitch.tv/helix/streams?user_id={userId}"
            };

            string json = JsonConvert.SerializeObject(hub);

            var response = await _rest.SendAsync("POST", "webhooks/hub", json, _cancelTokenSource.Token, false);
        }
    }
}
