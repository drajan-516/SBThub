using System.Text.RegularExpressions;
using SBThub.Domain.Errors;
using SBThub.Domain.Shared;

namespace SBThub.Domain.ValueObjects.UserProfile;

public sealed partial record UserProfilePhone
{
    public const int MaxLength = 15;

    private UserProfilePhone(string value) => Value = value;

    public string Value { get; }

    public static ResultResponse<UserProfilePhone> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return ResultResponse.Failure<UserProfilePhone>(UserProfileErrors.PhoneRequired);

        value = value.Trim();

        if (value.Length > MaxLength)
            return ResultResponse.Failure<UserProfilePhone>(UserProfileErrors.PhoneInvalidFormat);

        if (!PhoneRegex().IsMatch(value))
            return ResultResponse.Failure<UserProfilePhone>(UserProfileErrors.PhoneInvalidFormat);

        return ResultResponse.Success(new UserProfilePhone(value));
    }

    [GeneratedRegex(@"^\+?[0-9]{7,15}$")]
    private static partial Regex PhoneRegex();

    public override string ToString() => Value;
}