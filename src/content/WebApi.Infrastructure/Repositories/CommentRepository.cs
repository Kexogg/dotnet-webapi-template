using WebApi.Application.Repositories;
using WebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using WebApi.Infrastructure.Persist;

namespace WebApi.Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly DatabaseContext _context;

    public CommentRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<CommentEntity> CreateCommentAsync(CommentEntity comment)
    {
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
        return comment;
    }

    public async Task<List<CommentEntity>> GetCommentsByPostIdAsync(long postId, int page = 1, int pageSize = 10)
    {
        return await _context.Comments
            .Where(c => c.PostId == postId && !c.IsDeleted)
            .Include(c => c.User)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<List<CommentEntity>> GetCommentsByUserIdAsync(long userId, int page = 1, int pageSize = 10)
    {
        return await _context.Comments
            .Where(c => c.UserId == userId && !c.IsDeleted)
            .Include(c => c.User)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<CommentEntity> UpdateCommentAsync(long id, CommentEntity comment)
    {
        var existing = await _context.Comments.FindAsync(id);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Comment with id {id} not found.");
        }

        existing.Content = comment.Content;
        _context.Comments.Update(existing);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task DeleteCommentAsync(long id)
    {
        var existing = await _context.Comments.FindAsync(id);
        if (existing is { IsDeleted: false })
        {
            existing.IsDeleted = true;
            existing.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }
}