using KitNugs.Configuration;
using Microsoft.Extensions.Configuration;

namespace UnitTest.Configuration
{
    internal class ServiceConfigurationTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestBusinessLogic()
        {
            var inMemorySettings = new Dictionary<string, string> {{"TEST_VAR", "VALUE"}};
            
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var serviceConfiguration = new ServiceConfiguration(configuration);

            var result = serviceConfiguration.GetConfigurationValue(IServiceConfiguration.ConfigurationVariables.TEST_VAR);

            Assert.That(result, Is.EqualTo("VALUE"));
        }

        [Test]
        public void TestMissingVariableThrowsException()
        {
            var inMemorySettings = new Dictionary<string, string> {{"NOT_FOUND", "VALUE"}};
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var exception = Assert.Throws<UnsetEnvironmentVariableException>(() => new ServiceConfiguration(configuration));

            Assert.That(exception.Message, Is.EqualTo("Environment variable 'TEST_VAR' not set."));
        }
    }
}
