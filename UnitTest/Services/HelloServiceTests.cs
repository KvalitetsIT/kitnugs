using KitNugs.Configuration;
using KitNugs.Repository;
using KitNugs.Services;
using Microsoft.EntityFrameworkCore;
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

            var helloTableDbSet = Substitute.For<DbSet<HelloTable>, IQueryable<HelloTable>>();
            var appDb = Substitute.For<IAppDbContext>();
            appDb.HelloTable.Returns(helloTableDbSet);

            helloService = new HelloService(serviceConfiguration, logger, appDb);
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