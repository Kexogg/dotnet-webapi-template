using WebApi.Domain.Entities.Base;
using WebApi.Domain.Enums;

namespace WebApi.Domain.Entities;

/// <summary>
/// Represents a user in the system
/// </summary>
public class UserEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Role Role { get; set; } = Role.User;
}