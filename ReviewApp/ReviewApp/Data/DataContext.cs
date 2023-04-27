using Microsoft.EntityFrameworkCore;
using PlayCapsViewer.Models;

namespace PlayCapsViewer.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext>options) : base(options) { }
       
        //tell the dbcontext what our tables are 
        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<PlayCap> PlayCaps { get; set; }
        public DbSet<Player> Players { get; set; }
        //also include join tables
        public DbSet<PlayCapsCategory> PlayCapsCategories { get; set; }
        public DbSet<PlayCapsPlayer> PlayCapsPlayers { get; set; }
        public DbSet<PlayCap> Review { get; set; }
        public DbSet<PlayCap> Reviewer { get; set; }
    }

}
