using Newtonsoft.Json;
using System;

namespace Pugster
{
    public enum TwitchWebhookMode
    {
        Subscribe,
        Unsubscribe
    }

    public class TwitchHub
    {
        [JsonProperty("hub.callback")]
        public string CallbackUrl { get; set; }
        [JsonProperty("hub.topic")]
        public string Topic { get; set; }
        [JsonProperty("hub.mode")]
        public TwitchWebhookMode Mode { get; set; } = TwitchWebhookMode.Subscribe;
        [JsonProperty("hub.lease_seconds")]
        public int LeaseSeconds { get; set; } = 864000; // 10 days
        [JsonProperty("hub.secret")]
        public string Secret { get; set; } = new Guid().ToString();
    }
}
