using WebApi.Domain.Enums;

namespace WebApi.Application.DTOs.User;

public record UserLoginResponseDto
{
    public required long Id { get; init; }
    public required string Name { get; init; } = string.Empty;
    public required string Email { get; init; } = string.Empty;

    public required Role Role { get; init; } = Role.User;
};