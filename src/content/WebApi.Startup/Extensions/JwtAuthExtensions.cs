using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Protocols.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Startup.Extensions;

public static class JwtAuthExtensions
{
    public static WebApplicationBuilder AddJwtAuth(this WebApplicationBuilder builder)
    {
        var jwtSettings = builder.Configuration.GetSection("Authentication");
        var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JWT secret key is not set");
        var issuer = jwtSettings["Issuer"] ?? throw new InvalidOperationException("JWT issuer is not set");
        var audience = jwtSettings["Audience"] ?? throw new InvalidOperationException("JWT audience is not set");

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    context.Token = context.Request.Cookies["access_token"];
                    return Task.CompletedTask;
                }
            };
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };
        });
        
        builder.Services.AddAuthorization();

        return builder;
    }
}