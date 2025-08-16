using Microsoft.EntityFrameworkCore;
using WebApi.Application.Repositories;
using WebApi.Domain.Entities;
using WebApi.Infrastructure.Persist;

namespace WebApi.Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly DatabaseContext _context;

    public PostRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<PostEntity> CreatePostAsync(PostEntity post)
    {
        await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();
        return post;
    }

    public async Task<PostEntity?> GetPostByIdAsync(long id)
    {
        return await _context.Posts
            .Include(p => p.Author)
            .Include(p => p.Tags)
            .Include(p => p.Comments)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<PostEntity>> GetPostsByUserIdAsync(long userId)
    {
        return await _context.Posts
            .Where(p => p.AuthorId == userId)
            .Include(p => p.Author)
            .Include(p => p.Tags)
            .Include(p => p.Comments)
            .ToListAsync();
    }

    public async Task<List<PostEntity>> GetPostsAsync(int page = 1, int pageSize = 10, string? searchTerm = null, long? tagId = null)
    {
        var query = _context.Posts
            .Include(p => p.Author)
            .Include(p => p.Tags)
            .Include(p => p.Comments)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(p => p.Title.Contains(searchTerm) || p.Content.Contains(searchTerm));
        }

        if (tagId.HasValue)
        {
            query = query.Where(p => p.Tags.Any(t => t.Id == tagId.Value));
        }

        return await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<PostEntity> UpdatePostAsync(long id, PostEntity post)
    {
        var existing = await _context.Posts
            .Include(p => p.Tags)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Post with id {id} not found.");
        }

        existing.Title = post.Title;
        existing.Content = post.Content;
        existing.Tags = post.Tags;

        _context.Posts.Update(existing);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task DeletePostAsync(long id)
    {
        var existing = await _context.Posts.FindAsync(id);
        if (existing != null)
        {
            _context.Posts.Remove(existing);
            await _context.SaveChangesAsync();
        }
    }
}