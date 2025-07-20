using WebApi.Application.DTOs.User;

namespace WebApi.Application.Services;

/// <summary>
/// User-related operations service.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="user">The user to register.</param>
    /// <returns>The registered user.</returns>
    Task<UserResponseRichDto> RegisterUserAsync(UserRegistrationDto user);

    /// <summary>
    /// Gets a user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <returns>The user with the specified ID.</returns>
    Task<UserResponseRichDto?> GetUserByIdAsync(long id);

    /// <summary>
    /// Gets a user by their email.
    /// </summary>
    /// <param name="email">The email of the user.</param>
    /// <returns>The user with the specified email.</returns>
    Task<UserResponseRichDto?> GetUserByEmailAsync(string email);

    /// <summary>
    /// Gets all users.
    /// </summary>
    /// <param name="page">The page number to retrieve.</param>
    /// <param name="pageSize">The number of users per page.</param>
    Task<List<UserResponseDto>> GetAllUsersAsync(int page = 1, int pageSize = 10);

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="id">The ID of the user to update.</param>
    /// <param name="user">The updated user information.</param>
    /// <returns>The updated user.</returns>
    Task<UserResponseDto> UpdateUserAsync(long id, UserUpdateDto user);

    /// <summary>
    /// Deletes a user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user to delete.</param>
    Task DeleteUserAsync(long id);

    /// <summary>
    /// Verifies password.
    /// </summary>
    /// <param name="email">The email of the user.</param>
    /// <param name="password">The password to verify.</param>
    /// <returns></returns>
    Task<bool> VerifyUserPasswordAsync(string email, string password);
}