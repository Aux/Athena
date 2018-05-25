using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace Pugster
{
    public class OverwatchDatabase : DbContext
    {
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<ProfileHero> ProfileHeroes { get; set; }

        public OverwatchDatabase()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string baseDir = Path.Combine(AppContext.BaseDirectory, "data");
            if (!Directory.Exists(baseDir))
                Directory.CreateDirectory(baseDir);

            string datadir = Path.Combine(baseDir, "overwatch.sqlite.db");
            optionsBuilder.UseSqlite($"Filename={datadir}");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

        }
    }
}
