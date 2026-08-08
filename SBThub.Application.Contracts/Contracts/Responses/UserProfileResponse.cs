namespace SBThub.Application.Contracts.Responses;

public sealed record UserProfileResponse(Guid Uuid, string FullName, string? Phone, string? Email)

{
public bool IsFullNameVisible { get; private set; } = true;
public bool IsPhoneVisible { get; private set; } = true;
public bool IsEmailVisible { get; private set; } = true;
public bool IsAvatarVisible { get; private set; } = true;
}
