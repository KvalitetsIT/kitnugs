using Microsoft.Extensions.Logging;

namespace IntegrationTest
{
    public abstract class AbstractIntegrationTest
    {
        string dbUrl;
        static AbstractIntegrationTest()
        {
            Console.WriteLine("I am static");
            var builder = new Testcontainers.MariaDb.MariaDbBuilder()
                .WithUsername("hellouser")
                .WithPassword("secret1234");

            var x = builder.Build();
            x.StartAsync().Wait();

            var y = x.GetConnectionString();
            Console.WriteLine(y);
        }
    }
}