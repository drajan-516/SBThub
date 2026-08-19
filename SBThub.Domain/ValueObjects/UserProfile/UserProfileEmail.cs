using System.Text.RegularExpressions;
using SBThub.Domain.Errors;
using SBThub.Domain.Shared;

namespace SBThub.Domain.ValueObjects.UserProfile;

public sealed partial record UserProfileEmail
{
    public const int MaxLength = 254;
    
    private UserProfileEmail(string value) => Value = value;
    
    public string Value { get; }

    public static ResultResponse<UserProfileEmail> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return ResultResponse.Failure<UserProfileEmail>(UserProfileErrors.EmailRequired);
        
        if (string.IsNullOrWhiteSpace(value))
            return ResultResponse.Failure<UserProfileEmail>(UserProfileErrors.EmailInvalidFormat);
        
        if (!EmailRegex().IsMatch(value))
            return ResultResponse.Failure<UserProfileEmail>(UserProfileErrors.EmailInvalidFormat);
        
        return ResultResponse.Success(new UserProfileEmail(value));
    }

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    private static partial Regex EmailRegex();

    public override string ToString() => Value;
}