using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Linq;
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
            string roleName = $"Lobby: {name}";
            if (Context.Guild.Roles.Any(x => x.Name == roleName))
            {
                await ReplyAsync($"A role by the name of `{roleName}` already exists in this guild. Rename the role or choose another name for your lobby.");
                return;
            }

            var role = await Context.Guild.CreateRoleAsync(roleName);
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
