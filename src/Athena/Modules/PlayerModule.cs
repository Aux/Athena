using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Pugster
{
    public class PlayerModule : ModuleBase<SocketCommandContext>
    {
        [Command("createprofile")]
        [Summary("Create your overwatch pug profile")]
        public async Task CreateProfileAsync(string battleTag, [Range(0, 5001)]int skillRating)
        {
            await Task.Delay(0);
        }

        [Command("profile"), Alias("player")]
        [Summary("View a user's profile by name")]
        public async Task ProfileAsync(SocketUser user)
        {
            await Task.Delay(0);
        }

        [Command("profile"), Alias("player")]
        [Summary("View a user's profile by battletag")]
        public async Task ProfileAsync(string battleTag)
        {
            await Task.Delay(0);
        }

        [Command("profiles"), Alias("players")]
        [Summary("View a summary of many users' profiles")]
        public async Task ProfilesAsync(SkillRating rating)
        {
            await Task.Delay(0);
        }
    }
}
