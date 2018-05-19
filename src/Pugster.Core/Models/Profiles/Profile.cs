using System;

namespace Pugster
{
    public class Profile
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string BattleTag { get; set; }
        public int SkillRating { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
