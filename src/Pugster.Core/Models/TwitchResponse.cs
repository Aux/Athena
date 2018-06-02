using Newtonsoft.Json;
using System.Collections.Generic;

namespace Pugster
{
    public class TwitchResponse<TData>
    {
        [JsonProperty("data")]
        public IEnumerable<TData> Data { get; set; }
    }
}
