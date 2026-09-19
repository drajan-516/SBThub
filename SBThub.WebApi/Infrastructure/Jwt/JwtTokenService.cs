using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Buffers.Text;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SBThub.Application.Abstractions;
using SBThub.Application.Contracts.Contracts.Responses;
using SBThub.Domain.Entities;

namespace SBThub.WebApi.Infrastructure.Jwt;

public sealed class JwtTokenService(
    IOptions<JwtOptions> options) : IJwtTokenService
{
    private readonly JwtOptions _options = options.Value;

    public TokenResponse GenerateAccessToken(User user)
    {
        var jwtId = Guid.NewGuid().ToString();

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Uuid.ToString()),
            new(JwtRegisteredClaimNames.Jti, jwtId),
            new(ClaimTypes.NameIdentifier, user.Uuid.ToString()),
            new(ClaimTypes.Name, user.FullName.ToString())
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_options.SecretKey));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var expiresIn = TimeSpan.FromMinutes(
            _options.AccessTokenLifetimeMinutes);

        var expires = DateTime.UtcNow.Add(expiresIn);

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: expires,
            signingCredentials: credentials);

        var tokenString = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return new TokenResponse(
            tokenString,
            (int)expiresIn.TotalSeconds);
    }
    public RefreshTokenResponse GenerateRefreshToken()
    {
        var bytes = RandomNumberGenerator.GetBytes(64);
        var token = Base64Url.EncodeToString(bytes);
        var expiresAt = DateTime.UtcNow.AddDays(_options.RefreshTokenLifetimeDays);

        return new RefreshTokenResponse(token, expiresAt);
    }

    public string HashToken(string token) =>
        Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(token)));
}
