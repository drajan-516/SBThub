using SBThub.Application.Abstractions;
using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Responses;
using SBThub.Application.Mapping;
using SBThub.Domain.Entities;
using SBThub.Domain.Errors;
using SBThub.Domain.Repositories;
using SBThub.Domain.Shared;

namespace SBThub.Application.UseCases.Authorization.Login;

internal sealed class LoginHandler(IRepository repository, IPasswordHasher passwordHasher)
    : ICommandHandler<LoginCommand, UserResponse>
{
    public async Task<ResultResponse<UserResponse>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var user = await repository.GetSingleAsync<User>(u => u.Email == command.Request.Email, cancellationToken);
        if (user is null)
            return ResultResponse.Failure<UserResponse>(UserErrors.InvalidCredentials);

        if (!passwordHasher.Verify(command.Request.Password, user.PasswordHash))
            return ResultResponse.Failure<UserResponse>(UserErrors.InvalidCredentials);

        return ResultResponse.Success(user.ToResponse());
    }
}