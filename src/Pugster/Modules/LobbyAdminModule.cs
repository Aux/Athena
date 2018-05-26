using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;
using System.Linq;
using System.Threading.Tasks;

namespace Pugster.Modules
{
    [RequireUserPermission(GuildPermission.Administrator)]
    public class LobbyAdminModule : PugsterModuleBase
    {
        private readonly RootController _root;

        public LobbyAdminModule(RootController root)
        {
            _root = root;
        }
        
        [Command("createlobby")]
        [Summary("Create a new game lobby")]
        public async Task CreateLobbyWithRoleAsync(string name, [Remainder]string description = null)
        {
            bool lobbyExists = await _root.LobbyExistsAsync(name);
            if (lobbyExists)
            {
                await ReplyAsync($"A lobby named `{name}` already exists in this guild.");
                return;
            }
            
            RestRole role = null;
            if (Context.Guild.CurrentUser.GuildPermissions.ManageRoles)
            {
                string roleName = $"Lobby: {name}";
                if (Context.Guild.Roles.Any(x => x.Name == roleName))
                {
                    await ReplyAsync($"A role named `{roleName}` already exists in this guild, rename the role or choose another name for your lobby.");
                    return;
                }

                role = await Context.Guild.CreateRoleAsync(roleName);
            }

            var lobby = new Lobby
            {
                Name = name,
                Description = description,
                OwnerId = Context.User.Id,
                RoleId = role?.Id
            };

            await _root.CreateLobbyAsync(lobby);
            await ReplySuccessAsync();
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
