using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pugster
{
    public class OverwatchController : DbController<OverwatchDatabase>
    {
        private readonly RootController _profiles;

        public OverwatchController(OverwatchDatabase db, RootDatabase root) : base(db)
        {
            _profiles = new RootController(root);
        }

        //
        // Heroes
        //

        public Task<Hero> GetHeroAsync(ulong heroId)
            => _db.Heroes.SingleOrDefaultAsync(x => x.Id == heroId);
        public Task<Hero> GetHeroAsync(string heroName)
            => _db.Heroes.SingleOrDefaultAsync(x => x.Name.ToLower() == heroName.ToLower());

        public Task<List<Hero>> GetHeroesAsync()
            => _db.Heroes.ToListAsync();
        public Task<List<Hero>> GetHeroesAsync(params ulong[] heroIds)
            => _db.Heroes.Where(x => heroIds.Contains(x.Id)).ToListAsync();
        public Task<List<Hero>> GetHeroesAsync(params string[] heroNames)
            => _db.Heroes.Where(x => heroNames.Contains(x.Name.ToLower())).ToListAsync();
        public Task<List<Hero>> GetHeroesAsync(HeroClass heroClass)
            => _db.Heroes.Where(x => x.Class == heroClass).ToListAsync();
        public async Task<List<Hero>> FindHeroesAsync(string heroName, int distance = 3, int take = 3)
        {
            var hero = await GetHeroAsync(heroName);
            if (hero != null) return new List<Hero> { hero };

            var allHeroes = await GetHeroesAsync();
            var heroResult = allHeroes.Where(x => MathHelper.GetStringDistance(x.Name.ToLower(), heroName.ToLower()) <= distance).Take(take);
            return heroResult.ToList();
        }

        public async Task<List<Hero>> CreateHeroesAsync(params Hero[] heroes)
        {
            foreach (var hero in heroes)
            {
                await _db.Heroes.AddAsync(hero);
                await _db.SaveChangesAsync();
            }
            return await GetHeroesAsync();
        }

        //
        // Profile Heroes
        //

        public Task<List<Hero>> GetProfileHeroesAsync(ulong profileId)
            => _db.ProfileHeroes.Include(x => x.Hero).Where(x => x.ProfileId == profileId).Select(x => x.Hero).ToListAsync();
            
        public async Task AddProfileHeroesAsync(Profile profile, params Hero[] heroes)
        {
            foreach (var hero in heroes)
            {
                var profileHero = new ProfileHero
                {
                    HeroId = hero.Id,
                    ProfileId = profile.Id
                };

                await _db.ProfileHeroes.AddAsync(profileHero);
                await _db.SaveChangesAsync();
            }
            await _profiles.ModifyProfileAsync(profile);
        }

        public async Task RemoveProfileHeroesAsync(Profile profile, params Hero[] heroes)
        {
            var heroIds = heroes.Select(x => x.Id);
            var removeHeroes = await _db.ProfileHeroes.Where(x => x.ProfileId == profile.Id && heroIds.Contains(x.HeroId)).ToListAsync();

            _db.ProfileHeroes.RemoveRange(removeHeroes);
            await _db.SaveChangesAsync();
            await _profiles.ModifyProfileAsync(profile);
        }
    }
}
