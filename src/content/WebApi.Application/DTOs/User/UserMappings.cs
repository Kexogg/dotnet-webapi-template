using WebApi.Domain.Entities;
using WebApi.Application.DTOs.User;

namespace WebApi.Application.DTOs.User;

public static class UserMappings
{
    public static UserEntity ToEntity(this UserRegistrationDto dto, string hashedPassword)
    {
        return new UserEntity
        {
            Name = dto.Name,
            Email = dto.Email,
            Password = hashedPassword
        };
    }

    public static UserResponseRichDto ToRichDto(this UserEntity entity)
    {
        return new UserResponseRichDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Email = entity.Email,
            Role = entity.Role,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }

    public static UserResponseDto ToDto(this UserEntity entity)
    {
        return new UserResponseDto
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }

    public static UserLoginResponseDto ToLoginDto(this UserResponseRichDto dto)
    {
        return new UserLoginResponseDto
        {
            Id = dto.Id,
            Name = dto.Name,
            Email = dto.Email,
            Role = dto.Role
        };
    }
}
