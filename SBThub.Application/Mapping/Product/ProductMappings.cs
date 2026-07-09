using SBThub.Application.Contracts.Responses;

namespace SBThub.Application.Mapping.Product;

public static class ProductMappings
{
    public static ProductResponse ToResponse(this SBThub.Domain.Entities.Product product) =>
        new(product.Uuid, product.FullTitle, product.Description, product.Price, product.CreatedOn);
}