using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace Pugster
{
    [RequireProfile]
    [RequireContext(ContextType.DM)]
    public class UserProfileModule : PugsterModuleBase
    {
        private readonly ProfileController _profiles;

        public UserProfileModule(ProfileController profiles)
        {
            _profiles = profiles;
        }
        
        private async Task ModifyAsync(Action<Profile> action)
        {
            var profile = await _profiles.GetProfileAsync(Context.User.Id);
            action(profile);
            await _profiles.ModifyAsync(profile);
            await ReplySuccessAsync();
        }

        [Command("setbattletag"), Alias("battletag", "setbtag", "btag")]
        public async Task SetBattleTagAsync(BattleTag battleTag)
        {
            await ModifyAsync(x => x.BattleTag = battleTag.ToString());
        }

        [Command("setskillrating"), Alias("skillrating", "setrating", "rating", "setsr", "sr")]
        public async Task SetSkillRatingAsync([Range(0, 5000)]int skillRating)
        {
            await ModifyAsync(x => x.SkillRating = skillRating);
        }

        [Command("addheroes"), Alias("addhero")]
        public async Task AddHeroesAsync(params Hero[] heroName)
        {
            await Task.Delay(0);
        }

        [Command("removeheroes"), Alias("removehero", "deletehero", "delhero")]
        public async Task RemoveHeroesAsync(params Hero[] heroName)
        {
            await Task.Delay(0);
        }
    }
}
