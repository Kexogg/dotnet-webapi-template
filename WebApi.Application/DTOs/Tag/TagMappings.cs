using System.Collections.Generic;
using WebApi.Domain.Entities;

namespace WebApi.Application.DTOs.Tag;

public static class TagMappings
{
    public static TagEntity ToEntity(this TagRequestDto dto)
    {
        return new TagEntity
        {
            Name = dto.Name,
            Description = dto.Description
        };
    }

    public static TagResponseDto ToDto(this TagEntity entity)
    {
        return new TagResponseDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description
        };
    }

    public static TagResponseRichDto ToRichDto(this TagEntity entity)
    {
        return new TagResponseRichDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}
