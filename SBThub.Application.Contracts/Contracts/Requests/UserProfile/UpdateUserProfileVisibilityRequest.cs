namespace SBThub.Application.Contracts.Requests.UserProfile;

public sealed record UpdateUserProfileVisibilityRequest(bool IsFullNameVisible, bool IsPhoneVisible, bool IsEmailVisible, bool IsAvatarVisible);