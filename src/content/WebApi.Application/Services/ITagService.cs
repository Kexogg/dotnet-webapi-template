using WebApi.Application.DTOs.Tag;

namespace WebApi.Application.Services;

/// <summary>
/// Tag-related operations service.
/// </summary>
public interface ITagService
{
    /// <summary>
    /// Creates a new tag.
    /// </summary>
    /// <param name="tag">The tag to create.</param>
    /// <returns>The created tag with detailed metadata.</returns>
    Task<TagResponseRichDto> CreateTagAsync(TagRequestDto tag);

    /// <summary>
    /// Retrieves a tag by its ID.
    /// </summary>
    /// <param name="id">The ID of the tag.</param>
    /// <returns>The tag with detailed metadata, or null if not found.</returns>
    Task<TagResponseRichDto?> GetTagByIdAsync(long id);

    /// <summary>
    /// Retrieves all tags with optional pagination.
    /// </summary>
    /// <param name="page">The page number (starting from 1).</param>
    /// <param name="pageSize">The number of tags per page.</param>
    /// <returns>A list of tag summaries.</returns>
    Task<List<TagResponseDto>> GetAllTagsAsync(int page = 1, int pageSize = 10);

    /// <summary>
    /// Updates an existing tag.
    /// </summary>
    /// <param name="id">The ID of the tag to update.</param>
    /// <param name="tag">The tag data to update.</param>
    /// <returns>The updated tag summary.</returns>
    Task<TagResponseDto> UpdateTagAsync(long id, TagRequestDto tag);

    /// <summary>
    /// Deletes a tag by its ID.
    /// </summary>
    /// <param name="id">The ID of the tag to delete.</param>
    Task DeleteTagAsync(long id);
}
