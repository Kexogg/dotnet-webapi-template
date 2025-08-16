using WebApi.Application.Repositories;
using WebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using WebApi.Infrastructure.Persist;

namespace WebApi.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _context;

    public UserRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<UserEntity> GetUserByIdAsync(long id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            throw new KeyNotFoundException($"User with id {id} not found.");
        return user;
    }

    public async Task<UserEntity?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<List<UserEntity>> GetAllUsersAsync(int page = 1, int pageSize = 10)
    {
        return await _context.Users
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<UserEntity> CreateUserAsync(UserEntity user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<UserEntity> UpdateUserAsync(long id, UserEntity user)
    {
        var existing = await _context.Users.FindAsync(id);
        if (existing == null)
            throw new KeyNotFoundException($"User with id {id} not found.");

        existing.Name = user.Name;
        existing.Email = user.Email;
        existing.Password = user.Password;
        existing.Role = user.Role;

        _context.Users.Update(existing);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task DeleteUserAsync(long id)
    {
        var existing = await _context.Users.FindAsync(id);
        if (existing != null)
        {
            _context.Users.Remove(existing);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> UserExistsAsync(long id)
    {
        return await _context.Users.AnyAsync(u => u.Id == id);
    }
}