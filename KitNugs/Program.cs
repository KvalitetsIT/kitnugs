using KitNugs.Configuration;
using KitNugs.Logging;
using KitNugs.Repository;
using KitNugs.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Prometheus;
using Serilog;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

// Configure logging - we use serilog.
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container. TODO Refactor DI
builder.Services.AddScoped<IServiceConfiguration, ServiceConfiguration>();
builder.Services.AddScoped<IHelloService, HelloService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ISessionIdAccessor, DefaultSessionIdAccessor>();

// Replace with your connection string.
var connectionString = builder.Configuration.GetConnectionString("db");
// Replace with your server version and type.
// Use 'MariaDbServerVersion' for MariaDB.
// Alternatively, use 'ServerVersion.AutoDetect(connectionString)'.
// For common usages, see pull request #1233.
//var serverVersion = new MySqlServerVersion(new Version(8, 0, 31));

// Replace 'YourDbContext' with the name of your own DbContext derived class.
builder.Services.AddDbContextPool<FileName>(
    dbContextOptions => dbContextOptions
        .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
        // The following three options help with debugging, but should
        // be changed or removed for production.
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
);

builder.Services.AddControllers();

// Enable NSwag
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerDocument();

// Setup health checks and Prometheus endpoint
builder.Services.AddHealthChecks()
                .AddCheck<SampleHealthCheck>(nameof(SampleHealthCheck))
                .ForwardToPrometheus();


var app = builder.Build();

app.UseMiddleware<LogHeaderMiddleware>();

// Ensure all env variables is set.
//app.Services.GetRequiredService<IServiceConfiguration>();

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

using (var scope = app.Services.CreateScope())
{
    using var dbContext = scope.ServiceProvider.GetRequiredService<FileName>();
    dbContext.Database.Migrate();
}

app.Run();


public sealed class SampleHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        // All is well!
        return Task.FromResult(HealthCheckResult.Healthy());
    }
}