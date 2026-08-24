using SBThub.Application.Contracts.Responses;

namespace SBThub.Application.Contracts.Contracts.Responses;

public sealed record LoginResult(UserResponse User, TokenResponse AccessToken);