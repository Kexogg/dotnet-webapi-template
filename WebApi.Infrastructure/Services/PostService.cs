using WebApi.Application.DTOs.Post;
using WebApi.Application.Services;
using WebApi.Domain.Entities;
using WebApi.Application.Repositories;
using WebApi.Domain.Exceptions;

namespace WebApi.Infrastructure.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly ITagRepository _tagRepository;
    private readonly IUserRepository _userRepository;

    public PostService(IPostRepository postRepository, ITagRepository tagRepository, IUserRepository userRepository)
    {
        _postRepository = postRepository;
        _tagRepository = tagRepository;
        _userRepository = userRepository;
    }

    public async Task<PostResponseDto> CreatePostAsync(long userId, PostRequestDto post)
    {
        if (!await _userRepository.UserExistsAsync(userId))
            throw new WebNotFoundException($"User with id {userId} not found.");

        var tags = new List<TagEntity>();
        foreach (var tagId in post.TagIds)
        {
            var tag = await _tagRepository.GetTagByIdAsync(tagId);
            if (tag == null)
                throw new WebNotFoundException($"Tag with id {tagId} not found.");
            tags.Add(tag);
        }

        var entity = post.ToEntity(tags, userId);
        var created = await _postRepository.CreatePostAsync(entity);
        return created.ToResponseDto();
    }

    public async Task<PostResponseDto?> GetPostByIdAsync(long id)
    {
        var entity = await _postRepository.GetPostByIdAsync(id);
        return entity?.ToResponseDto();
    }

    public async Task<List<PostResponseDto>?> GetPostsByUserIdAsync(long userId)
    {
        var entities = await _postRepository.GetPostsByUserIdAsync(userId);
        return entities.Select(e => e.ToResponseDto()).ToList();
    }

    public async Task<List<PostResponseDto>> GetPostsAsync(int page = 1, int pageSize = 10, string? searchTerm = null, long? tagId = null)
    {
        var entities = await _postRepository.GetPostsAsync(page, pageSize, searchTerm, tagId);
        return entities.Select(e => e.ToResponseDto()).ToList();
    }

    public async Task<PostResponseDto> UpdatePostAsync(long id, PostRequestDto post)
    {
        var tags = new List<TagEntity>();
        foreach (var tagId in post.TagIds)
        {
            var tag = await _tagRepository.GetTagByIdAsync(tagId);
            if (tag == null)
                throw new WebNotFoundException($"Tag with id {tagId} not found.");
            tags.Add(tag);
        }

        var toUpdate = new PostEntity
        {
            Title = post.Title,
            Content = post.Content,
            Tags = tags
        };
        var updated = await _postRepository.UpdatePostAsync(id, toUpdate);
        return updated.ToResponseDto();
    }

    public async Task DeletePostAsync(long id)
    {
        await _postRepository.DeletePostAsync(id);
    }
}