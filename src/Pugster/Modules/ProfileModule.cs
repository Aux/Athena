using Discord;
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
        public async Task CreateProfileAsync(string battleTag, [Range(0, 5000)]int skillRating = 0)
        {
            var profile = await _profiles.CreateAsync(new Profile
            {
                Id = Context.User.Id,
                Name = Context.User.ToString(),
                BattleTag = battleTag,
                SkillRating = skillRating
            });

            var dm = await Context.User.GetOrCreateDMChannelAsync();
            await dm.SendMessageAsync("Thank you for creating a profile with Pugster!" +
                " You can now add additional information to your profile, such as preferred" +
                " roles, heroes, etc...\n\nType `!howto profilesetup` for more information");
            await ReplySuccessAsync();
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
