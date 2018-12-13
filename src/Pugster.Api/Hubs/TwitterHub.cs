using System.Threading.Tasks;

namespace Pugster.Api.Hubs
{
    public class TwitterHub : HubBase
    {
        public async Task RelayTweetAsync(object data)
        {
            await Clients.All.SendCoreAsync("tweet", new[] { data });
        }
    }
}
