using DotNet.Testcontainers.Builders;
using KitNugs.Repository;
using Microsoft.EntityFrameworkCore;

namespace UnitTest
{
    public class TestDatabaseFixture
    {
        protected static readonly string ConnectionString;

        private static readonly object _lock = new();

        static TestDatabaseFixture()
        {
            var network = new NetworkBuilder().Build();

            var db = new Testcontainers.MariaDb.MariaDbBuilder()
                .WithUsername("hellouser")
                .WithNetwork(network)
                .WithPassword("secret1234")
                .WithDatabase("hellodb")
                .Build();

            db.StartAsync()
                .Wait();

            ConnectionString = db.GetConnectionString();
        }

        public TestDatabaseFixture()
        {
            lock (_lock)
            {
                using (var context = CreateContext())
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                }
            }
        }

        public AppDbContext CreateContext()
            => new AppDbContext(
                new DbContextOptionsBuilder<AppDbContext>()
                    .UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString))
                    .Options);
    }
}
