using WebApi.Application.DTOs.Post;

namespace WebApi.Application.Services;

/// <summary>
/// Posts-related operations service.
/// </summary>
public interface IPostService
{
    /// <summary>
    /// Creates a new post.
    /// </summary>
    /// <param name="userId">The ID of the user creating the post.</param>
    /// <param name="post">The post to create.</param>
    /// <returns>The created post.</returns>
    Task<PostResponseDto> CreatePostAsync(long userId, PostRequestDto post);

    /// <summary>
    /// Gets a post by its ID.
    /// </summary>
    /// <param name="id">The ID of the post.</param>
    /// <returns>The post with the specified ID.</returns>
    Task<PostResponseDto?> GetPostByIdAsync(long id);

    /// <summary>
    /// Gets all posts by a specific user ID.
    /// </summary>
    /// <param name="userId">The ID of the user whose posts to retrieve.</param>
    /// <returns>A list of posts created by the specified user.</returns>
    Task<List<PostResponseDto>?> GetPostsByUserIdAsync(long userId);

    /// <summary>
    /// Gets all posts with optional pagination, search, and tag filtering.
    /// </summary>
    /// <param name="page">The page number to retrieve.</param>
    /// <param name="pageSize">The number of posts per page.</param>
    /// <param name="searchTerm">The search term to filter posts by title or content.</param>
    /// <param name="tagId">Optional tag ID to filter posts by tag.</param>
    /// <returns>A list of posts.</returns>
    Task<List<PostResponseDto>> GetPostsAsync(int page = 1, int pageSize = 10,
        string? searchTerm = null, long? tagId = null);

    /// <summary>
    /// Updates an existing post.
    /// </summary>
    /// <param name="id">The id of the post.</param>
    /// <param name="post">The post to update.</param>
    /// <returns>The updated post.</returns>
    Task<PostResponseDto> UpdatePostAsync(long id, PostRequestDto post);

    /// <summary>
    /// Deletes a post by its ID.
    /// </summary>
    /// <param name="id">The ID of the post to delete.</param>
    Task DeletePostAsync(long id);
}