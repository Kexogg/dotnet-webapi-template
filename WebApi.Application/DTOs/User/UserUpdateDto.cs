namespace WebApi.Application.DTOs.User;

public record UserUpdateDto
{
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
};