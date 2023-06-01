using KitNugs.Configuration;
using KitNugs.Services;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace UnitTest.Services
{
    public class HelloServiceTests
    {
        private IServiceConfiguration serviceConfiguration;
        public IHelloService helloService;

        [SetUp]
        public void Setup()
        {
            ILogger<HelloService> logger = Substitute.For<ILogger<HelloService>>();

            serviceConfiguration = Substitute.For<IServiceConfiguration>();
            serviceConfiguration.GetConfigurationValue(ConfigurationVariables.TEST_VAR).Returns("VALUE");

            helloService = new HelloService(serviceConfiguration, logger);
        }

        [Test]
        public void TestBusinessLogic()
        {

            var input = "my name";

            var result = helloService.BusinessLogic(input).Result;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(input));
            Assert.That(result.FromConfiguration, Is.EqualTo("VALUE"));
        }
    }
}