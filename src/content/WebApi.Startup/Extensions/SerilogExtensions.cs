using Serilog;
using Serilog.Events;

namespace WebApi.Startup.Extensions;

/// <summary>
///     Extensions for configuring Serilog in the application.
/// </summary>
public static class SerilogExtensions
{
    public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder)
    {
        builder.Services.AddSerilogServices();
        builder.Host.ConfigureSerilog();
        return builder;
    }

    /// <summary>
    ///     Adds Serilog services to the service collection.
    /// </summary>
    /// <param name="services">
    ///     The service collection to add Serilog services to.
    /// </param>
    public static IServiceCollection AddSerilogServices(this IServiceCollection services)
    {
        return services
            .AddSingleton(Log.Logger);
    }

    /// <summary>
    ///     Configures Serilog for the application.
    /// </summary>
    /// <param name="hostBuilder">
    ///     The host builder to configure Serilog for.
    /// </param>
    public static IHostBuilder ConfigureSerilog(this IHostBuilder hostBuilder)
    {
        return hostBuilder.UseSerilog((context, services, configuration) =>
        {
            configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .GetConfiguration();
        });
    }

    private static void GetConfiguration(this LoggerConfiguration loggerConfiguration)
    {
        const string logFormat =
            "[{Timestamp:yyyy.MM.dd HH:mm:ss:ms}] [{Level}] [T-{TraceId}] {Message}{NewLine}{Exception}";

        loggerConfiguration
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
            .MinimumLevel.Override("System", LogEventLevel.Information)
            .MinimumLevel.Is(LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Async(option => { option.Console(LogEventLevel.Information, logFormat); });
    }
}