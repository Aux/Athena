using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Pugster.Web
{
    [Route("webhooks/twitch")]
    public class TwitchWebhooksController : Controller
    {
        private readonly TwitchHubService _hub;

        public TwitchWebhooksController(TwitchHubService hub)
        {
            _hub = hub;
        }
        
        [HttpPost("streamstatus/{userId}")]
        public async Task<IActionResult> PostStreamStatusAsync(ulong userId, [FromBody]TwitchResponse<TwitchStream> content)
        {
            if (!_hub.IsConnected)
                await _hub.StartAsync();
            
            await _hub.SendStreamStatusAsync(userId, content?.Data.FirstOrDefault());
            return Ok();
        }

        [HttpPost("follower/{userId}")]
        public async Task<IActionResult> PostFollowerAsync(ulong userId, [FromBody]TwitchResponse<TwitchFollow> content)
        {
            if (!_hub.IsConnected)
                await _hub.StartAsync();

            await _hub.SendFollowerAsync(userId, content.Data.FirstOrDefault());
            return Ok();
        }
    }
}
