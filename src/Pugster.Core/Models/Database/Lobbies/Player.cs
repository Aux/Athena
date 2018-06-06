using System;

namespace Pugster
{
    public class Player
    {
        public ulong Id { get; set; }
        public ulong LobbyId { get; set; }
        public ulong ProfileId { get; set; }
        public DateTime JoinedAt { get; set; }
    }
}
