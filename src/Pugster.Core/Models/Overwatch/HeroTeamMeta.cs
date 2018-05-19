namespace Pugster
{
    public class HeroTeamMeta
    {
        public ulong Id { get; set; }
        public ulong HeroId { get; set; }
        public ulong TeamMetaId { get; set; }

        public Hero Hero { get; set; }
        public TeamMeta TeamMeta { get; set; }
    }
}
