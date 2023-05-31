using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Networks;

namespace IntegrationTest
{
    public abstract class AbstractIntegrationTest
    {
        protected static readonly ServiceClient client;
        protected static int servicePort;

        static AbstractIntegrationTest()
        {
            // Create network
            var network = new NetworkBuilder().Build();

            StartDatabase(network);
            BuildAndStartService(network);

            client = new ServiceClient(new HttpClient())
            {
                BaseUrl = $"http://localhost:{servicePort}"
            };


        }

        private static void BuildAndStartService(INetwork network)
        {
            var image = new ImageFromDockerfileBuilder()
              .WithDockerfileDirectory(CommonDirectoryPath.GetSolutionDirectory(), String.Empty)
              .WithDockerfile("KitNugs/Dockerfile")
              .WithName("service-qa")
              .Build();

            image.CreateAsync()
                .Wait();

            var service = new ContainerBuilder()
                .WithImage("service-qa:latest")
                .WithPortBinding(8080, true)
                .WithName("service-qa")
                .WithEnvironment("TEST_VAR", "TEST_VARIABLE")
                .Build();

            service.StartAsync()
                .Wait();

            servicePort = service.GetMappedPublicPort(8080);
        }

        private static void StartDatabase(INetwork network)
        {
            // Create and start database container
            new Testcontainers.MariaDb.MariaDbBuilder()
                .WithUsername("hellouser")
                .WithNetwork(network)
                .WithName("db")
                .WithPassword("secret1234")
                .Build()
                .StartAsync()
                .Wait();

        }
    }
}