using System.Threading.Tasks;

namespace Pugster.Api.Hubs
{
    public class DiscordHub : HubBase
    {
        public async Task RelayMessageReceivedAsync(object data)
        {
            await Clients.All.SendCoreAsync("message_received", new[] { data });
        }

        public async Task RelayMessageDeletedAsync(object data)
        {
            await Clients.All.SendCoreAsync("message_deleted", new[] { data });
        }

        public async Task RelayReactionAddedAsync(object data)
        {
            await Clients.All.SendCoreAsync("reaction_added", new[] { data });
        }

        public async Task RelayReactionRemovedAsync(object data)
        {
            await Clients.All.SendCoreAsync("reaction_removed", new[] { data });
        }

        public async Task RelayUserJoinedAsync(object data)
        {
            await Clients.All.SendCoreAsync("user_joined", new[] { data });
        }

        public async Task RelayUserLeftAsync(object data)
        {
            await Clients.All.SendCoreAsync("user_left", new[] { data });
        }
    }
}
