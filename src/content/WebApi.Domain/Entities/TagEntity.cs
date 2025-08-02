using WebApi.Domain.Entities.Base;

namespace WebApi.Domain.Entities;

/// <summary>
/// Represents a tag for categorizing posts
/// </summary>
public class TagEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Posts associated with this tag.
    /// </summary>
    public virtual ICollection<PostEntity> Posts { get; set; } = new List<PostEntity>();
}