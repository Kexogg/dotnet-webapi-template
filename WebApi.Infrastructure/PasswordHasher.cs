using Microsoft.AspNetCore.Identity;
using WebApi.Domain.Entities;

namespace WebApi.Infrastructure;

public class PasswordHasher
{
    private readonly PasswordHasher<UserEntity> _inner = new();

    public string HashPassword(string password)
    {
        return _inner.HashPassword(new UserEntity(), password);
    }

    public bool VerifyPassword(string hashedPassword, string providedPassword)
    {
        var result = _inner.VerifyHashedPassword(new UserEntity(), hashedPassword, providedPassword);
        return result == PasswordVerificationResult.Success;
    }
}