using System.Threading.Tasks;

namespace Pugster.Dashboard.Hubs
{
    public class DiscordHub : HubBase
    {
        public async Task RelayMessageCreated(string user, string message)
        {
            await Clients.Group(Constants.MessageCreated).SendCoreAsync(Constants.MessageCreated, new[] { user, message });
        }

        public async Task RelayMessageDeleted(object data)
        {
            await Clients.Group(Constants.MessageDeleted).SendCoreAsync(Constants.MessageDeleted, new[] { data });
        }

        public async Task RelayReactionAdded(object data)
        {
            await Clients.Group(Constants.ReactionAdded).SendCoreAsync(Constants.ReactionAdded, new[] { data });
        }

        public async Task RelayReactionRemoved(object data)
        {
            await Clients.Group(Constants.ReactionRemoved).SendCoreAsync(Constants.ReactionRemoved, new[] { data });
        }

        public async Task RelayUserJoined(object data)
        {
            await Clients.Group(Constants.UserJoined).SendCoreAsync(Constants.UserJoined, new[] { data });
        }

        public async Task RelayUserLeft(object data)
        {
            await Clients.Group(Constants.UserLeft).SendCoreAsync(Constants.UserLeft, new[] { data });
        }
    }
}
