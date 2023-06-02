using KitNugs.Configuration;
using KitNugs.Logging;
using KitNugs.Repository;
using KitNugs.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Prometheus;
using Serilog;

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
Console.WriteLine(connectionString);
// Replace 'YourDbContext' with the name of your own DbContext derived class.
builder.Services.AddDbContextPool<IAppDbContext, AppDbContext>(
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
                .AddDbContextCheck<AppDbContext>()
                .ForwardToPrometheus();

var app = builder.Build();

app.UseMiddleware<LogHeaderMiddleware>();


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
    // Ensure all env variables is set.
    scope.ServiceProvider.GetRequiredService<IServiceConfiguration>();

    using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
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