namespace WebApi.Startup.Extensions;

public static class AppSettingsExtensions
{
    public static WebApplicationBuilder AddAppSettings(this WebApplicationBuilder builder)
    {
        builder.Configuration
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
            .AddEnvironmentVariables();
        return builder;
    }
}