using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pugster
{
    public class OverwatchController : DbController<OverwatchDatabase>
    {
        public OverwatchController(OverwatchDatabase db) : base(db) { }

        public Task<Hero> GetHeroAsync(ulong heroId)
            => _db.Heroes.SingleOrDefaultAsync(x => x.Id == heroId);
        public Task<Hero> GetHeroAsync(string heroName)
            => _db.Heroes.SingleOrDefaultAsync(x => x.Name == heroName);

        public Task<List<Hero>> GetHeroesAsync()
            => _db.Heroes.ToListAsync();
        public Task<List<Hero>> GetHeroesAsync(params ulong[] heroIds)
            => _db.Heroes.Where(x => heroIds.Contains(x.Id)).ToListAsync();
        public Task<List<Hero>> GetHeroesAsync(params string[] heroNames)
            => _db.Heroes.Where(x => heroNames.Contains(x.Name.ToLower())).ToListAsync();
        public Task<List<Hero>> GetHeroesAsync(HeroClass heroClass)
            => _db.Heroes.Where(x => x.Class == heroClass).ToListAsync();

        public async Task<List<Hero>> CreateHeroesAsync(params Hero[] heroes)
        {
            foreach (var hero in heroes)
            {
                await _db.Heroes.AddAsync(hero);
                await _db.SaveChangesAsync();
            }
            return await GetHeroesAsync();
        }
    }
}
