using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pugster
{
    public partial class RootController
    {
        public Task<bool> LobbyExistsAsync(ulong id)
            => _db.Lobbies.AnyAsync(x => x.Id == id);
        public Task<bool> LobbyExistsAsync(string name)
            => _db.Lobbies.AnyAsync(x => x.Name.ToLower() == name.ToLower());
        public Task<bool> LobbyHasPlayerAsync(ulong lobbyId, ulong profileId)
            => _db.Players.AnyAsync(x => x.LobbyId == lobbyId && x.ProfileId == profileId);

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
    }
}
