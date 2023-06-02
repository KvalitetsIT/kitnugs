using KitNugs.Configuration;
using KitNugs.Repository;
using KitNugs.Services.Model;

namespace KitNugs.Services
{
    public class HelloService : IHelloService
    {
        private readonly string configurationValue;
        private readonly ILogger<HelloService> logger;
        private readonly IAppDbContext dbContext;

        public HelloService(IServiceConfiguration configuration, ILogger<HelloService> logger, IAppDbContext dbContext)
        {
            configurationValue = configuration.GetConfigurationValue(ConfigurationVariables.TEST_VAR);
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public async Task<HelloModel> BusinessLogic(string name)
        {
            await dbContext.HelloTable.AddAsync(new HelloTable
            {
                Created = DateTimeOffset.Now,
            });

            logger.LogDebug("Doing business logic.");

            dbContext.SaveChanges();

            return new HelloModel()
            {
                Name = name,
                Now = DateTime.Now,
                FromConfiguration = configurationValue,
            };
        }
    }
}
