using System.Threading.Tasks;
using Voltaic;

namespace Pugster.Dashboard.Hubs
{
    public class DiscordHub : HubBase
    {
        public async Task RelayMessageCreated(string json)
        {
            await Clients.Group(Constants.MessageCreated).SendCoreAsync(Constants.MessageCreated, new[] { json });
        }

        public async Task RelayMessageDeleted(string json)
        {
            await Clients.Group(Constants.MessageDeleted).SendCoreAsync(Constants.MessageDeleted, new[] { json });
        }

        public async Task RelayReactionAdded(string json)
        {
            await Clients.Group(Constants.ReactionAdded).SendCoreAsync(Constants.ReactionAdded, new[] { json });
        }

        public async Task RelayReactionRemoved(string json)
        {
            await Clients.Group(Constants.ReactionRemoved).SendCoreAsync(Constants.ReactionRemoved, new[] { json });
        }

        public async Task RelayUserJoined(string json)
        {
            await Clients.Group(Constants.UserJoined).SendCoreAsync(Constants.UserJoined, new[] { json });
        }

        public async Task RelayUserLeft(string json)
        {
            await Clients.Group(Constants.UserLeft).SendCoreAsync(Constants.UserLeft, new[] { json });
        }

        public async Task RelayPresenceUpdate(string json)
        {
            await Clients.Group(Constants.PresenceUpdate).SendCoreAsync(Constants.PresenceUpdate, new[] { json });
        }
    }
}
