using System.Linq;

namespace Pugster
{
    public struct BattleTag
    {
        public string Name { get; }
        public string Discriminator { get; }

        public bool IsValid
            => (Name != null) && ((Discriminator != null) && Discriminator.Length > 4);

        private BattleTag(string battleTag)
        {
            var parts = battleTag.Split(new[] { '#' }, 2);

            string name = parts.FirstOrDefault();
            string discrim = parts.LastOrDefault();

            if (uint.TryParse(discrim, out uint _))
                Discriminator = discrim;
            else
                Discriminator = null;

            if (!string.IsNullOrWhiteSpace(name))
                Name = name;
            else
                Name = null;
        }

        public static BattleTag Parse(string battleTag)
            => new BattleTag(battleTag);
        public override string ToString()
            => $"{Name}#{Discriminator}";
    }
}
