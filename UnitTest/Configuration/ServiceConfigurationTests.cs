using KitNugs.Configuration;
using KitNugs.Services;
using Microsoft.Extensions.Configuration;
using NSubstitute;

namespace UnitTest.Configuration
{
    internal class ServiceConfigurationTests
    {
        private IConfiguration configuration;

        [SetUp]
        public void Setup()
        {
            configuration = Substitute.For<IConfiguration>();
        }

        [Test]
        public void TestBusinessLogic()
        {
            configuration.GetValue<string>("TEST_VAR").Returns("VALUE");

            var helloService = new ServiceConfiguration(configuration);

            var result = helloService.GetConfigurationValue(IServiceConfiguration.ConfigurationVariables.TEST_VAR);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void TestMissingVariableThrowsException()
        {
            var helloService = new ServiceConfiguration(configuration);

            var result = helloService.GetConfigurationValue(IServiceConfiguration.ConfigurationVariables.TEST_VAR);

            Assert.That(result, Is.Not.Null);
        }
    }
}
