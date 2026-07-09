using SBThub.Domain.Errors;
using SBThub.Domain.Shared;

namespace SBThub.Domain.ValueObjects;
public sealed record ProductTitle
{
    public const int MaxLength = 400;

    private ProductTitle(string value) => Value = value;

    public string Value { get; }

    public static ResultResponse<ProductTitle> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return ResultResponse.Failure<ProductTitle>(UserErrors.NameRequired);

        value = value.Trim();

        if (value.Length > MaxLength)
            return ResultResponse.Failure<ProductTitle>(UserErrors.NameTooLong);

        return ResultResponse.Success(new ProductTitle(value));
    }

    public override string ToString() => Value;
}