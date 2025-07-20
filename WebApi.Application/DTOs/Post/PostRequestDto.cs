namespace WebApi.Application.DTOs.Post;

public record PostRequestDto
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public List<long> TagIds { get; set; } = [];
}