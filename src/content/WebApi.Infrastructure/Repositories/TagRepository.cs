using WebApi.Application.Repositories;
using WebApi.Domain.Entities;
using WebApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Exceptions;

namespace WebApi.Infrastructure.Repositories;

public class TagRepository : ITagRepository
{
    private readonly DatabaseContext _context;

    public TagRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<TagEntity> CreateTagAsync(TagEntity tag)
    {
        await _context.Tags.AddAsync(tag);
        await _context.SaveChangesAsync();
        return tag;
    }

    public async Task<TagEntity?> GetTagByIdAsync(long id)
    {
        return await _context.Tags
            .Include(t => t.Posts)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<List<TagEntity>> GetAllTagsAsync(int page = 1, int pageSize = 10)
    {
        return await _context.Tags
            .Include(t => t.Posts)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<TagEntity> UpdateTagAsync(long id, TagEntity tag)
    {
        var existing = await _context.Tags.FindAsync(id);
        if (existing == null)
        {
            throw new WebNotFoundException($"Tag with id {id} not found.");
        }

        existing.Name = tag.Name;
        existing.Description = tag.Description;
        _context.Tags.Update(existing);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task DeleteTagAsync(long id)
    {
        var existing = await _context.Tags.FindAsync(id);
        if (existing != null)
        {
            _context.Tags.Remove(existing);
            await _context.SaveChangesAsync();
        }
    }
}