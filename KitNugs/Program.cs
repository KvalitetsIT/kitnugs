using System.Text.Json;
using System.Text.Json.Serialization;
using KitNugs.Configuration;
using KitNugs.Logging;
using KitNugs.Repository;
using KitNugs.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Prometheus;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Wire up configuration
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddOptions<ServiceConfiguration>()
    .Bind(builder.Configuration)
    .ValidateDataAnnotations()
    .ValidateOnStart();

// Configure logging - we use serilog.
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container.
builder.Services.AddScoped<IHelloService, HelloService>();
builder.Services.AddScoped<ISessionIdAccessor, DefaultSessionIdAccessor>();

builder.Services.AddHttpContextAccessor();

// Configure database
var connectionString = builder.Configuration.GetConnectionString("db");

builder.Services.AddDbContextPool<AppDbContext>(
    dbContextOptions => dbContextOptions
        .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
        // The following three options help with debugging, but should
        // be changed or removed for production.
        //.LogTo(Console.WriteLine, LogLevel.Information)
        //.EnableSensitiveDataLogging()
        //.EnableDetailedErrors()
);

// Add controllers
builder.Services.AddControllers().AddNewtonsoftJson(opts =>
{
    opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    opts.SerializerSettings.Converters.Add(new StringEnumConverter
    {
        NamingStrategy = new CamelCaseNamingStrategy()
    });
});

// Enable Swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

// Setup health checks and Prometheus endpoint
builder.Services.AddHealthChecks()
                .AddDbContextCheck<AppDbContext>()
                .ForwardToPrometheus();

var app = builder.Build();

app.UseMiddleware<LogHeaderMiddleware>();

app.UseHttpMetrics();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

// Map controllers
app.MapControllers();

// Ensure health endpoint and Prometheus only respond on port 8081
app.UseHealthChecks("/healthz", 8081);
app.UseMetricServer(8081, "/metrics");

using (var scope = app.Services.CreateScope())
{
    using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.Run();


public partial class Program { }
