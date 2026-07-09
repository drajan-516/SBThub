using SBThub.Domain.Errors;
using SBThub.Domain.Shared;

namespace SBThub.Domain.ValueObjects.Product;
public sealed record ProductDescription
{
    public const int MaxLength = 3000;

    private ProductDescription(string value) => Value = value;

    public string Value { get; }

    public static ResultResponse<ProductDescription> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return ResultResponse.Failure<ProductDescription>(UserErrors.NameRequired);

        value = value.Trim();

        if (value.Length > MaxLength)
            return ResultResponse.Failure<ProductDescription>(UserErrors.NameTooLong);

        return ResultResponse.Success(new ProductDescription(value));
    }

    public override string ToString() => Value;
}