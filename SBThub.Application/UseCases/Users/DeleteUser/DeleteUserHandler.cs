using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Responses;
using SBThub.Application.Mapping;
using SBThub.Domain.Entities;
using SBThub.Domain.Repositories;
using SBThub.Domain.Shared;

namespace SBThub.Application.UseCases.Users.DeleteUser;

internal sealed class DeleteUserHandler(IUserRepository users, IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteUserCommand>
{
    public async Task<ResultResponse> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var user = await users.GetByUuidAsync(command.Uuid, cancellationToken);
        if (user is null) return ResultResponse.Failure<UserResponse>(Error.NotFound("User not found", "Пользователь не найден"));

        users.Remove(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ResultResponse.Success();
    }
}
