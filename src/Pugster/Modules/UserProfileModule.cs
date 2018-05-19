using Discord.Commands;
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
        
        [Command("setbattletag"), Alias("battletag", "setbtag", "btag")]
        public async Task SetBattleTagAsync(BattleTag battleTag)
        {
            await Task.Delay(0);
        }

        [Command("setskillrating"), Alias("skillrating", "setrating", "rating", "setsr", "sr")]
        public async Task SetSkillRatingAsync([Range(0, 5001)]int skillRating)
        {
            await Task.Delay(0);
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
