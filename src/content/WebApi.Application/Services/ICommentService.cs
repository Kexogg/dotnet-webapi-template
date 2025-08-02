using WebApi.Application.DTOs.Comment;

namespace WebApi.Application.Services;

/// <summary>
/// Comment-related operations service.
/// </summary>
public interface ICommentService
{
    /// <summary>
    /// Creates a new comment.
    /// </summary>
    /// <param name="postId"> The ID of the post to which the comment belongs. </param>
    /// <param name="userId">The ID of the user creating the comment.</param>
    /// <param name="comment">The comment to create.</param>
    /// <returns>The created comment.</returns>
    Task<CommentResponseDto> CreateCommentAsync(long postId, long userId, CommentCreateDto comment);

    /// <summary>
    /// Gets all comments for a specific post by its ID.
    /// </summary>
    /// <param name="postId">The ID of the post whose comments to retrieve.</param>
    /// <param name="page">The page number to retrieve.</param>
    /// <param name="pageSize">The number of comments per page.</param>
    /// <returns>A list of comments for the specified post.</returns>
    Task<List<CommentResponseDto>> GetCommentsByPostIdAsync(long postId, int page = 1, int pageSize = 10);

    /// <summary>
    /// Gets all comments by a specific user ID.
    /// </summary>
    /// <param name="userId">The ID of the user whose comments to retrieve.</param>
    /// <param name="page">The page number to retrieve.</param>
    /// <param name="pageSize">The number of comments per page.</param>
    /// <returns>A list of comments created by the specified user.</returns>
    Task<List<CommentResponseDto>> GetCommentsAsync(long userId, int page = 1, int pageSize = 10);

    /// <summary>
    /// Updates an existing comment.
    /// </summary>
    /// <param name="id">The id of the comment.</param>
    /// <param name="comment">The comment to update.</param>
    /// <returns>The updated comment.</returns>
    Task<CommentResponseDto> UpdateCommentAsync(long id, CommentCreateDto comment);

    /// <summary>
    /// Deletes a comment by its ID.
    /// </summary>
    /// <param name="id">The ID of the comment to delete.</param>
    Task DeleteCommentAsync(long id);
}