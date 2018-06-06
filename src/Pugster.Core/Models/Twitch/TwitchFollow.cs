using Newtonsoft.Json;
using System;

namespace Pugster
{
    public class TwitchFollow
    {
        [JsonProperty("from_id")]
        public ulong FollowingUserId { get; set; }
        [JsonProperty("to_id")]
        public ulong FollowedUserId { get; set; }
        [JsonProperty("followed_at")]
        public DateTime FollowedAt { get; set; }
    }
}
