using KitNugs.Configuration;
using KitNugs.Services.Model;

namespace KitNugs.Services
{
    public class HelloService : IHelloService
    {
        private readonly string configurationValue;
        private readonly ILogger<HelloService> logger;

        public HelloService(IServiceConfiguration configuration, 
            ILogger<HelloService> logger)
        {
            this.configurationValue = configuration.GetConfigurationValue(ConfigurationVariables.TEST_VAR);
            this.logger = logger;
        }

        public async Task<HelloModel> BusinessLogic(string name)
        {
            logger.LogDebug("Doing business logic.");
            return new HelloModel()
            {
                Name = name,
                Now = DateTime.Now,
                FromConfiguration = configurationValue,
            };
        }
    }
}
