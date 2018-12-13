using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Pugster.Api.Hubs
{
    public abstract class HubBase : Hub
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
    }
}
