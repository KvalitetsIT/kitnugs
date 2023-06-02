using Microsoft.EntityFrameworkCore;

namespace KitNugs.Repository
{
    public interface IAppDbContext
    {
        DbSet<HelloTable> HelloTable { get; }
        int SaveChanges();
    }
}
