using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Responses;
using SBThub.Application.Mapping;
using SBThub.Domain.Entities;
using SBThub.Domain.Repositories;
using SBThub.Domain.Shared;

namespace SBThub.Application.UseCases.Users.GetUsers;

internal sealed class GetUserHandler(IRepository repository)
    : IQueryHandler<GetUsersQuery, IReadOnlyList<UserResponse>>
{
    public async Task<ResultResponse<IReadOnlyList<UserResponse>>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        var allUsers = await repository.GetAllAsync<User>(cancellationToken);
        IReadOnlyList<UserResponse> response = allUsers.Select(user => user.ToResponse()).ToList();
        return ResultResponse.Success(response);
    }
}