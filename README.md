# 🔴🔴 REPOSITORIET ER ARKIVERET! 🔴🔴
__Dette er sket eftersom det er flyttet til Azure. Det kan findes her [https://novaxsoftware.visualstudio.com/Service%20Hub/_git/keycloak-wrapper-api](https://novaxsoftware.visualstudio.com/Service%20Hub/_git/kitnugs)__


# kitnugs

Template repository showing how to be a good .NET application in a k8s cluster.

## A good citizen

Below is a set of recommendations for being a good service. The recommendations are not tied to a specific language or framework.

1. Configuration through environment variables.
2. Expose readiness endpoint
3. Expose endpoint that Prometheus can scrape
4. Be stateless
5. Support multiple instances
6. Always be in a releasable state
7. Automate build and deployment.

Some of above recommendations are heavily inspired by [https://12factor.net/](https://12factor.net/). It is recommended 
read [https://12factor.net/](https://12factor.net/) for more inspiration and further details. Some points go 
further than just being a good service and also touches areas like operations.

## Requirements

Below tools is required to work with the solution. 

- EF Core for database migrations. Can be installed with `dotnet tool install --global dotnet-ef`
- .NET 7.0
- Docker
- It is recommended to use Visual Studio 2022. Visual Studio Code can be used as well. 

## Building blocks

The service uses a few different NuGet packages out of the box. 
- Serilog is used for logging - [https://serilog.net/](https://serilog.net/)
- NSwag is used for for two things - [https://github.com/RicoSuter/NSwag](https://github.com/RicoSuter/NSwag)
    - Generating abstract controller classes based on OpenAPI specification
    - Generating clients used in integration test. 
- Entity Framework Core - [https://learn.microsoft.com/en-us/ef/core/](https://learn.microsoft.com/en-us/ef/core/)
- Pomelo.EntityFrameworkCore.MySql provides both MariaDB and MySQL driver for EF Core - [https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql)
- EF Core HealthChecks is used to provide EF Core health check probe - [https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-7.0](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-7.0)
- prometheus-net is used to generate Prometheus scrape endpoint - [https://github.com/prometheus-net/prometheus-net](https://github.com/prometheus-net/prometheus-net)

## Getting started

Click "Use this template" in Github. After new repository have been created clone the solution and open it in Visual Studio. Finally rename solution, namespaces etc. 

## Endpoints

### Service

The service is listening for connections on port 8080.

Health endpoint is listening on port 8081. This is used for Prometheus scraping and health endpoints. 

Prometheus scrape endpoint: `http://localhost:8081/metrics`  
Health URL that can be used for readiness probe etc: `http://localhost:8081/healthz`

### Documentation

OpenAPI documentation is exposed through Swagger if the service is started in Development mode. This is done by settings the 
environment variable `ASPNETCORE_ENVIRONMENT` to `Development`. 

## Databases

The service is using EF Core as database framework. Database migrations is handlded by the service during startup. 

Detailed documentation and guide for EF core can be found at https://learn.microsoft.com/en-us/ef/core/. 

### Initial migration

When service is first created an initial migration must be created. It should created after initial model have been created. 

Initial migration is done with the command `dotnet ef migrations add InitialCreate`.

### New migration

When a schema change is required a new migration must be added. 

After the model have been changed in the code a new migration is added with the command `dotnet ef migrations add <migration name>`. Consider naming your migration something meaningful. 

## Configuration

Configuration is prefarably done through environment variables.

| Environment variable              | Description                                                                                          | Required |
|-----------------------------------|------------------------------------------------------------------------------------------------------|----------|
| Serilog__MinimumLevel__Default    | Default log level. Defaults to Information.                                                          | No      |
| ConnectionStrings__db             | Database connection string. Example: `server=db;user=hellouser;password=secret1234;database=hellodb` | Yes      |
| TEST_VAR                          | Variable used for demonstrating on how to use environment variables in the service.                  | Yes      |
