using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Pugster
{
    // Profiles
    public partial class RootController
    {
        public Task<bool> ProfileExistsAsync(ulong id)
            => _db.Profiles.AnyAsync(x => x.Id == id);

        public Task<Profile> GetProfileAsync(ulong id)
            => _db.Profiles.SingleOrDefaultAsync(x => x.Id == id);
        public Task<Profile> GetProfileAsync(string name)
            => _db.Profiles.SingleOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
        public Task<Profile> GetProfileAsync(BattleTag battleTag)
            => _db.Profiles.SingleOrDefaultAsync(x => x.BattleTag.ToLower() == battleTag.ToString().ToLower());

        public async Task<Profile> CreateProfileAsync(Profile profile)
        {
            profile.UpdatedAt = DateTime.UtcNow;
            await _db.Profiles.AddAsync(profile);
            await _db.SaveChangesAsync();
            return profile;
        }
        public async Task<Profile> ModifyProfileAsync(Profile profile)
        {
            profile.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            return profile;
        }
    }
}
