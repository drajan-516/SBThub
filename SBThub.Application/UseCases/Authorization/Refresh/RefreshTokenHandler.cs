using SBThub.Application.Abstractions;
using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Contracts.Responses;
using SBThub.Application.Mapping;
using SBThub.Domain.Entities;
using SBThub.Domain.Errors;
using SBThub.Domain.Repositories;
using SBThub.Domain.Shared;

namespace SBThub.Application.UseCases.Authorization.Refresh;

internal sealed class RefreshTokenHandler(
    IRepository repository,
    IUnitOfWork unitOfWork,
    IJwtTokenService jwtTokenService)
    : ICommandHandler<RefreshTokenCommand, LoginResult>
{
    public async Task<ResultResponse<LoginResult>> Handle(RefreshTokenCommand command, CancellationToken ct)
    {
        var hash = jwtTokenService.HashToken(command.RefreshToken);
        var stored = await repository.GetSingleAsync<RefreshToken>(t => t.TokenHash == hash, ct);

        if (stored is null || !stored.IsActive)
            return ResultResponse.Failure<LoginResult>(UserErrors.InvalidRefreshToken);

        var user = await repository.GetSingleAsync<User>(u => u.Uuid == stored.UserUuid, ct);
        if (user is null)
            return ResultResponse.Failure<LoginResult>(UserErrors.InvalidRefreshToken);

        stored.Revoke();
        var newRefresh = jwtTokenService.GenerateRefreshToken();
        await repository.Add(
            RefreshToken.Create(user.Uuid, jwtTokenService.HashToken(newRefresh.Token), newRefresh.ExpiresAt),
            ct);
        await unitOfWork.SaveChangesAsync(ct);

        var accessToken = jwtTokenService.GenerateAccessToken(user);
        return ResultResponse.Success(new LoginResult(user.ToResponse(), accessToken, newRefresh));
    }
}