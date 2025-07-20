using WebApi.Domain.Entities;
using WebApi.Application.DTOs.User;

namespace WebApi.Application.DTOs.Comment;

public static class CommentMappings
{
    public static CommentEntity ToEntity(this CommentCreateDto dto, long postId, long userId)
    {
        return new CommentEntity
        {
            Content = dto.Content,
            PostId = postId,
            UserId = userId
        };
    }

    public static CommentResponseDto ToDto(this CommentEntity entity)
    {
        return new CommentResponseDto
        {
            Id = entity.Id,
            Content = entity.Content,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            ParentCommentId = null,
            User = new UserResponseDto { Id = entity.User.Id, Name = entity.User.Name }
        };
    }
}
