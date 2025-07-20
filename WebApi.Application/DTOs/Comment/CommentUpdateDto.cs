namespace WebApi.Application.DTOs.Comment;

public record CommentUpdateDto
{
    public string Content { get; init; } = string.Empty;
}