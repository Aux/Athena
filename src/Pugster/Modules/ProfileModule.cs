using Discord;
using Discord.Commands;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pugster
{
    public class ProfileModule : PugsterModuleBase
    {
        private readonly ProfileController _profiles;
        private readonly OverwatchController _overwatch;

        public ProfileModule(ProfileController profiles, OverwatchController overwatch)
        {
            _profiles = profiles;
            _overwatch = overwatch;
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
        [Summary("View a user's profile by name or battletag")]
        public async Task ProfileAsync(Profile profile)
        {
            var user = Context.Guild.GetUser(profile.Id);
            var builder = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(profile.Description))
                builder.Append(profile.Description);
            else
                builder.Append("*no description available*");

            builder.Append($"\n\n**Battletag:** {profile.BattleTag}");

            var rating = EnumHelper.GetSkillRating(profile.SkillRating);
            builder.Append($"\n**Skill Rating:** {profile.SkillRating} ({rating})");

            var heroes = await _overwatch.GetProfileHeroesAsync(profile.Id);
            if (heroes.Count > 0)
                builder.Append($"\n**Preferred Heroes:** {string.Join(", ", heroes.Select(x => x.Name))}");

            var embed = new EmbedBuilder()
                .WithAuthor(user.ToString(), user.GetAvatarUrl(ImageFormat.Jpeg))
                .WithDescription(builder.ToString())
                .WithFooter("Last Updated")
                .WithTimestamp(profile.UpdatedAt);

            await ReplyAsync("", embed: embed);
        }
        
        [Command("profiles"), Alias("players")]
        [Summary("View a summary of many users' profiles")]
        public async Task ProfilesAsync()
        {
            await Task.Delay(0);
        }
    }
}
