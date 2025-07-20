using WebApi.Domain.Entities.Base;

namespace WebApi.Domain.Entities;

/// <summary>
/// Represents a blog post.
/// </summary>
public class PostEntity : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public virtual UserEntity Author { get; set; } = null!;
    public long AuthorId { get; set; }

    /// <summary>
    /// Tags associated with this post.
    /// </summary>
    public virtual ICollection<TagEntity> Tags { get; set; } = new List<TagEntity>();

    /// <summary>
    /// Comments made on this post.
    /// </summary>
    public virtual ICollection<CommentEntity> Comments { get; set; } = new List<CommentEntity>();
}