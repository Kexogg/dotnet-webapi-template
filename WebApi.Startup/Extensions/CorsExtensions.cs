using Microsoft.AspNetCore.Cors.Infrastructure;

namespace WebApi.Startup.Extensions;

public static class CorsExtensions
{
    /// <summary>
    /// Adds CORS policy to the service collection based on the environment and configuration.
    /// </summary>
    /// <param name="builder">
    ///   The web application builder to add CORS policy to.
    /// </param>
    /// <remarks>
    ///  In production mode, allows origins specified in the "Domain" configuration section.
    ///  In development mode, allows any origin that is allowed in production, but also allows loopback addresses.
    ///  </remarks>
    public static WebApplicationBuilder AddCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            var corsPolicyBuilder = new CorsPolicyBuilder();
            corsPolicyBuilder
                .WithMethods("GET", "POST", "PUT", "DELETE", "PATCH", "OPTIONS", "HEAD")
                .AllowAnyHeader()
                .AllowCredentials();

            corsPolicyBuilder.WithOrigins(builder.Configuration.GetSection("Cors:Domain").Get<string[]>() ?? []);

            if (builder.Environment.IsDevelopment())
                corsPolicyBuilder.SetIsOriginAllowed(origin => new Uri(origin).IsLoopback);

            options.AddDefaultPolicy(corsPolicyBuilder.Build());
        });
        return builder;
    }
}