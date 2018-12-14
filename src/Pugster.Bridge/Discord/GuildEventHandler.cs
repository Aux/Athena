using Microsoft.AspNetCore.SignalR.Client;
using Wumpus;
using Wumpus.Events;

namespace Pugster.Bridge.Discord
{
    public class GuildEventHandler : BaseEventHandler
    {
        private readonly WumpusGatewayClient _gateway;

        public GuildEventHandler(WumpusGatewayClient gateway, string hubUrl) : base(hubUrl)
        {
            _gateway = gateway;
            
            _gateway.GuildMemberAdd += OnGuildMemberAdd;
            _gateway.GuildMemberRemove += OnGuildMemberRemove;
        }
        
        private void OnGuildMemberRemove(GuildMemberRemoveEvent obj)
        {
            _hub.InvokeAsync("RelayUserJoined", obj).GetAwaiter().GetResult();
        }

        private void OnGuildMemberAdd(GuildMemberAddEvent obj)
        {
            _hub.InvokeAsync("RelayUserLeft", obj).GetAwaiter().GetResult();
        }
    }
}
