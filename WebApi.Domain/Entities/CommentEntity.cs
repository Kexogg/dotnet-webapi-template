using WebApi.Domain.Entities.Base;

namespace WebApi.Domain.Entities;

/// <summary>
/// Represents a comment on a post.
/// </summary>
public class CommentEntity : BaseSoftDeleteEntity
{
    public string Content { get; set; } = string.Empty;
    public long PostId { get; set; }
    public virtual PostEntity Post { get; set; } = null!;
    public long UserId { get; set; }
    public virtual UserEntity User { get; set; } = null!;
}