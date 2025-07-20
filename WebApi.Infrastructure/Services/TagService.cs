using WebApi.Application.DTOs.Tag;
using WebApi.Application.Repositories;
using WebApi.Application.Services;

namespace WebApi.Infrastructure.Services;

public class TagService : ITagService
{
    private readonly ITagRepository _tagRepository;

    public TagService(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task<TagResponseRichDto> CreateTagAsync(TagRequestDto tag)
    {
        var entity = tag.ToEntity();
        var created = await _tagRepository.CreateTagAsync(entity);
        return created.ToRichDto();
    }

    public async Task<TagResponseRichDto?> GetTagByIdAsync(long id)
    {
        var entity = await _tagRepository.GetTagByIdAsync(id);
        return entity?.ToRichDto();
    }

    public async Task<List<TagResponseDto>> GetAllTagsAsync(int page = 1, int pageSize = 10)
    {
        var entities = await _tagRepository.GetAllTagsAsync(page, pageSize);
        return entities.Select(e => e.ToDto()).ToList();
    }

    public async Task<TagResponseDto> UpdateTagAsync(long id, TagRequestDto tag)
    {
        var entity = tag.ToEntity();
        var updated = await _tagRepository.UpdateTagAsync(id, entity);
        return updated.ToDto();
    }

    public async Task DeleteTagAsync(long id)
    {
        await _tagRepository.DeleteTagAsync(id);
    }
}