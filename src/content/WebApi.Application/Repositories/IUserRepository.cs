using WebApi.Domain.Entities;

namespace WebApi.Application.Repositories;

/// <summary>
/// Interface for user-related repository operations.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Retrieves a user by their unique identifier.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve.</param>
    /// <returns>The user entity, or null if not found.</returns>
    Task<UserEntity> GetUserByIdAsync(long id);

    /// <summary>
    /// Retrieves a user by their email address.
    /// </summary>
    /// <param name="email">The email of the user to retrieve.</param>
    /// <returns>The user entity, or null if not found.</returns>
    Task<UserEntity?> GetUserByEmailAsync(string email);

    /// <summary>
    /// Retrieves all users with optional pagination.
    /// </summary>
    /// <param name="page">The page number (starting from 1).</param>
    /// <param name="pageSize">The number of users per page.</param>
    /// <returns>A list of user entities.</returns>
    Task<List<UserEntity>> GetAllUsersAsync(int page = 1, int pageSize = 10);

    /// <summary>
    /// Creates a new user entity.
    /// </summary>
    /// <param name="user">The user entity to create.</param>
    /// <returns>The created user entity.</returns>
    Task<UserEntity> CreateUserAsync(UserEntity user);

    /// <summary>
    /// Updates an existing user entity.
    /// </summary>
    /// <param name="id">The ID of the user to update.</param>
    /// <param name="user">The user entity with updated values.</param>
    /// <returns>The updated user entity.</returns>
    Task<UserEntity> UpdateUserAsync(long id, UserEntity user);

    /// <summary>
    /// Deletes a user by their unique identifier.
    /// </summary>
    /// <param name="id">The ID of the user to delete.</param>
    Task DeleteUserAsync(long id);

    /// <summary>
    /// Checks if a user exists by their unique identifier.
    /// </summary>
    /// <param name="id">The ID of the user to check.</param>
    /// <returns>True if the user exists; otherwise, false.</returns>
    Task<bool> UserExistsAsync(long id);
}