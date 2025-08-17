using Serilog;
using Serilog.Events;

namespace WebApi.Startup.Extensions;

/// <summary>
///     Extensions for configuring Serilog in the application.
/// </summary>
public static class SerilogExtensions
{
    /// <summary>
    ///    Adds Serilog to the web application builder for logging.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton(Log.Logger);
        builder.Host.UseSerilog((context, services, configuration) =>
        {
            configuration
                .Configure()
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services);
        });
        return builder;
    }


    /// <summary>
    ///    Configures Serilog for the application.
    /// </summary>
    public static LoggerConfiguration Configure(this LoggerConfiguration loggerConfiguration)
    {
        const string logFormat = "[{Timestamp:o}] [{Level}] [T-{TraceId}] {Message}{NewLine}{Exception}";


        return loggerConfiguration
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
            .MinimumLevel.Override("System", LogEventLevel.Information)
            .MinimumLevel.Is(LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Async(option => { option.Console(LogEventLevel.Information, logFormat); })
            .WriteTo.OpenTelemetry();
    }
}