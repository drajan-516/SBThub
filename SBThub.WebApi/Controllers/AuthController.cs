using System.Security.Claims;
using SBThub.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SBThub.Application.Contracts.Contracts.Requests.Authorization;
using SBThub.Application.UseCases.Authorization.Login;

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

        var user = result.Value; // допустим тут UserResponse с Uuid, FullName и т.д.

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Uuid.ToString()),
            new(ClaimTypes.Name, user.FullName)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
        {
            IsPersistent = true, // куку не удалять при закрытии браузера
            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
        });

        return Ok(new { user.Uuid, user.FullName });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok();
    }
}
