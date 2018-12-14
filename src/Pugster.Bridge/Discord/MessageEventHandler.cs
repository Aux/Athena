using Microsoft.AspNetCore.SignalR.Client;
using Wumpus;
using Wumpus.Entities;
using Wumpus.Events;

namespace Pugster.Bridge.Discord
{
    public class MessageEventHandler : BaseEventHandler
    {
        private readonly WumpusGatewayClient _gateway;

        public MessageEventHandler(WumpusGatewayClient gateway, string hubUrl) : base(hubUrl)
        {
            _gateway = gateway;

            _gateway.MessageCreate += OnMessageCreate;
            _gateway.MessageUpdate += OnMessageUpdate;
            _gateway.MessageDelete += OnMessageDelete;
            _gateway.MessageDeleteBulk += OnMessageDeleteBulk;

            _gateway.MessageReactionAdd += OnReactionAdded;
            _gateway.MessageReactionRemove += OnReactionRemoved;
            _gateway.MessageReactionRemoveAll += OnReactionRemovedAll;
        }

        private void OnMessageCreate(Message obj)
        {
            _hub.InvokeAsync("RelayMessageCreated", obj).GetAwaiter().GetResult();
        }

        private void OnMessageUpdate(Message obj)
        {
            _hub.InvokeAsync("RelayMessageUpdated", obj).GetAwaiter().GetResult();
        }

        private void OnMessageDelete(MessageDeleteEvent obj)
        {
            _hub.InvokeAsync("RelayMessageDeleted", obj).GetAwaiter().GetResult();
        }

        private void OnMessageDeleteBulk(MessageDeleteBulkEvent obj)
        {
            _hub.InvokeAsync("RelayMessageDeleted", obj).GetAwaiter().GetResult();
        }

        private void OnReactionAdded(MessageReactionAddEvent obj)
        {
            _hub.InvokeAsync("RelayReactionAdded", obj).GetAwaiter().GetResult();
        }

        private void OnReactionRemoved(MessageReactionRemoveEvent obj)
        {
            _hub.InvokeAsync("RelayReactionRemoved", obj).GetAwaiter().GetResult();
        }

        private void OnReactionRemovedAll(MessageReactionRemoveAllEvent obj)
        {
            _hub.InvokeAsync("RelayReactionRemoved", obj).GetAwaiter().GetResult();
        }
    }
}
