using Microsoft.EntityFrameworkCore;

namespace FootballManagement.Model
{
    public class FussballmannschaftDB : DbContext
    {
        public FussballmannschaftDB(DbContextOptions<FussballmannschaftDB> options) : base(options)
        {
        }
        public DbSet<Spieler> Spieler { get; set; }

        public DbSet<Fussballmannschaft> Fussballmannschaft { get; set; }
    }
}
