using System.Threading.Tasks;

namespace Pugster.Dashboard.Hubs
{
    public class TwitchHub : HubBase
    {
        public async Task RelayStreamStatusAsync(ulong userId, object data)
        {
            await Clients.All.SendCoreAsync("stream_status", new[] { userId, data });
        }

        public async Task RelayChatModeAsync(object data)
        {
            await Clients.All.SendCoreAsync("chat_mode", new[] { data });
        }

        public async Task RelayMessageReceivedAsync(object data)
        {
            await Clients.All.SendCoreAsync("message_received", new[] { data });
        }

        public async Task RelayMessageDeletedAsync(object data)
        {
            await Clients.All.SendCoreAsync("message_deleted", new[] { data });
        }

        public async Task RelayBitsAsync(ulong userId, object data)
        {
            await Clients.All.SendCoreAsync("bits", new[] { userId, data });
        }

        public async Task RelayFollowerAsync(ulong userId, object data)
        {
            await Clients.All.SendCoreAsync("follower", new[] { userId, data });
        }

        public async Task RelaySubscriberAsync(ulong userId, object data)
        {
            await Clients.All.SendCoreAsync("subscriber", new[] { userId, data });
        }
    }
}
