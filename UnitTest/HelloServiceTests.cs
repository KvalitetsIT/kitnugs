using KitNugs.Services;

namespace UnitTest
{
    public class HelloServiceTests
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
            var input = "my name";
            var result = helloService.BusinessLogic(input);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(input));
            Assert.That(result.DayOfWeek, Is.EqualTo(DateTime.Today.DayOfWeek.ToString()));
        }
    }
}