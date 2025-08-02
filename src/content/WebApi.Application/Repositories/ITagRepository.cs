using WebApi.Domain.Entities;

namespace WebApi.Application.Repositories;

/// <summary>
/// Interface for tag-related repository operations.
/// </summary>
public interface ITagRepository
{
    /// <summary>
    /// Creates a new tag entity.
    /// </summary>
    /// <param name="tag">The tag entity to create.</param>
    /// <returns>The created tag entity.</returns>
    Task<TagEntity> CreateTagAsync(TagEntity tag);

    /// <summary>
    /// Retrieves a tag entity by its ID.
    /// </summary>
    /// <param name="id">The ID of the tag to retrieve.</param>
    /// <returns>The tag entity, or null if not found.</returns>
    Task<TagEntity?> GetTagByIdAsync(long id);

    /// <summary>
    /// Retrieves all tag entities with optional pagination.
    /// </summary>
    /// <param name="page">The page number (starting from 1).</param>
    /// <param name="pageSize">The number of tags per page.</param>
    /// <returns>A list of tag entities.</returns>
    Task<List<TagEntity>> GetAllTagsAsync(int page = 1, int pageSize = 10);

    /// <summary>
    /// Updates an existing tag entity.
    /// </summary>
    /// <param name="id">The ID of the tag to update.</param>
    /// <param name="tag">The tag entity with updated values.</param>
    /// <returns>The updated tag entity.</returns>
    Task<TagEntity> UpdateTagAsync(long id, TagEntity tag);

    /// <summary>
    /// Deletes a tag by its ID.
    /// </summary>
    /// <param name="id">The ID of the tag to delete.</param>
    Task DeleteTagAsync(long id);
}
