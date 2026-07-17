using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Responses;
using SBThub.Application.Mapping.Product;
using SBThub.Domain.Entities;
using SBThub.Domain.Repositories;
using SBThub.Domain.Shared;

namespace SBThub.Application.UseCases.Products.CreateProduct;

internal sealed class CreateProductHandler(IProductRepository products, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateProductCommand>
{
    public async Task<ResultResponse> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var productResult = Product.Create(command.Request);

        if (productResult.IsFailure)
            return ResultResponse.Failure<ProductResponse>(productResult.Error);

        products.Add(productResult.Value);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ResultResponse.Success();
    }
}