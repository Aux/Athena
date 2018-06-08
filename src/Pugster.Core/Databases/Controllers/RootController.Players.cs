using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pugster
{
    // Players
    public partial class RootController
    {
        public Task<bool> PlayerExistsAsync(ulong id)
            => _db.Lobbies.AnyAsync(x => x.Id == id);

        public Task<int> GetLobbyTotalPlayersAsync(ulong lobbyId)
            => _db.Players.CountAsync(x => x.LobbyId == lobbyId);

        public Task<Player> GetPlayerAsync(ulong id)
            => _db.Players.SingleOrDefaultAsync(x => x.Id == id);
        public Task<List<Player>> GetPlayersAsync(ulong lobbyId)
            => _db.Players.Where(x => x.LobbyId == lobbyId).ToListAsync();
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
