namespace WebApi.Application.DTOs.Tag;

public record TagResponseDto
{
    public long Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
};