using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Responses;
using SBThub.Application.Mapping;
using SBThub.Domain.Entities;
using SBThub.Domain.Repositories;
using SBThub.Domain.Shared;

namespace SBThub.Application.UseCases.Users.CreateUser;

internal sealed class CreateUserHandler(IUserRepository users, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateUserCommand, UserResponse>
{
    public async Task<ResultResponse<UserResponse>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var userResult = User.Create(command.Request.FullName, command.Request.Phone);
        if (userResult.IsFailure)
            return ResultResponse.Failure<UserResponse>(userResult.Error);

        users.Add(userResult.Value);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ResultResponse.Success(userResult.Value.ToResponse());
    }
}
