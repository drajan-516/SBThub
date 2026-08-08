namespace SBThub.Application.Contracts.Requests.UserProfile;

public sealed record CreateUserProfileRequest(string FullName, string? Phone, string? Email, string? AvatarUrl);