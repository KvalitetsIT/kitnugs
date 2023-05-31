using KitNugs.Configuration;
using KitNugs.Services.Model;

namespace KitNugs.Services
{
    public class HelloService : IHelloService
    {
        private readonly string configurationValue;

        public HelloService(IServiceConfiguration configuration)
        {
            this.configurationValue = configuration.GetConfigurationValue(ConfigurationVariables.TEST_VAR);
        }

        public async Task<HelloModel> BusinessLogic(string name)
        {
            return new HelloModel()
            {
                Name = name,
                Now = DateTime.Now,
                FromConfiguration = configurationValue,
            };
        }
    }
}
