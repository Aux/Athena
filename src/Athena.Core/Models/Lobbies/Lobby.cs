using System.Collections.Generic;

namespace Athena
{
    public class Lobby
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<LobbyPlayer> Players { get; set; }
    }
}
