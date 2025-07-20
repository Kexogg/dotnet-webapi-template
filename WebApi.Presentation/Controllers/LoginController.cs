using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.Application.DTOs.User;
using WebApi.Application.Services;
using WebApi.Domain.Exceptions;

namespace WebApi.Presentation.Controllers;

[ApiController]
[Route("auth")]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
public class LoginController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;

    public LoginController(IUserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
    }

    [HttpPost("login")]
    [ProducesResponseType<UserLoginResponseDto>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] UserLoginDto request)
    {
        if (!await _userService.VerifyUserPasswordAsync(request.Email, request.Password))
        {
            throw new WebUnauthorizedException("Invalid email or password.");
        }

        var user = await _userService.GetUserByEmailAsync(request.Email);
        if (user == null)
        {
            throw new WebUnauthorizedException("Invalid email or password");
        }

        Response.Cookies.Append("access_token", GenerateJwtToken(user), GetCookieOptions());


        return Ok(user.ToLoginDto());
    }

    [HttpPost("register")]
    [ProducesResponseType<UserLoginResponseDto>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Register([FromBody] UserRegistrationDto request)
    {
        var user = await _userService.RegisterUserAsync(request);
        if (user == null)
        {
            throw new WebBadRequestException("User registration failed.");
        }
        Response.Cookies.Append("access_token", GenerateJwtToken(user), GetCookieOptions());
        return Ok(user.ToLoginDto());
    }

    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("access_token");
        return Ok();
    }

    private static CookieOptions GetCookieOptions()
    {
        return new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddHours(24)
        };
    }

    private string GenerateJwtToken(UserResponseRichDto user)
    {
        var jwtSettings = _configuration.GetSection("Authentication");
        var secretKey = jwtSettings["SecretKey"]!;
        var issuer = jwtSettings["Issuer"]!;
        var audience = jwtSettings["Audience"]!;

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(24),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}