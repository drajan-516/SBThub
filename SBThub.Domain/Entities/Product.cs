using SBThub.Domain.Common;
using SBThub.Domain.Shared;
using SBThub.Domain.ValueObjects.Product;


namespace SBThub.Domain.Entities;

public sealed class Product : Entity
{
    private Product() { }
    public ProductTitle FullTitle { get; private set; }
    public ProductDescription? Description { get; private set; }
    public decimal Price { get; private set; }
    public DateTime CreatedOn { get; private set; }
    
    public Guid CreatedByUserId { get; private set; }
    
    private Product(Guid uuid, ProductTitle fullTitle, ProductDescription? description, decimal price, DateTime createdOn, Guid createdByUserId) : base(uuid)
    {
        FullTitle = fullTitle;
        Description = description;
        Price = price;
        CreatedOn = createdOn;
        CreatedByUserId = createdByUserId;
    }
    
    
    public static ResultResponse<Product> Create(string? fullTitle, ProductDescription? description,  decimal price, DateTime createdOn, Guid createdByUserId)
    {
        var titleResult = ProductTitle.Create(fullTitle);
        if (titleResult.IsFailure)
            return ResultResponse.Failure<Product>(titleResult.Error);

        return ResultResponse.Success(new Product(Guid.NewGuid(), titleResult.Value, description, price,  createdOn, createdByUserId));
    }
    
    
    public ResultResponse Update(string? fullTitle, string? description)
    {
        var titleResult = ProductTitle.Create(fullTitle);
        if (titleResult.IsFailure) return ResultResponse.Failure(titleResult.Error);
        FullTitle = titleResult.Value;
        
        var descriptionResult = ProductDescription.Create(description);   
        if (descriptionResult.IsFailure) return ResultResponse.Failure(descriptionResult.Error);
        Description = descriptionResult.Value;

        return ResultResponse.Success();
    }
}
