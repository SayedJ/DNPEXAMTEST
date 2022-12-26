using Microsoft.EntityFrameworkCore;
using WebApplicationApi.Model;

namespace WebApplicationApi.DatabaseContext;

public class FootballContext : DbContext
{

    public DbSet<Team> Teams { get; set; }
    public DbSet<Player> Players { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename=Database.db");
    }
    
    
}