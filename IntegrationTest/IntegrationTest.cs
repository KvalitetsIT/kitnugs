namespace IntegrationTest
{
    public class Tests : AbstractIntegrationTest
    {

        [Test]
        public void TestGetHello()
        {
            var name = Guid.NewGuid().ToString();
 
            var response = client.HelloAsync(name).Result;

            Assert.That(response, Is.Not.Null);
            Assert.That(response.Name, Is.EqualTo(name));
        }
    }
}