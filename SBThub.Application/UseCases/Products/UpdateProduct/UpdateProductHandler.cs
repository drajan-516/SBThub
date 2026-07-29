using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Responses;
using SBThub.Application.Mapping.Product;
using SBThub.Domain.Entities;
using SBThub.Domain.Repositories;
using SBThub.Domain.Shared;

namespace SBThub.Application.UseCases.Products.UpdateProduct;

internal sealed class UpdateProductHandler(IRepository repository, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateProductCommand, ProductResponse>
{
    public async Task<ResultResponse<ProductResponse>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await repository.GetByUuidAsync<Product>(command.Uuid, cancellationToken);
        if (product is null)
            return ResultResponse.Failure<ProductResponse>(Error.NotFound("Product.NotFound", "Продукт не найден"));

        var updateResult = product.Update(command.Request.FullTitle, command.Request.Description);
        if (updateResult.IsFailure)
            return ResultResponse.Failure<ProductResponse>(updateResult.Error);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ResultResponse.Success(product.ToResponse());
    }
}