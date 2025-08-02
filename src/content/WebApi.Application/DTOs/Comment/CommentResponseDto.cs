using WebApi.Application.DTOs.User;

namespace WebApi.Application.DTOs.Comment;

public record CommentResponseDto
{
    public long Id { get; init; }
    public string Content { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
    public long? ParentCommentId { get; init; }
    public required UserResponseDto User { get; init; }
}