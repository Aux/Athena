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
        
        [Command("forcejoin")]
        [Summary("Force a user to join an open lobby by name")]
        public async Task ForceJoinAsync(SocketUser user, [Remainder]Lobby lobby)
        {
            var hasPlayer = await _root.LobbyHasPlayerAsync(lobby.Id, user.Id);
            if (hasPlayer)
            {
                await ReplyAsync($"{user} has already joined this lobby.");
                return;
            }

            var player = new Player
            {
                LobbyId = lobby.Id,
                ProfileId = user.Id
            };

            await _root.CreatePlayerAsync(player);
            await ReplyAsync($"{user.Mention} has been forced into the lobby");
        }

        [Command("forceleave")]
        [Summary("Force a user to leave an open lobby by name")]
        public async Task ForceLeaveAsync(SocketUser user, [Remainder]Lobby lobby)
        {
            var player = await _root.GetPlayerFromLobbyAsync(lobby.Id, user.Id);
            if (player == null)
            {
                await ReplyAsync($"{user} is not in this lobby.");
                return;
            }
            
            await _root.DeletePlayerAsync(player);
            await ReplyAsync($"{user.Mention} has been forced out of the lobby");
        }

        [Command("createlobby")]
        public Task CreateLobbyAsync(string name, [Remainder]string description = null)
            => CreateLobbyAsync(name, description);
        
        [Command("createlobby")]
        [Summary("Create a new game lobby")]
        public async Task CreateLobbyAsync(string name, string description = null, 
            [Range(0, 5000)]int skillratingmin = -1,
            [Range(0, 5000)]int skillratingmax = -1)
        {
            bool lobbyExists = await _root.LobbyExistsAsync(name);
            if (lobbyExists)
            {
                await ReplyAsync($"A lobby named `{name}` already exists in this guild.");
                return;
            }

            if (skillratingmin > skillratingmax)
            {
                await ReplyAsync($"The minimum skill rating ({skillratingmin}) cannot be higher than the maximum skill rating ({skillratingmax}).");
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
                RoleId = role?.Id,
                SkillRatingMax = skillratingmax,
                SkillRatingMin = skillratingmin
            };

            await _root.CreateLobbyAsync(lobby);
            await ReplySuccessAsync();
        }

        [Command("openlobby")]
        [Summary("Open a lobby for new players to join")]
        public async Task OpenLobbyAsync([Remainder]Lobby lobby)
        {
            if (lobby.IsOpen)
            {
                await ReplyAsync("This lobby is already open.");
                return;
            }

            lobby.IsOpen = true;
            await _root.ModifyLobbyAsync(lobby);
            await ReplySuccessAsync();
        }

        [Command("closelobby")]
        [Summary("Close a lobby from new players")]
        public async Task CloseLobbyAsync([Remainder]Lobby lobby)
        {
            if (!lobby.IsOpen)
            {
                await ReplyAsync("This lobby is already closed.");
                return;
            }

            lobby.IsOpen = false;
            await _root.ModifyLobbyAsync(lobby);
            await ReplySuccessAsync();
        }

        [Command("ratelobby")]
        [Summary("Change a lobby's skill rating requirements")]
        public async Task RateLobbyAsync(Lobby lobby, 
            [Range(0, 5000)]int skillratingmin,
            [Range(0, 5000)]int skillratingmax)
        {
            lobby.SkillRatingMin = skillratingmin;
            lobby.SkillRatingMax = skillratingmax;
            await _root.ModifyLobbyAsync(lobby);
            await ReplySuccessAsync();
        }

        [Command("rolecall")]
        [Summary("Ping all players of a lobby with a message")]
        public async Task RoleCallAsync(Lobby lobby, [Remainder]string message)
        {
            if (!lobby.RoleId.HasValue)
            {
                await ReplyAsync("This lobby doesn't have a role configured.");
                return;
            }

            var role = Context.Guild.GetRole(lobby.RoleId.Value);

            var embed = new EmbedBuilder()
                .WithAuthor(Context.User)
                .WithDescription(message);

            await role.ModifyAsync(x => x.Mentionable = true);
            await ReplyAsync(role.Mention, embed: embed);
            await role.ModifyAsync(x => x.Mentionable = false);
        }
    }
}
