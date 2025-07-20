using WebApi.Domain.Enums;

namespace WebApi.Application.DTOs.User;

public record UserResponseRichDto
{
    public required long Id { get; init; }
    public required string Name { get; init; } = string.Empty;
    public required string Email { get; init; } = string.Empty;
    public required Role Role { get; init; } = Role.User;
    public required DateTime CreatedAt { get; init; }
    public required DateTime UpdatedAt { get; init; }
}