using MediatR;
using Microsoft.AspNetCore.Mvc;
using SBThub.Application.Contracts.Contracts.Requests.Authorization;
using SBThub.Application.Contracts.Contracts.Responses;
using SBThub.Application.UseCases.Authorization.Login;
using SBThub.Application.UseCases.Authorization.Refresh;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SBThub.WebApi.Controllers;

[Route("api/auth")]
public sealed class AuthController(ISender sender) : BaseApiController(sender)
{
    private const string AccessCookie = "access_token";
    private const string RefreshCookie = "refresh_token";
    private const string RefreshPath = "/api/auth";
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        // Handler проверяет логин/пароль, возвращает данные юзера если верно
        var result = await Sender.Send(new LoginCommand(request), cancellationToken);
        if (result.IsFailure)
            return HandleFailure(result);
        
        SetAuthCookies(result.Value);
        return Ok(result.Value.User);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(CancellationToken ct)
    {
        if (!Request.Cookies.TryGetValue(RefreshCookie, out var refreshToken) ||
            string.IsNullOrEmpty(refreshToken))
            return Unauthorized();

        var result = await Sender.Send(new RefreshTokenCommand(refreshToken), ct);
        if (result.IsFailure)
        {
            DeleteAuthCookies();
            return HandleFailure(result);
        }

        SetAuthCookies(result.Value);
        return Ok(result.Value.User);
    }
    
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        DeleteAuthCookies();
        return Ok();
    }
    
    // Просто проверка авторизации
    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        return Ok(new
        {
            UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
            Name = User.Identity?.Name
        });
    }

    private void SetAuthCookies(LoginResult result)
    {
        Response.Cookies.Append(AccessCookie, result.AccessToken.AccessToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            MaxAge = TimeSpan.FromSeconds(result.AccessToken.ExpiresIn)
        });

        Response.Cookies.Append(RefreshCookie, result.RefreshToken.Token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Path = RefreshPath,
            Expires = result.RefreshToken.ExpiresAt
        });
    }

    private void DeleteAuthCookies()
    {
        Response.Cookies.Delete(AccessCookie);
        Response.Cookies.Delete(RefreshCookie, new CookieOptions { Path = RefreshPath });
    }
}
