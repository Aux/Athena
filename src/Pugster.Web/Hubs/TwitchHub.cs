using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Pugster.Web
{
    public class TwitchHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "connections");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "connections");
            await base.OnDisconnectedAsync(exception);
        }
        
        public async Task SendStreamStatusAsync(ulong channelId, TwitchStream twitchStream)
        {
            await Clients.All.SendAsync("stream_status", new object[] { channelId, twitchStream });
        }

        public async Task SendSubscriberAsync(object obj)
        {
            await Clients.All.SendAsync("subscriber", new[] { obj });
        }

        public async Task SendFollowerAsync(object obj)
        {
            await Clients.All.SendAsync("follower", new[] { obj });
        }
    }
}
