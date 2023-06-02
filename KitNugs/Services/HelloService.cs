using KitNugs.Configuration;
using KitNugs.Repository;
using KitNugs.Services.Model;
using Microsoft.EntityFrameworkCore;

namespace KitNugs.Services
{
    public class HelloService : IHelloService
    {
        private readonly string configurationValue;
        private readonly ILogger<HelloService> logger;
        private readonly FileName dbContext;

        public HelloService(IServiceConfiguration configuration, 
            ILogger<HelloService> logger, 
            FileName dbContext)
        {
            this.configurationValue = configuration.GetConfigurationValue(ConfigurationVariables.TEST_VAR);
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public async Task<HelloModel> BusinessLogic(string name)
        {
            logger.LogDebug("Doing business logic.");
            var rowsInTable = await dbContext.HelloTable.CountAsync();
            logger.LogInformation($"Number of rows in table: {rowsInTable}");

            return new HelloModel()
            {
                Name = name,
                Now = DateTime.Now,
                FromConfiguration = configurationValue,
            };
        }
    }
}
