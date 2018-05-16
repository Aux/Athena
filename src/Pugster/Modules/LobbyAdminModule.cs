using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Pugster.Modules
{
    [RequireUserPermission(GuildPermission.Administrator)]
    public class LobbyAdminModule : ModuleBase<SocketCommandContext>
    {
        [Command("createlobby")]
        [Summary("Create a new game lobby")]
        public async Task CreateLobbyAsync(string name, [Remainder]string description)
        {
            await Task.Delay(0);
        }

        [Command("forcejoin")]
        [Summary("Force a user to join an open lobby by name")]
        public async Task ForceJoinAsync(SocketUser user, [Remainder]string name)
        {
            await Task.Delay(0);
        }

        [Command("forceleave")]
        [Summary("Force a user to leave an open lobby by name")]
        public async Task ForceLeaveAsync(SocketUser user, [Remainder]string name)
        {
            await Task.Delay(0);
        }
    }
}
