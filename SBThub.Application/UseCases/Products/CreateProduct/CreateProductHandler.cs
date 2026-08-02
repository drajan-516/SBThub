using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Responses;
using SBThub.Application.Mapping.Product;
using SBThub.Domain.Entities;
using SBThub.Domain.Repositories;
using SBThub.Domain.Shared;

namespace SBThub.Application.UseCases.Products.CreateProduct;

internal sealed class CreateProductHandler(IRepository repository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateProductCommand, ProductResponse>
{
    public async Task<ResultResponse<ProductResponse>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var productResult = Product.Create(
            command.Request.FullTitle,
            command.Request.Description,
            command.Request.Price,
            command.Request.UserUuid);

        if (productResult.IsFailure)
            return ResultResponse.Failure<ProductResponse>(productResult.Error);

        await repository.Add(productResult.Value, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ResultResponse.Success(productResult.Value.ToResponse());
    }
}