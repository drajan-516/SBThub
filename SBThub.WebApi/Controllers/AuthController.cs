using MediatR;
using Microsoft.AspNetCore.Mvc;
using SBThub.Application.Contracts.Contracts.Requests.Authorization;
using SBThub.Application.UseCases.Authorization.Login;
// Для проверки авторизации
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace SBThub.WebApi.Controllers;

[Route("api/auth")]
public sealed class AuthController(ISender sender) : BaseApiController(sender)
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        // Handler проверяет логин/пароль, возвращает данные юзера если верно
        var result = await Sender.Send(new LoginCommand(request), cancellationToken);
        if (result.IsFailure)
            return HandleFailure(result);

        var tokenResponse = result.Value.AccessToken; // допустим тут UserResponse с Uuid, FullName и т.д.

        Response.Cookies.Append(
            "access_token",
            tokenResponse.AccessToken,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                MaxAge = TimeSpan.FromSeconds(tokenResponse.ExpiresIn)
            });

        return Ok(result.Value.User);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        Response.Cookies.Delete("access_token");
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
}
