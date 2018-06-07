using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pugster
{
    public class RootController : DbController<RootDatabase>
    {
        public RootController(RootDatabase db) : base(db) { }

        //
        // Profiles
        //

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

        //
        // Lobbies
        //

        public Task<bool> LobbyExistsAsync(ulong id)
            => _db.Lobbies.AnyAsync(x => x.Id == id);
        public Task<bool> LobbyExistsAsync(string name)
            => _db.Lobbies.AnyAsync(x => x.Name.ToLower() == name.ToLower());
        public Task<int> GetTotalOpenLobbiesAsync()
            => _db.Lobbies.CountAsync(x => x.IsOpen);
        public Task<Lobby> GetLobbyAsync(ulong id)
            => _db.Lobbies.SingleOrDefaultAsync(x => x.Id == id);
        public Task<Lobby> GetLobbyAsync(string name)
            => _db.Lobbies.SingleOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
        public Task<List<Lobby>> GetLobbiesAsync()
            => _db.Lobbies.ToListAsync();
        public Task<List<Lobby>> GetOpenLobbiesAsync(int skip = 0, int take = 10)
            => _db.Lobbies.Where(x => x.IsOpen).Skip(skip).Take(take).ToListAsync();
        public async Task<List<Lobby>> FindLobbiesAsync(string name, int distance = 3, int take = 3)
        {
            var lobby = await GetLobbyAsync(name);
            if (lobby != null) return new List<Lobby> { lobby };

            var allLobbies = await GetLobbiesAsync();
            var lobbyResult = allLobbies.Where(x => MathHelper.GetStringDistance(x.Name.ToLower(), name.ToLower()) <= distance).Take(take);
            return lobbyResult.ToList();
        }

        public Task<bool> LobbyHasPlayerAsync(ulong lobbyId, ulong profileId)
            => _db.Players.AnyAsync(x => x.LobbyId == lobbyId && x.ProfileId == profileId);

        public async Task<Lobby> CreateLobbyAsync(Lobby lobby)
        {
            lobby.CreatedAt = DateTime.UtcNow;
            await _db.Lobbies.AddAsync(lobby);
            await _db.SaveChangesAsync();
            return lobby;
        }
        public async Task<Lobby> ModifyLobbyAsync(Lobby lobby)
        {
            await _db.SaveChangesAsync();
            return lobby;
        }

        //
        // Players
        //

        public Task<bool> PlayerExistsAsync(ulong id)
            => _db.Lobbies.AnyAsync(x => x.Id == id);
        public Task<Player> GetPlayerAsync(ulong id)
            => _db.Players.SingleOrDefaultAsync(x => x.Id == id);
        public Task<List<Player>> GetPlayersAsync(ulong lobbyId)
            => _db.Players.Where(x => x.LobbyId == lobbyId).ToListAsync();

        public Task<int> GetLobbyTotalPlayersAsync(ulong lobbyId)
            => _db.Players.CountAsync(x => x.LobbyId == lobbyId);
        public Task<List<Player>> GetLobbyPlayersAsync(ulong lobbyId)
            => _db.Players.Where(x => x.LobbyId == lobbyId).ToListAsync();
        public Task<Player> GetPlayerFromLobbyAsync(ulong lobbyId, ulong profileId)
            => _db.Players.FirstOrDefaultAsync(x => x.LobbyId == lobbyId && x.ProfileId == profileId);

        public async Task<Player> CreatePlayerAsync(Player player)
        {
            player.JoinedAt = DateTime.UtcNow;
            await _db.Players.AddAsync(player);
            await _db.SaveChangesAsync();
            return player;
        }
        public async Task DeletePlayerAsync(Player player)
        {
            _db.Remove(player);
            await _db.SaveChangesAsync();
        }
        public async Task<Player> ModifyPlayerAsync(Player player)
        {
            await _db.SaveChangesAsync();
            return player;
        }
    }
}
