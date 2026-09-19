namespace SBThub.Application.Contracts.Contracts.Responses;

public sealed record RefreshTokenResponse(string Token, DateTime ExpiresAt);