namespace Pugster
{
    public class ProfileHero
    {
        public ulong Id { get; set; }
        public ulong ProfileId { get; set; }
        public ulong HeroId { get; set; }
        
        public Hero Hero { get; set; }
    }
}
