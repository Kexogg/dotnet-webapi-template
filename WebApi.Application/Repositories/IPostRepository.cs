using WebApi.Domain.Entities;

namespace WebApi.Application.Repositories;

/// <summary>
/// Interface for posts-related repository operations.
/// </summary>
public interface IPostRepository
{
    /// <summary>
    /// Creates a new post entity.
    /// </summary>
    /// <param name="post">The post entity to create.</param>
    Task<PostEntity> CreatePostAsync(PostEntity post);

    /// <summary>
    /// Retrieves a post entity by its ID.
    /// </summary>
    /// <param name="id">The ID of the post to retrieve.</param>
    Task<PostEntity?> GetPostByIdAsync(long id);

    /// <summary>
    /// Retrieves all posts created by a specific user.
    /// </summary>
    /// <param name="userId">The user ID whose posts are to be retrieved.</param>
    Task<List<PostEntity>> GetPostsByUserIdAsync(long userId);

    /// <summary>
    /// Retrieves posts with optional pagination, search, and tag filtering.
    /// </summary>
    /// <param name="page">The page number (starting from 1).</param>
    /// <param name="pageSize">The number of posts per page.</param>
    /// <param name="searchTerm">Optional search term to filter by title or content.</param>
    /// <param name="tagId">Optional tag ID to filter posts by tag.</param>
    Task<List<PostEntity>> GetPostsAsync(int page = 1, int pageSize = 10, string? searchTerm = null, long? tagId = null);

    /// <summary>
    /// Updates an existing post entity.
    /// </summary>
    /// <param name="id">The ID of the post to update.</param>
    /// <param name="post">The post entity with updated values.</param>
    Task<PostEntity> UpdatePostAsync(long id, PostEntity post);

    /// <summary>
    /// Deletes a post by its ID.
    /// </summary>
    /// <param name="id">The ID of the post to delete.</param>
    Task DeletePostAsync(long id);
}