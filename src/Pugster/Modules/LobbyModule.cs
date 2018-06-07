using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pugster
{
    //
    // Please fix your bad logic idiot man
    //

    public class LobbyModule : PugsterModuleBase
    {
        private readonly RootController _root;

        public LobbyModule(RootController root)
        {
            _root = root;
        }

        [Command("lobbies")]
        [Summary("List currently available lobbies")]
        public async Task LobbiesAsync()
        {
            var lobbyCount = await _root.GetTotalOpenLobbiesAsync();
            var lobbies = await _root.GetOpenLobbiesAsync();

            if (lobbyCount == 0)
            {
                await ReplyAsync("There are currently no open lobbies available.");
                return;
            }

            var embed = new EmbedBuilder()
                .WithTitle("Available Lobbies")
                .WithDescription(string.Join(", ", lobbies.Select(x => x.Name)))
                .WithFooter($"Showing {lobbies.Count} of {lobbyCount}");

            await ReplyEmbedAsync(embed);
        }

        [Command("mylobbies")]
        [Summary("List lobbies you have joined")]
        [RequireProfile]
        public async Task MyLobbiesAsync()
        {
            var lobbyCount = await _root.GetTotalOpenLobbiesAsync();
            var lobbies = await _root.GetOpenLobbiesAsync();

            if (lobbyCount == 0)
            {
                await ReplyAsync("There are currently no open lobbies available.");
                return;
            }

            var embed = new EmbedBuilder()
                .WithTitle("Available Lobbies")
                .WithDescription(string.Join(", ", lobbies.Select(x => x.Name)))
                .WithFooter($"Showing {lobbies.Count} of {lobbyCount}");

            await ReplyEmbedAsync(embed);
        }


        [Command("lobby")]
        [Summary("View information about a lobby")]
        [RequireProfile]
        public async Task LobbAsync([Remainder]Lobby lobby)
        {
            var embed = new EmbedBuilder()
                .WithColor(lobby.IsOpen ? Color.Green : Color.Red)
                .WithTitle(lobby.Name)
                .WithDescription(lobby.Description)
                .WithFooter("Created At")
                .WithTimestamp(lobby.CreatedAt);
            
            if (lobby.SkillRatingMin == -1 && lobby.SkillRatingMax == -1)
                embed.AddInlineField("Rating Requirements", $"*None*");
            else
            {
                string ratingMin = lobby.SkillRatingMin == -1 ? "*No Minimum*" : lobby.SkillRatingMin.ToString();
                string ratingMax = lobby.SkillRatingMax == -1 ? "*No Maximum*" : lobby.SkillRatingMax.ToString();
                embed.AddInlineField("Rating Requirements", $"{ratingMin} -> {ratingMax}");
            }

            var playerCount = await _root.GetLobbyTotalPlayersAsync(lobby.Id);
            embed.AddInlineField("Players", playerCount);

            if (lobby.RoleId != null)
            {
                var role = Context.Guild.GetRole(lobby.RoleId.Value);
                embed.AddField("Role", role.Mention);
            }
            
            await ReplyEmbedAsync(embed);
        }

        [Command("lobbyplayers")]
        [Summary("View players who have joined a lobby")]
        [RequireProfile]
        public async Task LobbyPlayersAsync([Remainder]Lobby lobby)
        {
            var players = await _root.GetLobbyPlayersAsync(lobby.Id);

            if (players.Count() == 0)
            {
                await ReplyAsync("There are no players in this lobby :'(");
                return;
            }

            var builder = new StringBuilder();
            foreach (var player in players)
            {
                var profile = await _root.GetProfileAsync(player.ProfileId);
                var user = Context.Guild.GetUser(profile.Id);
                builder.AppendLine($"{user.Mention}\t {EnumHelper.GetSkillRating(profile.SkillRating)} ({profile.SkillRating})");
            }

            var embed = new EmbedBuilder()
                .WithTitle($"Players in {lobby.Name}")
                .WithDescription(builder.ToString())
                .WithFooter($"{players.Count()} Player(s)");
            await ReplyEmbedAsync(embed);
        }

        [Command("join")]
        [Summary("Join an open lobby by name")]
        [RequireProfile]
        public async Task JoinAsync([Remainder]Lobby lobby)
        {
            var player = new Player
            {
                LobbyId = lobby.Id,
                ProfileId = Context.User.Id
            };

            if (lobby.RoleId.HasValue)
            {
                var role = Context.Guild.GetRole(lobby.RoleId.Value);
                var guildUser = Context.User as SocketGuildUser;
                await guildUser.AddRoleAsync(role);
            }

            await _root.CreatePlayerAsync(player);
            await ReplySuccessAsync();
        }
        
        [Command("leave")]
        [Summary("Leave a lobby by name")]
        [RequireProfile]
        public async Task LeaveAsync([Remainder]Lobby lobby)
        {
            var player = await _root.GetPlayerAsync(Context.User.Id);
            if (player == null)
            {
                await ReplyAsync("You have not joined this lobby.");
                return;
            }

            if (lobby.RoleId.HasValue)
            {
                var role = Context.Guild.GetRole(lobby.RoleId.Value);
                var guildUser = Context.User as SocketGuildUser;
                await guildUser.RemoveRoleAsync(role);
            }

            await _root.DeletePlayerAsync(player);
            await ReplySuccessAsync();
        }

        [Command("leaveall")]
        [Summary("Leave all lobbies")]
        [RequireProfile]
        public async Task LeaveAllAsync()
        {
            await Task.Delay(0);
        }
    }
}
