using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using WebApi.Application.DTOs.User;
using WebApi.Application.Services;
using WebApi.Domain.Enums;
using WebApi.Domain.Exceptions;

namespace WebApi.Presentation.Controllers;

[ApiController]
[Route("users")]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [ProducesResponseType<List<UserResponseDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllUsers([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var users = await _userService.GetAllUsersAsync(page, pageSize);
        return Ok(users);
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType<UserResponseDto>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserById(long id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
        {
            throw new WebNotFoundException($"User with id {id} not found.");
        }

        return Ok(user);
    }

    [HttpGet("profile")]
    [ProducesResponseType<UserResponseDto>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCurrentUserProfile()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !long.TryParse(userIdClaim, out var userId))
        {
            throw new WebUnauthorizedException("Invalid user token");
        }

        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null)
        {
            throw new WebNotFoundException("User not found");
        }

        return Ok(user);
    }

    [HttpPut("{id:long}")]
    [ProducesResponseType<UserResponseDto>(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateUser(long id, [FromBody] UserUpdateDto request)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !long.TryParse(userIdClaim, out var currentUserId))
        {
            throw new WebUnauthorizedException("Invalid user token");
        }

        // Users can only update their own profile
        if (currentUserId != id && !User.IsInRole(nameof(Role.Admin)))
        {
            throw new WebForbiddenException("You do not have permission to update this user.");
        }

        var updatedUser = await _userService.UpdateUserAsync(id, request);
        return Ok(updatedUser);
    }

    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteUser(long id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !long.TryParse(userIdClaim, out var currentUserId))
        {
            throw new WebUnauthorizedException("Invalid user token");
        }

        if (currentUserId != id && !User.IsInRole(nameof(Role.Admin)))
        {
            throw new WebForbiddenException("You do not have permission to delete this user.");
        }

        await _userService.DeleteUserAsync(id);
        return NoContent();
    }
}