using System.Threading.Tasks;

namespace Pugster.Dashboard.Hubs
{
    public class TwitterHub : HubBase
    {
        public async Task RelayTweetAsync(object data)
        {
            await Clients.All.SendCoreAsync("tweet", new[] { data });
        }
    }
}
