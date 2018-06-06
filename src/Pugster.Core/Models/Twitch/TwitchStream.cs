using Newtonsoft.Json;
using System;

namespace Pugster
{
    public class TwitchStream
    {
        [JsonProperty("id")]
        public ulong Id { get; set; }
        [JsonProperty("user_id")]
        public ulong UserId { get; set; }
        [JsonProperty("game_id")]
        public ulong GameId { get; set; }
        [JsonProperty("community_ids")]
        public string[] CommunityIds { get; set; }
        //[JsonProperty("type")]                        // json property "type" interferes with signalr models :)
        //public string StreamType { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("viewer_count")]
        public int ViewerCount { get; set; }
        [JsonProperty("started_at")]
        public DateTime StartedAt { get; set; }
        [JsonProperty("language")]
        public string Language { get; set; }
        [JsonProperty("thumbnail_url")]
        public string ThumbnailUrl { get; set; }
    }
}
