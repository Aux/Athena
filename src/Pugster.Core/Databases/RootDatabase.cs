using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace Pugster
{
    public class RootDatabase : DbContext
    {
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Lobby> Lobbies { get; set; }
        public DbSet<Player> Players { get; set; }

        public RootDatabase()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string baseDir = Path.Combine(AppContext.BaseDirectory, "data");
            if (!Directory.Exists(baseDir))
                Directory.CreateDirectory(baseDir);

            string datadir = Path.Combine(baseDir, "root.sqlite.db");
            optionsBuilder.UseSqlite($"Filename={datadir}");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

        }
    }
}
