namespace WebApi.Application.DTOs.User;

public record UserResponseDto
{
    public long Id { get; init; }
    public string Name { get; init; } = string.Empty;
}