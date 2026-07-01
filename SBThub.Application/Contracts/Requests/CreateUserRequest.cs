namespace SBThub.Application.Contracts.Requests;

public sealed record CreateUserRequest(string FullName, string? Phone);
