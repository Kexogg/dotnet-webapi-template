namespace WebApi.Application.DTOs.Post;

public record PostResponseDto
{
    public long Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
    public long UserId { get; init; }
    public string UserName { get; init; } = string.Empty;
    public List<string> Tags { get; init; } = [];
    public int CommentCount { get; init; }
}