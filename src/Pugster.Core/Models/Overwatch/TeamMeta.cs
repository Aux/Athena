using System.Collections.Generic;

namespace Pugster
{
    public class TeamMeta
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<HeroTeamMeta> Heroes { get; set; }
    }
}
