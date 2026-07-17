using SBThub.Application.Contracts.Contracts.Requests.Product;
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
    public DateTime CreatedOn { get; private set; }
    public Guid CreatedByUserId { get; private set; }
    
    private Product(Guid uuid, string fullTitle, string description, decimal price, DateTime createdOn, Guid createdByUserId) : base(uuid)
    {
        FullTitle = fullTitle;
        Description = description;
        Price = price;
        CreatedOn = createdOn;
        CreatedByUserId = createdByUserId;
    }
    
    
    public static ResultResponse<Product> Create(CreateProductRequest createProductRequest)
    {
        var titleResult = ProductTitle.Create(createProductRequest.FullTitle);
        
        if (titleResult.IsFailure)
            return ResultResponse.Failure<Product>(titleResult.Error);
        
        var newProduct = new Product
        {
            FullTitle = titleResult.Value.Value,
            Description = createProductRequest.Description,
            Price = createProductRequest.Price,
            CreatedByUserId = createProductRequest.CreatedByUserId
        };

        return ResultResponse.Success(newProduct);
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
