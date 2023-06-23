using KitNugs.Configuration;
using KitNugs.Repository;
using KitNugs.Services.Model;

namespace KitNugs.Services
{
    public class HelloService : IHelloService
    {
        private readonly string _configurationValue;
        private readonly ILogger<HelloService> _logger;
        private readonly AppDbContext _dbContext;

        public HelloService(IServiceConfiguration configuration, ILogger<HelloService> logger, AppDbContext dbContext)
        {
            _configurationValue = configuration.GetConfigurationValue(ConfigurationVariables.TEST_VAR);
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<HelloModel> BusinessLogic(string name)
        {
            await _dbContext.HelloTable.AddAsync(new HelloTable
            {
                Created = DateTimeOffset.Now,
            });

            _logger.LogDebug("Doing business logic.");

            _dbContext.SaveChanges();

            return new HelloModel()
            {
                Name = name,
                Now = DateTime.Now,
                FromConfiguration = _configurationValue,
            };
        }
    }
}
