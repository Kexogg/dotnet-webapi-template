using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using WebApi.Application.DTOs.Comment;
using WebApi.Application.Services;
using WebApi.Domain.Exceptions;

namespace WebApi.Presentation.Controllers;

[ApiController]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
[Route("comments")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet("post/{postId:long}")]
    [ProducesResponseType<List<CommentResponseDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCommentsByPostId(
        long postId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var comments = await _commentService.GetCommentsByPostIdAsync(postId, page, pageSize);
        return Ok(comments);
    }

    [HttpGet("user/{userId:long}")]
    [Authorize]
    [ProducesResponseType<List<CommentResponseDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCommentsByUserId(
        long userId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var comments = await _commentService.GetCommentsAsync(userId, page, pageSize);
        return Ok(comments);
    }

    [HttpPost("post/{postId:long}")]
    [Authorize]
    [ProducesResponseType<CommentResponseDto>(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateComment(long postId, [FromBody] CommentCreateDto request)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !long.TryParse(userIdClaim, out var userId))
        {
            throw new WebUnauthorizedException("Invalid user token");
        }

        var comment = await _commentService.CreateCommentAsync(postId, userId, request);
        return CreatedAtAction(nameof(GetCommentsByPostId),
            new { postId }, comment);
    }

    [HttpPut("{id:long}")]
    [Authorize]
    [ProducesResponseType<CommentResponseDto>(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateComment(long id, [FromBody] CommentCreateDto request)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !long.TryParse(userIdClaim, out var userId))
        {
            throw new WebUnauthorizedException("Invalid user token");
        }

        return Ok(await _commentService.UpdateCommentAsync(id, request));
    }

    [HttpDelete("{id:long}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteComment(long id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !long.TryParse(userIdClaim, out _))
        {
            throw new WebUnauthorizedException("Invalid user token");
        }

        await _commentService.DeleteCommentAsync(id);
        return NoContent();
    }
}