using WebApi.Application.Repositories;
using WebApi.Application.DTOs.User;
using WebApi.Application.Services;
using WebApi.Domain.Exceptions;

namespace WebApi.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly PasswordHasher _passwordHasher;

    public UserService(IUserRepository userRepository, PasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserResponseRichDto> RegisterUserAsync(UserRegistrationDto user)
    {
        var existing = await _userRepository.GetUserByEmailAsync(user.Email);
        if (existing != null)
            throw new WebConflictException($"User with email {user.Email} already exists.");

        var hashed = _passwordHasher.HashPassword(user.Password);
        var entity = user.ToEntity(hashed);
        var created = await _userRepository.CreateUserAsync(entity);
        return created.ToRichDto();
    }

    public async Task<UserResponseRichDto?> GetUserByIdAsync(long id)
    {
        try
        {
            var entity = await _userRepository.GetUserByIdAsync(id);
            return entity.ToRichDto();
        }
        catch (WebNotFoundException)
        {
            return null;
        }
    }

    public async Task<UserResponseRichDto?> GetUserByEmailAsync(string email)
    {
        var entity = await _userRepository.GetUserByEmailAsync(email);
        return entity?.ToRichDto();
    }

    public async Task<List<UserResponseDto>> GetAllUsersAsync(int page = 1, int pageSize = 10)
    {
        var entities = await _userRepository.GetAllUsersAsync(page, pageSize);
        return entities.Select(e => e.ToDto()).ToList();
    }

    public async Task<UserResponseDto> UpdateUserAsync(long id, UserUpdateDto user)
    {
        var existing = await _userRepository.GetUserByIdAsync(id);
        existing.Name = user.Name;
        existing.Email = user.Email;
        var updated = await _userRepository.UpdateUserAsync(id, existing);
        return updated.ToDto();
    }

    public async Task DeleteUserAsync(long id)
    {
        await _userRepository.DeleteUserAsync(id);
    }

    public async Task<bool> VerifyUserPasswordAsync(string email, string password)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        return user != null && _passwordHasher.VerifyPassword(user.Password, password);
    }
}