using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Pugster
{
    public class ProfileModule : PugsterModuleBase
    {
        private readonly ProfileController _profiles;

        public ProfileModule(ProfileController profiles)
        {
            _profiles = profiles;
        }
        
        [Command("createprofile")]
        [Summary("Create your overwatch pug profile")]
        public async Task CreateProfileAsync(string battleTag, [Range(0, 5001)]int? skillRating = null)
        {
            var profile = await _profiles.CreateProfileAsync(new Profile
            {
                Name = Context.User.ToString(),
                BattleTag = battleTag,
                SkillRating = skillRating ?? 0
            });

            var dm = await Context.User.GetOrCreateDMChannelAsync();
            await dm.SendMessageAsync("Thank you for creating a profile with Pugster!" +
                " You can now add addition information to your profile, such as preferred" +
                " role, heroes, etc...\n\nType `!howto profilesetup` for more information");
            await ReplyReactionAsync(":thumbsup:");
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
