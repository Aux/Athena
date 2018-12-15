using System.Threading.Tasks;
using Voltaic;

namespace Pugster.Dashboard.Hubs
{
    public class DiscordHub : HubBase
    {
        public async Task RelayMessageCreated(Utf8String json)
        {
            await Clients.Group(Constants.MessageCreated).SendCoreAsync(Constants.MessageCreated, new[] { json });
        }

        public async Task RelayMessageDeleted(Utf8String json)
        {
            await Clients.Group(Constants.MessageDeleted).SendCoreAsync(Constants.MessageDeleted, new[] { json });
        }

        public async Task RelayReactionAdded(Utf8String json)
        {
            await Clients.Group(Constants.ReactionAdded).SendCoreAsync(Constants.ReactionAdded, new[] { json });
        }

        public async Task RelayReactionRemoved(Utf8String json)
        {
            await Clients.Group(Constants.ReactionRemoved).SendCoreAsync(Constants.ReactionRemoved, new[] { json });
        }

        public async Task RelayUserJoined(Utf8String json)
        {
            await Clients.Group(Constants.UserJoined).SendCoreAsync(Constants.UserJoined, new[] { json });
        }

        public async Task RelayUserLeft(Utf8String json)
        {
            await Clients.Group(Constants.UserLeft).SendCoreAsync(Constants.UserLeft, new[] { json });
        }
    }
}
