using SBThub.Application.Contracts.Contracts.Responses;
using SBThub.Domain.Entities;

namespace SBThub.Application.Abstractions;

public interface IJwtTokenService
{
    TokenResponse GenerateAccessToken(User user);
}