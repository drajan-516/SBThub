using SBThub.Application.Abstractions;
using SBThub.Application.Contracts.Contracts.Requests.Registration;
using SBThub.Application.Contracts.Contracts.Responses;
using SBThub.Domain.Entities;
using SBThub.Domain.Repositories;
using SBThub.Domain.Shared;


namespace SBThub.Application.UseCases.Authorization.Registration;

internal sealed class RegistrationHandler(IRepository users, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IRepository usersProfiles, IJwtTokenService jwtTokenService)
{
    public async Task<ResultResponse<TokenResponse>> Handle(RegistrationRequest request, CancellationToken cancellationToken)
    {
        var passwordHash = request.Password is not null
            ? passwordHasher.Hash(request.Password)
            : null;

        var userResult = User.Create(request.FullName, request.Phone, request.Email, passwordHash);
        if (userResult.IsFailure)
            return ResultResponse.Failure<TokenResponse>(userResult.Error);

        var user = userResult.Value;
        await users.Add(user, cancellationToken);

        var profileResult = UserProfile.Create(user.Uuid, request.FullName, request.Phone, request.Email, null);
        if (profileResult.IsFailure)
            return ResultResponse.Failure<TokenResponse>(profileResult.Error);

        await usersProfiles.Add(profileResult.Value, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var token = jwtTokenService.GenerateAccessToken(user);

        return ResultResponse.Success(token);
    }
}