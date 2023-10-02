﻿using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Networks;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace IntegrationTest
{
    public abstract class AbstractIntegrationTest
    {
        protected static readonly ServiceClient client;
        protected static int servicePort = 8080;
        private static string _connectionString;

        static AbstractIntegrationTest()
        {
            // Create network
            var network = new NetworkBuilder().Build();

            StartDatabase(network);

            HttpClient? httpClient;
            if (Debugger.IsAttached)
            {
                Environment.SetEnvironmentVariable("ConnectionStrings__db", _connectionString);
                Environment.SetEnvironmentVariable("TEST_VAR", "TEST_VARIABLE");

                var server = new WebApplicationFactory<Program>().Server;
                httpClient = server.CreateClient();
            }
            else
            {
                BuildAndStartService(network);
                httpClient = new HttpClient();
            }

            httpClient.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

            client = new ServiceClient(httpClient)
            {
                BaseUrl = $"http://localhost:{servicePort}"
            };
        }

        private static void BuildAndStartService(INetwork network)
        {
            var image = new ImageFromDockerfileBuilder()
              .WithDockerfileDirectory(CommonDirectoryPath.GetSolutionDirectory(), string.Empty)
              .WithDockerfile("KitNugs/Dockerfile")
              .WithName("kvalitetsit/kitnugs")
              .WithCleanUp(false)
              .Build();

            image.CreateAsync()
                .Wait();

            var service = new ContainerBuilder()
                .WithImage("kvalitetsit/kitnugs:latest")
                .WithPortBinding(8080, true)
                .WithPortBinding(8081, true)
                .WithName("service-qa")
                .WithNetwork(network)
                .WithEnvironment("TEST_VAR", "TEST_VARIABLE")
                .WithEnvironment("ConnectionStrings__db", "server=db-qa,3306;user=hellouser;password=secret1234;database=hellodb")
                .WithWaitStrategy(Wait.ForUnixContainer().UntilHttpRequestIsSucceeded(r => r.ForPath("/healthz").ForPort(8081)))
                .Build();

            service.StartAsync()
                .Wait();

            servicePort = service.GetMappedPublicPort(8080);
        }

        private static void StartDatabase(INetwork network)
        {
            // Create and start database container
            var db = new Testcontainers.MariaDb.MariaDbBuilder()
                .WithUsername("hellouser")
                .WithNetwork(network)
                .WithName("db-qa")
                .WithPassword("secret1234")
                .WithDatabase("hellodb")
                .Build();

            db.StartAsync()
                .Wait();

            _connectionString = db.GetConnectionString();
        }
    }
}