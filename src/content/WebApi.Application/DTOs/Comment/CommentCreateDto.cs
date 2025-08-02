namespace WebApi.Application.DTOs.Comment;

public record CommentCreateDto
{
    public string Content { get; init; } = string.Empty;
    public long? ParentCommentId { get; init; }
}