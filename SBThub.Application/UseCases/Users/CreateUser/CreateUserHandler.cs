using SBThub.Application.Abstractions;
using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Responses;
using SBThub.Application.Mapping;
using SBThub.Domain.Entities;
using SBThub.Domain.Repositories;
using SBThub.Domain.Shared;

namespace SBThub.Application.UseCases.Users.CreateUser;

internal sealed class CreateUserHandler(IRepository users, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
    : ICommandHandler<CreateUserCommand, UserResponse>
{
    public async Task<ResultResponse<UserResponse>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var passwordHash = passwordHasher.Hash(command.Request.Password!);

        var userResult = User.Create(command.Request.FullName, command.Request.Phone, command.Request.Email, passwordHash);
        if (userResult.IsFailure)
            return ResultResponse.Failure<UserResponse>(userResult.Error);

        await users.Add(userResult.Value, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ResultResponse.Success(userResult.Value.ToResponse());
    }
}