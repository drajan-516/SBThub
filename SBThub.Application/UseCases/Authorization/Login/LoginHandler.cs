using SBThub.Application.Abstractions;
using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Contracts.Responses;
using SBThub.Application.Mapping;
using SBThub.Domain.Entities;
using SBThub.Domain.Errors;
using SBThub.Domain.Repositories;
using SBThub.Domain.Shared;

namespace SBThub.Application.UseCases.Authorization.Login;

internal sealed class LoginHandler(IRepository repository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IJwtTokenService jwtTokenService)
    : ICommandHandler<LoginCommand, LoginResult>
{
    public async Task<ResultResponse<LoginResult>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var user = await repository.GetSingleAsync<User>(u => u.Email == command.Request.Email, cancellationToken);
        if (user is null)
            return ResultResponse.Failure<LoginResult>(UserErrors.InvalidCredentials);

        if (!passwordHasher.Verify(command.Request.Password, user.PasswordHash))
            return ResultResponse.Failure<LoginResult>(UserErrors.InvalidCredentials);
        var accessToken = jwtTokenService.GenerateAccessToken(user);
        var refresh = jwtTokenService.GenerateRefreshToken();
        await repository.Add(
            RefreshToken.Create(user.Uuid, jwtTokenService.HashToken(refresh.Token), refresh.ExpiresAt), cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return ResultResponse.Success(
            new LoginResult(user.ToResponse(), accessToken, refresh));
    }
}