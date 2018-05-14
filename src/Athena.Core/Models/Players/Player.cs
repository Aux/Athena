using System.Collections.Generic;

namespace Athena
{
    public class Player
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string BattleTag { get; set; }
        public int SkillRating { get; set; }

        public List<LobbyPlayer> Lobbies { get; set; }
    }
}
