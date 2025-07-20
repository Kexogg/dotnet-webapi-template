using Microsoft.AspNetCore.Mvc;
using WebApi.Domain.Exceptions;

namespace WebApi.Startup.Middlewares;

public class GlobalExceptionHandlerMiddleware
    : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = ex switch
            {
                WebBadRequestException => StatusCodes.Status400BadRequest,
                WebUnauthorizedException => StatusCodes.Status401Unauthorized,
                WebNotFoundException => StatusCodes.Status404NotFound,
                WebConflictException => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };
            var detail = ex.Message;

            if (context.Response.StatusCode == StatusCodes.Status500InternalServerError)
            {
                _logger.LogError(ex, "An unhandled exception occurred while processing the request.");
                detail = "An unexpected error occurred. Please try again later.";
            }

            var problemDetails = new ProblemDetails
            {
                Title = "An error occurred while processing your request.",
                Status = context.Response.StatusCode,
                Detail = detail,
                Instance = context.Request.Path,
                Type = GetIetfLink(context.Response.StatusCode)
            };
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }

    private static string GetIetfLink(int statusCode)
    {
        return statusCode switch
        {
            StatusCodes.Status400BadRequest => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
            StatusCodes.Status401Unauthorized => "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1",
            StatusCodes.Status404NotFound => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
            StatusCodes.Status409Conflict => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8",
            StatusCodes.Status500InternalServerError => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
            _ => "https://datatracker.ietf.org/doc/html/rfc7231"
        };
    }
}