using KitNugs.Configuration;
using KitNugs.Services;
using NSubstitute;

namespace UnitTest
{
    public class HelloServiceTests
    {
        private IServiceConfiguration serviceConfiguration;
        public IHelloService helloService;

        [SetUp]
        public void Setup()
        {
            serviceConfiguration = Substitute.For<IServiceConfiguration>();
            helloService = new HelloService(serviceConfiguration);
        }

        [Test]
        public void TestBusinessLogic()
        {
            serviceConfiguration.GetConfigurationValue(IServiceConfiguration.ConfigurationVariables.TEST_VAR).Returns("VALUE");

            var input = "my name";

            var result = helloService.BusinessLogic(input).Result;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(input));
            Assert.That(result.FromConfiguration, Is.EqualTo("VALUE"));
        }
    }
}