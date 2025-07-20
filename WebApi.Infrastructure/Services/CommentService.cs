using WebApi.Application.Repositories;
using WebApi.Domain.Entities;
using WebApi.Application.DTOs.Comment;
using WebApi.Application.Services;
using WebApi.Domain.Exceptions;

namespace WebApi.Infrastructure.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IPostRepository _postRepository;

    public CommentService(ICommentRepository commentRepository, IPostRepository postRepository)
    {
        _commentRepository = commentRepository;
        _postRepository = postRepository;
    }

    public async Task<CommentResponseDto> CreateCommentAsync(long postId, long userId, CommentCreateDto comment)
    {
        var post = await _postRepository.GetPostByIdAsync(postId);
        if (post == null)
            throw new WebNotFoundException($"Post with id {postId} not found.");

        var entity = comment.ToEntity(postId, userId);
        var created = await _commentRepository.CreateCommentAsync(entity);
        return created.ToDto();
    }

    public async Task<List<CommentResponseDto>> GetCommentsByPostIdAsync(long postId, int page = 1, int pageSize = 10)
    {
        var entities = await _commentRepository.GetCommentsByPostIdAsync(postId, page, pageSize);
        return entities.Select(e => e.ToDto()).ToList();
    }

    public async Task<List<CommentResponseDto>> GetCommentsAsync(long userId, int page = 1, int pageSize = 10)
    {
        var entities = await _commentRepository.GetCommentsByUserIdAsync(userId, page, pageSize);
        return entities.Select(e => e.ToDto()).ToList();
    }

    public async Task<CommentResponseDto> UpdateCommentAsync(long id, CommentCreateDto comment)
    {
        var toUpdate = new CommentEntity { Content = comment.Content };
        var updated = await _commentRepository.UpdateCommentAsync(id, toUpdate);
        return updated.ToDto();
    }

    public async Task DeleteCommentAsync(long id)
    {
        await _commentRepository.DeleteCommentAsync(id);
    }
}