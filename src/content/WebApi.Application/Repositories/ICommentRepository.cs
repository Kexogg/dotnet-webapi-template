using WebApi.Domain.Entities;

namespace WebApi.Application.Repositories;

/// <summary>
/// Interface for comment-related repository operations.
/// </summary>
public interface ICommentRepository
{
    /// <summary>
    /// Creates a new comment entity.
    /// </summary>
    /// <param name="comment">The comment entity to create.</param>
    Task<CommentEntity> CreateCommentAsync(CommentEntity comment);

    /// <summary>
    /// Retrieves comments for a specific post with pagination.
    /// </summary>
    /// <param name="postId">The ID of the post.</param>
    /// <param name="page">The page number (starting from 1).</param>
    /// <param name="pageSize">The number of comments per page.</param>
    Task<List<CommentEntity>> GetCommentsByPostIdAsync(long postId, int page = 1, int pageSize = 10);

    /// <summary>
    /// Retrieves comments created by a specific user with pagination.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="page">The page number.</param>
    /// <param name="pageSize">The number of comments per page.</param>
    Task<List<CommentEntity>> GetCommentsByUserIdAsync(long userId, int page = 1, int pageSize = 10);

    /// <summary>
    /// Updates an existing comment entity.
    /// </summary>
    /// <param name="id">The ID of the comment to update.</param>
    /// <param name="comment">The comment entity with updated values.</param>
    Task<CommentEntity> UpdateCommentAsync(long id, CommentEntity comment);

    /// <summary>
    /// Deletes a comment by its ID.
    /// </summary>
    /// <param name="id">The ID of the comment to delete.</param>
    Task DeleteCommentAsync(long id);
}
