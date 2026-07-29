using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Responses;
using SBThub.Application.Mapping;
using SBThub.Domain.Entities;
using SBThub.Domain.Repositories;
using SBThub.Domain.Shared;

namespace SBThub.Application.UseCases.Users.GetUserByUuid;

internal sealed class GetUserByUuidHandler(IRepository repository)
    : IQueryHandler<GetUserByUuidQuery, UserResponse>
{
    public async Task<ResultResponse<UserResponse>> Handle(GetUserByUuidQuery query, CancellationToken cancellationToken)
    {
        var user = await repository.GetByUuidAsync<User>(query.Uuid, cancellationToken);
        if (user is null)
            return ResultResponse.Failure<UserResponse>(Error.NotFound("User.NotFound", "Пользователь не найден"));

        return ResultResponse.Success(user.ToResponse());
    }
}