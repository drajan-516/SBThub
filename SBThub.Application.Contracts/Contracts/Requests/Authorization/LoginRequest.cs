namespace SBThub.Application.Contracts.Contracts.Requests.Authorization;

public sealed record LoginRequest(string Email, string Password);