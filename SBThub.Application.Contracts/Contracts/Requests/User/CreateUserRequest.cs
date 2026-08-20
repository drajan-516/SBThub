namespace SBThub.Application.Contracts.Requests.User;

public sealed record CreateUserRequest(string FullName, string? Phone, string? Email, string? Password);
