﻿using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Pugster
{
    public class ProfileController : DbController<RootDatabase>
    {
        public ProfileController(RootDatabase db) : base(db) { }

        public Task<bool> ProfileExistsAsync(ulong profileId)
            => _db.Profiles.AnyAsync(x => x.Id == profileId);
        public Task<Profile> GetProfileAsync(ulong profileId)
            => _db.Profiles.SingleOrDefaultAsync(x => x.Id == profileId);
        public Task<Profile> GetProfileAsync(string name)
            => _db.Profiles.SingleOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
        public Task<Profile> GetProfileAsync(BattleTag battleTag)
            => _db.Profiles.SingleOrDefaultAsync(x => x.BattleTag.ToLower() == battleTag.ToString().ToLower());

        public async Task<Profile> CreateProfileAsync(Profile profile)
        {
            await _db.Profiles.AddAsync(profile);
            await _db.SaveChangesAsync();
            return await GetProfileAsync(profile.Name);
        }
    }
}
