using System;
using System.Threading.Tasks;

namespace Pugster
{
    public class Lobby
    {
        public ulong Id { get; set; }
        public ulong OwnerId { get; set; }
        public ulong? RoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsOpen { get; set; }
    }
}
