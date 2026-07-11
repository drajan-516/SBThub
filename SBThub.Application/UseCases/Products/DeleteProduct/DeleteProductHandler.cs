using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Responses;
using SBThub.Application.Mapping;
using SBThub.Domain.Entities;
using SBThub.Domain.Repositories;
using SBThub.Domain.Shared;

namespace SBThub.Application.UseCases.Products.DeleteProduct;

internal sealed class DeleteProductHandler(IProductRepository products, IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteProductCommand>
{
    public async Task<ResultResponse> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await products.GetByUuidAsync(command.Uuid, cancellationToken);
        if (product is null)
            return ResultResponse.Failure(Error.NotFound("Product.NotFound", "Продукт не найден"));

        products.Remove(product);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ResultResponse.Success();
    }
}