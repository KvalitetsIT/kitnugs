using KitNugs.Services;

namespace UnitTest
{
    public class Tests
    {
        public IHelloService helloService;

        [SetUp]
        public void Setup()
        {
            helloService = new HelloService();
        }

        [Test]
        public void TestBusinessLogic()
        {
            var result = helloService.BusinessLogic();
            Assert.NotNull(result);
        }
    }
}