namespace Athena
{
    public class LobbyPlayer
    {
        public ulong Id { get; set; }
        public ulong LobbyId { get; set; }
        public ulong PlayerId { get; set; }

        public Lobby Lobby { get; set; }
        public Player Player { get; set; }
    }
}
