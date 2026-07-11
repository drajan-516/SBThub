using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Responses;
using SBThub.Application.Mapping;
using SBThub.Application.Mapping.Product;
using SBThub.Domain.Errors;
using SBThub.Domain.Repositories;
using SBThub.Domain.Shared;

namespace SBThub.Application.UseCases.Products.ShowProductsCreatedByUser;

internal sealed class ShowProductsCreatedByUserHandler(IProductRepository products, IUserRepository users)
    : IQueryHandler<ShowProductsCreatedByUserQuery, IReadOnlyList<ProductResponse>>
{
    public async Task<ResultResponse<IReadOnlyList<ProductResponse>>> Handle(
        ShowProductsCreatedByUserQuery query, CancellationToken cancellationToken)
    {
        var user = await users.GetByUuidAsync(query.UserUuid, cancellationToken);
        if (user is null)
            return ResultResponse.Failure<IReadOnlyList<ProductResponse>>(UserErrors.NotFound);

        var allProducts = await products.GetAllAsync(cancellationToken);

        IReadOnlyList<ProductResponse> response = allProducts
            .Where(product => product.CreatedByUserId == user.Uuid)
            .Select(product => product.ToResponse())
            .ToList();

        return ResultResponse.Success(response);
    }
}