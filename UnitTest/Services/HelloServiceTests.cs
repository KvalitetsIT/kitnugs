using KitNugs.Configuration;
using KitNugs.Repository;
using KitNugs.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace UnitTest.Services
{
    public class HelloServiceTests
    {
        private IOptions<ServiceConfiguration> _options = null!;
        private AppDbContext _dbContext = null!;
        private IHelloService _helloService = null!;

        [SetUp]
        public void Setup()
        {
            ILogger<HelloService> logger = Substitute.For<ILogger<HelloService>>();

            _options = Substitute.For<IOptions<ServiceConfiguration>>();
            _options.Value.Returns(new ServiceConfiguration() { TEST_VAR = "VALUE" });

            _dbContext = new TestDatabaseFixture().CreateContext();

            _helloService = new HelloService(_options, logger, _dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext?.Dispose();
        }

        [Test]
        public void TestBusinessLogic()
        {
            var input = "my name";

            var result = _helloService.BusinessLogic(input).Result;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(input));
            Assert.That(result.FromConfiguration, Is.EqualTo("VALUE"));
        }
    }
}