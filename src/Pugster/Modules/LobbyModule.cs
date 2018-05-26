using Discord;
using Discord.Commands;
using System.Linq;
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

            var embed = new EmbedBuilder()
                .WithTitle("Available Lobbies")
                .WithDescription(string.Join(", ", lobbies.Select(x => x.Name)))
                .WithFooter($"Showing {lobbies?.Count ?? 0} of {lobbyCount}");

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

            await _root.CreatePlayerAsync(player);
            await ReplySuccessAsync();
        }

        [Command("leave")]
        [Summary("Leave your current lobby")]
        public async Task LeaveAsync()
        {
            await Task.Delay(0);
        }

        [Command("leave")]
        [Summary("Leave a lobby by name")]
        public async Task LeaveAsync([Remainder]Lobby lobby)
        {
            var player = await _root.GetPlayerAsync(Context.User.Id);
            if (player == null)
            {
                await ReplyAsync("You have not joined this lobby.");
                return;
            }

            await _root.DeletePlayerAsync(player);
            await ReplySuccessAsync();
        }

        [Command("leaveall")]
        [Summary("Leave all lobbies")]
        public async Task LeaveAllAsync()
        {
            await Task.Delay(0);
        }
    }
}
