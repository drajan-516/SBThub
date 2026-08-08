using SBThub.Domain.Common;
using SBThub.Domain.Shared;
using SBThub.Domain.ValueObjects.User;

namespace SBThub.Domain.Entities;

public sealed class UserProfile : Entity
{
    private UserProfile() { }

    public Guid UserId { get; private set; }

    public UserName FullName { get; private set; } = null!;
    public string? Phone { get; private set; }
    public string? Email { get; private set; }
    public string? AvatarUrl { get; private set; }

    public bool IsFullNameVisible { get; private set; } = true;
    public bool IsPhoneVisible { get; private set; } = true;
    public bool IsEmailVisible { get; private set; } = true;
    public bool IsAvatarVisible { get; private set; } = true;

    private UserProfile(Guid uuid, Guid userId, UserName fullName, string? phone, string? email, string? avatarUrl)
        : base(uuid)
    {
        UserId = userId;
        FullName = fullName;
        Phone = phone;
        Email = email;
        AvatarUrl = avatarUrl;
    }

    public static ResultResponse<UserProfile> Create(Guid userId, string? fullName, string? phone, string? email, string? avatarUrl)
    {
        var nameResult = UserName.Create(fullName);
        if (nameResult.IsFailure)
            return ResultResponse.Failure<UserProfile>(nameResult.Error);

        return ResultResponse.Success(new UserProfile(Guid.NewGuid(), userId, nameResult.Value, phone, email, avatarUrl));
    }

    public ResultResponse Update(string? fullName, string? phone, string? email, string? avatarUrl)
    {
        if (fullName is not null)
        {
            var nameResult = UserName.Create(fullName);
            if (nameResult.IsFailure) return ResultResponse.Failure(nameResult.Error);
            FullName = nameResult.Value;
        }
        
        Phone = phone ?? Phone;
        Email = email ?? Email;
        AvatarUrl = avatarUrl ?? AvatarUrl;

        return ResultResponse.Success();
    }

    public ResultResponse UpdateVisibility(bool isFullNameVisible, bool isPhoneVisible, bool isEmailVisible, bool isAvatarVisible)
    {
        IsFullNameVisible = isFullNameVisible;
        IsPhoneVisible = isPhoneVisible;
        IsEmailVisible = isEmailVisible;
        IsAvatarVisible = isAvatarVisible;

        return ResultResponse.Success();
    }
}