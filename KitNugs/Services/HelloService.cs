using KitNugs.Configuration;
using KitNugs.Services.Model;

namespace KitNugs.Services
{
    public class HelloService : IHelloService
    {
        private readonly IServiceConfiguration configuration;

        public HelloService(IServiceConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<HelloModel> BusinessLogic(string name)
        {
            string testVariable = configuration.GetConfigurationValue(IServiceConfiguration.ConfigurationVariables.TEST_VAR);
            
            return new HelloModel()
            {
                Name = name,
                Now = DateTime.Now,
                FromConfiguration = testVariable,
            };
        }
    }
}
