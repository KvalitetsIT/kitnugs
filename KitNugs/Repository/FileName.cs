using Microsoft.EntityFrameworkCore;

namespace KitNugs.Repository
{
    public class FileName : DbContext
    {
        public FileName(DbContextOptions options) : base(options)
        {
        }
        public DbSet<HelloTable> HelloTable { get; set; }
    }

    public class HelloTable
    {
        public int HelloTableId { get; set; }
        public DateTimeOffset Created { get; set; }
    }
}
