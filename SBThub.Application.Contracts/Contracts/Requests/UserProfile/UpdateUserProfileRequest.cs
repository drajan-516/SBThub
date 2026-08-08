namespace SBThub.Application.Contracts.Requests.UserProfile;

public sealed record UpdateUserProfileRequest(string FullName, string? Phone, string? Email, string? AvatarUrl);