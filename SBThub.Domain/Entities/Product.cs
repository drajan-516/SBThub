using SBThub.Domain.Common;
using SBThub.Domain.Shared;
using SBThub.Domain.ValueObjects.Product;

namespace SBThub.Domain.Entities;

public sealed class Product : Entity
{
    private Product() { }
    public string FullTitle { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public Guid UserUuid { get; private set; }

    private Product(Guid uuid, string fullTitle, string description, decimal price, Guid userUuid) : base(uuid)
    {
        FullTitle = fullTitle;
        Description = description;
        Price = price;
        UserUuid = userUuid;
    }

    public static ResultResponse<Product> Create(string? fullTitle, string description, decimal price, Guid userUuid)
    {
        var titleResult = ProductTitle.Create(fullTitle);
        if (titleResult.IsFailure)
            return ResultResponse.Failure<Product>(titleResult.Error);

        return ResultResponse.Success(new Product(Guid.NewGuid(), titleResult.Value.Value, description, price, userUuid));
    }

    public ResultResponse Update(string? fullTitle, string? description)
    {
        var titleResult = ProductTitle.Create(fullTitle);
        if (titleResult.IsFailure) return ResultResponse.Failure(titleResult.Error);
        FullTitle = titleResult.Value.Value;

        var descriptionResult = ProductDescription.Create(description);
        if (descriptionResult.IsFailure) return ResultResponse.Failure(descriptionResult.Error);
        Description = descriptionResult.Value.Value;

        return ResultResponse.Success();
    }
}