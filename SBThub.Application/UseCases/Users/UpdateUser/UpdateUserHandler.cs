using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Responses;
using SBThub.Application.Mapping;
using SBThub.Domain.Entities;
using SBThub.Domain.Repositories;
using SBThub.Domain.Shared;

namespace SBThub.Application.UseCases.Users.UpdateUser;

internal sealed class UpdateUserHandler(IUserRepository users, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateUserCommand, UserResponse>
{
    public async Task<ResultResponse<UserResponse>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var user = await users.GetByUuidAsync(command.Uuid, cancellationToken);
        if (user is null) return ResultResponse.Failure<UserResponse>(Error.NotFound("User not found", "Пользователь не найден"));
        
        var updateResult = user.Update(command.Request.FullName, command.Request.Phone);
        if (updateResult.IsFailure) return ResultResponse.Failure<UserResponse>(updateResult.Error);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ResultResponse.Success(user.ToResponse());
    }
}
