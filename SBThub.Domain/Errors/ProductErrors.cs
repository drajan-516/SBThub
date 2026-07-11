using SBThub.Domain.Shared;
using SBThub.Domain.ValueObjects.Product;

namespace SBThub.Domain.Errors;

public static class ProductErrors
{
    public static readonly Error NameRequired =
        Error.Validation("Product.NameRequired", "Название продукта обязательно.");

    public static readonly Error NameTooLong =
        Error.Validation("Product.NameTooLong", $"Название продукта не длиннее {ProductTitle.MaxLength} символов.");

    public static readonly Error NotFound =
        Error.NotFound("Product.NotFound", "Продукт не найден.");
}