using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Pugster.Web
{
    [Route("api/webhooks/twitch")]
    public class TwitchWebhooksController : Controller
    {
        private readonly TwitchHubService _hub;

        public TwitchWebhooksController(TwitchHubService hub)
        {
            _hub = hub;
        }
        
        [HttpPost("{webhookId}")]
        public async Task<IActionResult> PostStreamStatusAsync(ulong webhookId, ulong userId, [FromBody]TwitchResponse<TwitchStream> content)
        {
            if (!_hub.IsConnected)
                await _hub.StartAsync();

            await _hub.SendStreamStatusAsync(userId, content?.Data.FirstOrDefault());
            return Ok();
        }
    }
}
