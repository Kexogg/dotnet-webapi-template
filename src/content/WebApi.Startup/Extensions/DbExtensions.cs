using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Entities;
using WebApi.Domain.Enums;
using WebApi.Infrastructure;
using WebApi.Infrastructure.Persistence;

namespace WebApi.Startup.Extensions;

public static class DbExtensions
{
    public static async Task MigrateDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        await context.Database.MigrateAsync();
        if (!await context.Users.AnyAsync())
        {
            var bootstrap = app.Configuration.GetSection("Bootstrap");
            var passwordHasher = new PasswordHasher();
            await context.Users.AddAsync(new UserEntity
            {
                Id = 1,
                Email = bootstrap["Email"] ?? throw new InvalidOperationException(
                    "Admin email is not set in Bootstrap configuration"),
                Password = passwordHasher.HashPassword(bootstrap["Password"] ?? throw new InvalidOperationException(
                        "Admin password is not set in Bootstrap configuration")),
                Name = bootstrap["Name"] ?? "Admin",
                Role = Role.Admin
            });
            await context.SaveChangesAsync();
        }
    }
}