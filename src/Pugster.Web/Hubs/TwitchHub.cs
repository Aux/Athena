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
        
        public async Task SendStreamStatusAsync(ulong userId, TwitchStream twitchStream)
        {
            await Clients.All.SendAsync("stream_status", userId, twitchStream);
        }

        public async Task SendFollowerAsync(ulong userId, TwitchFollow twitchFollow)
        {
            await Clients.All.SendAsync("follower", userId, twitchFollow);
        }

        public async Task SendSubscriberAsync(object obj)
        {
            await Clients.All.SendAsync("subscriber", obj);
        }
    }
}
