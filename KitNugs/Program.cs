using KitNugs.Configuration;
using KitNugs.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. TODO Refactor DI
builder.Services.AddSingleton<IServiceConfiguration, ServiceConfiguration>();
builder.Services.AddSingleton<IHelloService, HelloService>();

builder.Configuration.AddEnvironmentVariables();
builder.Logging.AddJsonConsole();

builder.Services.AddControllers();

// Enable NSwag
builder.Services.AddSwaggerDocument();

// Setup health checks and Prometheus endpoint
builder.Services.AddHealthChecks()
                .AddCheck<SampleHealthCheck>(nameof(SampleHealthCheck))
                .ForwardToPrometheus();


var app = builder.Build();

app.UseHttpMetrics();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi3();
}

app.UseAuthorization();

// Ensure controllers only respond on port 8080
app.MapControllers()
    //.RequireHost("*:8080");
    ;
// Ensure health endpoint and Prometheus only respond on port 8081
app.MapHealthChecks("/healthz")
    .RequireHost("*:8081")
    ;
app.MapMetrics()
    .RequireHost("*:8081")
    ;

app.Run();


public sealed class SampleHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        // All is well!
        return Task.FromResult(HealthCheckResult.Healthy());
    }
}