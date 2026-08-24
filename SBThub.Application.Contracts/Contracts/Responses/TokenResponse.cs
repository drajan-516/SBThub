namespace SBThub.Application.Contracts.Contracts.Responses;

public sealed record TokenResponse(
    string AccessToken,
    int ExpiresIn);