using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OpenTelemetry.Instrumentation.StackExchangeRedis;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.OpenTelemetry;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

const string serviceName = "roll-dice";

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console(LogEventLevel.Debug)
    .WriteTo.OpenTelemetry(options =>
    {
        options.Endpoint = "http://localhost:4317";
        options.Protocol = OtlpProtocol.Grpc;
    })
);

builder.Logging.AddOpenTelemetry(options =>
{
    options
        .SetResourceBuilder(
            ResourceBuilder.CreateDefault()
                .AddService(serviceName))
        .AddConsoleExporter();
});

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService(serviceName))
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation()
        .AddRedisInstrumentation()
        .AddOtlpExporter()
        .AddConsoleExporter());

builder.Services.AddRedis();

var app = builder.Build();

string HandleRollDice([FromServices]ILogger<Program> logger, string? player)
{
    var result = RollDice();

    if (string.IsNullOrEmpty(player))
    {
        logger.LogInformation("Anonymous player is rolling the dice: {result}", result);
    }
    else
    {
        logger.LogInformation("{player} is rolling the dice: {result}", player, result);
    }

    return result.ToString(CultureInfo.InvariantCulture);
}

app.MapGet("/rolldice/{player?}", HandleRollDice);

app.MapGet("/cache", async (IDistributedCache cache, [FromServices]IServiceProvider serviceProvider) =>
{
    serviceProvider.GetRequiredService<StackExchangeRedisInstrumentation>();
    
    const string key = "time";

    var value = await cache.GetStringAsync(key);
    if (value == null)
    {
        value = DateTime.UtcNow.ToString("O");
        await cache.SetStringAsync(key, value, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
        });
    }

    return value;
});

app.Run();
return;

int RollDice()
{
    return Random.Shared.Next(1, 7);
}

public static class SetupExtensions
{
    public static IServiceCollection AddRedis(this IServiceCollection services)
    {
        var connection = ConnectionMultiplexer.Connect("localhost:6379");
        services.TryAddSingleton<IConnectionMultiplexer>(connection);
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = "localhost:6379";
            options.InstanceName = "myapp:";
            options.ConnectionMultiplexerFactory = () => Task.FromResult<IConnectionMultiplexer>(connection);
        });
        
        return services;
    }
}
