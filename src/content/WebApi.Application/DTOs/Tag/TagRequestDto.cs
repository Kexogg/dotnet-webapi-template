namespace WebApi.Application.DTOs.Tag;

public record TagRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}