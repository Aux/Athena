using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Pugster
{
    public class LobbyModule : ModuleBase<SocketCommandContext>
    {
        [Command("lobbies")]
        [Summary("List currently available lobbies")]
        public async Task LobbiesAsync()
        {
            await Task.Delay(0);
        }
        
        [Command("join")]
        [Summary("Join an open lobby by name")]
        public async Task JoinAsync([Remainder]string name)
        {
            await Task.Delay(0);
        }
        
        [Command("leave")]
        [Summary("Leave your current lobby")]
        public async Task LeaveAsync()
        {
            await Task.Delay(0);
        }

        [Command("leave")]
        [Summary("Leave a lobby by name")]
        public async Task LeaveAsync([Remainder]string name)
        {
            await Task.Delay(0);
        }

        [Command("leaveall")]
        [Summary("Leave all lobbies")]
        public async Task LeaveAllAsync()
        {
            await Task.Delay(0);
        }
    }
}
