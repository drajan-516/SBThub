using SBThub.Domain.Errors;
using SBThub.Domain.Shared;

namespace SBThub.Domain.ValueObjects.UserProfile;

public sealed record UserProfileName
{
    public const int MaxLength = 20;

    private UserProfileName(string value) => Value = value;

    public string Value { get; }

    public static ResultResponse<UserProfileName> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return ResultResponse.Failure<UserProfileName>(UserProfileErrors.NameRequired);

        value = value.Trim();

        if (value.Length > MaxLength)
            return ResultResponse.Failure<UserProfileName>(UserProfileErrors.NameTooLong);

        return ResultResponse.Success(new UserProfileName(value));
    }

    public override string ToString() => Value;
}