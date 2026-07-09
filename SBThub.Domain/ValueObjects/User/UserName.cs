using SBThub.Domain.Errors;
using SBThub.Domain.Shared;

namespace SBThub.Domain.ValueObjects;
public sealed record UserName
{
    public const int MaxLength = 100;

    private UserName(string value) => Value = value;

    public string Value { get; }

    public static ResultResponse<UserName> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return ResultResponse.Failure<UserName>(UserErrors.NameRequired);

        value = value.Trim();

        if (value.Length > MaxLength)
            return ResultResponse.Failure<UserName>(UserErrors.NameTooLong);

        return ResultResponse.Success(new UserName(value));
    }

    public override string ToString() => Value;
}
