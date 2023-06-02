using Microsoft.EntityFrameworkCore;

namespace KitNugs.Repository
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<HelloTable> HelloTable { get; set; }
    }
}
