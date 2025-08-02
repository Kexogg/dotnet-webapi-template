using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using WebApi.Application.DTOs.Post;
using WebApi.Application.Services;
using WebApi.Domain.Enums;
using WebApi.Domain.Exceptions;

namespace WebApi.Presentation.Controllers;

[ApiController]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
[Route("posts")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpGet]
    [ProducesResponseType<List<PostResponseDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllPosts(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? searchTerm = null,
        [FromQuery] long? tagId = null)
    {
        var posts = await _postService.GetPostsAsync(page, pageSize, searchTerm, tagId);
        return Ok(posts);
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType<PostResponseDto>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPostById(long id)
    {
        var post = await _postService.GetPostByIdAsync(id);
        if (post == null)
        {
            throw new WebNotFoundException($"Post with id {id} not found.");
        }

        return Ok(post);
    }

    [HttpGet("user/{userId:long}")]
    [ProducesResponseType<List<PostResponseDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPostsByUserId(long userId)
    {
        var posts = await _postService.GetPostsByUserIdAsync(userId);
        if (posts == null)
        {
            throw new WebNotFoundException($"No posts found for user with id {userId}.");
        }

        return Ok(posts);
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType<PostResponseDto>(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreatePost([FromBody] PostRequestDto request)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !long.TryParse(userIdClaim, out var userId))
        {
            throw new WebNotFoundException("User not found.");
        }

        var post = await _postService.CreatePostAsync(userId, request);
        return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
    }

    [HttpPut("{id:long}")]
    [Authorize]
    [ProducesResponseType<PostResponseDto>(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdatePost(long id, [FromBody] PostRequestDto request)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !long.TryParse(userIdClaim, out var userId))
        {
            throw new WebUnauthorizedException("Invalid user token");
        }

        var existingPost = await _postService.GetPostByIdAsync(id);
        if (existingPost == null)
        {
            throw new WebNotFoundException("Post not found");
        }

        if (existingPost.UserId != userId && !User.IsInRole(nameof(Role.Admin)))
        {
            throw new WebForbiddenException("You do not have permission to update this post.");
        }

        var updatedPost = await _postService.UpdatePostAsync(id, request);
        return Ok(updatedPost);
    }

    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Authorize]
    public async Task<IActionResult> DeletePost(long id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !long.TryParse(userIdClaim, out var userId))
        {
            throw new WebUnauthorizedException("Invalid user token");
        }

        var existingPost = await _postService.GetPostByIdAsync(id);
        if (existingPost == null)
        {
            throw new WebNotFoundException("Post not found");
        }

        if (existingPost.UserId != userId && !User.IsInRole(nameof(Role.Admin)))
        {
            throw new WebForbiddenException("You do not have permission to delete this post.");
        }

        await _postService.DeletePostAsync(id);
        return NoContent();
    }
}