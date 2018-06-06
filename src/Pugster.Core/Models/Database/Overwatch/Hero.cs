using System.Collections.Generic;

namespace Pugster
{
    public class Hero
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public HeroClass Class { get; set; }
    }
}
