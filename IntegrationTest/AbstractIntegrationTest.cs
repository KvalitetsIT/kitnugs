using DotNet.Testcontainers.Builders;

namespace IntegrationTest
{
    public abstract class AbstractIntegrationTest
    {
        string dbUrl;
        static AbstractIntegrationTest()
        {
            // Create network
            var network = new NetworkBuilder().Build();

            Console.WriteLine("I am static");
            var builder = new Testcontainers.MariaDb.MariaDbBuilder()
                .WithUsername("hellouser")
                .WithNetwork(network)
                .WithName("db")
                .WithPassword("secret1234");

            var x = builder.Build();
            x.StartAsync().Wait();

            var z = CommonDirectoryPath.GetSolutionDirectory();

            var futureImage = new ImageFromDockerfileBuilder()
              .WithDockerfileDirectory(CommonDirectoryPath.GetSolutionDirectory(), String.Empty)
              .WithDockerfile("Dockerfile")
              .Build();

            futureImage.CreateAsync().Wait();

            var service = new ContainerBuilder()
                .WithImage(futureImage)
                .Build();

            service.StartAsync().Wait();


            var y = x.GetConnectionString();
            Console.WriteLine(y);
        }
    }
}