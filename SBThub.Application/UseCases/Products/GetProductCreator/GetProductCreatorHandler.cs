using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Responses;
using SBThub.Domain.Errors;
using SBThub.Application.Mapping;
using SBThub.Domain.Repositories;
using SBThub.Domain.Shared;

namespace SBThub.Application.UseCases.Products.GetProductCreator;

internal sealed class GetProductCreatorHandler(IProductRepository products, IUserRepository users)
    : IQueryHandler<GetProductCreatorQuery, UserResponse>
{
    public async Task<ResultResponse<UserResponse>> Handle(GetProductCreatorQuery query, CancellationToken cancellationToken)
    {
        var user = await users.GetByUuidAsync(query.UserUuid, cancellationToken);
        if (user is null)
            return ResultResponse.Failure<UserResponse>(UserErrors.NotFound);

        return ResultResponse.Success(user.ToResponse());
    }
}