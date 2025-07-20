using Microsoft.AspNetCore.Mvc;

namespace WebApi.Startup.Extensions;

public static class ControllersExtensions
{
    /// <summary>
    /// Adds controllers with error handling configuration to the WebApplicationBuilder.
    /// </summary>
    /// <param name="builder">
    /// The WebApplicationBuilder to add controllers to.
    /// </param>
    /// <returns>
    /// A <see cref="WebApplicationBuilder"></see> with controllers and error handling configured.
    /// </returns>
    public static WebApplicationBuilder AddControllersWithErrorHandling(this WebApplicationBuilder builder)
    {
        builder.Services
            .Configure<RouteOptions>(options => { options.LowercaseUrls = true; })
            .AddControllers()
            .AddErrorHandling();
        return builder;
    }

    /// <summary>
    ///  Adds error handling configuration to the MVC builder.
    /// </summary>
    /// <param name="builder">
    /// The MVC builder to configure error handling for.
    /// </param>
    /// <returns>
    /// <see cref="IMvcBuilder"/>
    /// </returns>
    /// <remarks>
    /// This method configures the API behavior options to handle invalid model state responses.
    /// It sets the response type to a <see cref="BadRequestObjectResult"/> with a <see cref="ValidationProblemDetails"/> object.
    /// </remarks>
    public static IMvcBuilder AddErrorHandling(this IMvcBuilder builder)
    {
        builder.ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var details = new ValidationProblemDetails(context.ModelState)
                {
                    Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                    Title = "One or more validation errors occurred.",
                    Detail = "INVALID_MODEL",
                    Status = StatusCodes.Status400BadRequest
                };
                return new BadRequestObjectResult(details)
                {
                    ContentTypes = { "application/problem+json" }
                };
            };
        });
        return builder;
    }
}