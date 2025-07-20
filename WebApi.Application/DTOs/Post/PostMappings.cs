using WebApi.Domain.Entities;

namespace WebApi.Application.DTOs.Post;

/// <summary>
/// Provides extension methods for mapping between Post DTOs and PostEntity.
/// </summary>
public static class PostMappings
{
    /// <summary>
    /// Maps a PostRequestDto to a PostEntity.
    /// </summary>
    public static PostEntity ToEntity(this PostRequestDto dto, IEnumerable<TagEntity> tags, long userId)
    {
        return new PostEntity
        {
            Title = dto.Title,
            Content = dto.Content,
            AuthorId = userId,
            Tags = tags.ToList()
        };
    }

    /// <summary>
    /// Maps a PostEntity to a PostResponseDto.
    /// </summary>
    public static PostResponseDto ToResponseDto(this PostEntity entity)
    {
        return new PostResponseDto
        {
            Id = entity.Id,
            Title = entity.Title,
            Content = entity.Content,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            UserId = entity.AuthorId,
            UserName = entity.Author?.Name ?? string.Empty,
            Tags = entity.Tags?.Select(t => t.Name).ToList() ?? new List<string>(),
            CommentCount = entity.Comments?.Count ?? 0
        };
    }
}
