using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Responses;
using SBThub.Application.Mapping;
using SBThub.Domain.Entities;
using SBThub.Domain.Errors;
using SBThub.Domain.Repositories;
using SBThub.Domain.Shared;

namespace SBThub.Application.UseCases.Users.GetUserByUuid;

internal sealed class GetUserByUuidHandler(IRepository repository)
    : IQueryHandler<GetUserByUuidQuery, UserResponse>
{
    public async Task<ResultResponse<UserResponse>> Handle(GetUserByUuidQuery query, CancellationToken cancellationToken)
    {
        var user = await repository.GetSingleAsync<User>(u => u.Uuid == query.Uuid, cancellationToken);

        return user is null
            ? ResultResponse.Failure<UserResponse>(UserErrors.NotFound)
            : ResultResponse.Success(user.ToResponse());
    }
}

