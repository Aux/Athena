using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Pugster.Dashboard.Hubs
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

        public virtual async Task SystemMessage(string message)
        {
            await Clients.Caller.SendCoreAsync("system", new[] { message });
        }

        public virtual async Task SubscribeEvent(string eventId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, eventId.ToLower());
            await SystemMessage($"Subscribed to event: {eventId}");
        }

        public virtual async Task UnsubscribeEvent(string eventId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, eventId.ToLower());
            await SystemMessage($"Unsubscribed from event: {eventId}");
        }
    }
}
