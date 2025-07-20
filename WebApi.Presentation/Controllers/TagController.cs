using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using WebApi.Application.DTOs.Tag;
using WebApi.Application.Services;
using WebApi.Domain.Exceptions;

namespace WebApi.Presentation.Controllers;

[ApiController]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
[Route("tags")]
public class TagController : ControllerBase
{
    private readonly ITagService _tagService;

    public TagController(ITagService tagService)
    {
        _tagService = tagService;
    }

    [HttpGet]
    [ProducesResponseType<List<TagResponseDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllTags([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var tags = await _tagService.GetAllTagsAsync(page, pageSize);
        return Ok(tags);
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType<TagResponseDto>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTagById(long id)
    {
        var tag = await _tagService.GetTagByIdAsync(id);
        if (tag == null)
        {
            throw new WebNotFoundException($"Tag with id {id} not found.");
        }

        return Ok(tag);
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType<TagResponseDto>(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateTag([FromBody] TagRequestDto request)
    {
        var tag = await _tagService.CreateTagAsync(request);
        return CreatedAtAction(nameof(GetTagById), new { id = tag.Id }, tag);
    }

    [HttpPut("{id:long}")]
    [ProducesResponseType<TagResponseDto>(StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> UpdateTag(long id, [FromBody] TagRequestDto request)
    {
        var updatedTag = await _tagService.UpdateTagAsync(id, request);
        return Ok(updatedTag);
    }

    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Authorize]
    public async Task<IActionResult> DeleteTag(long id)
    {
        await _tagService.DeleteTagAsync(id);
        return NoContent();
    }
}