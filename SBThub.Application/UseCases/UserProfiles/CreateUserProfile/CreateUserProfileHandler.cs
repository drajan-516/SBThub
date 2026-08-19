using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Responses;
using SBThub.Application.Mapping;
using SBThub.Domain.Entities;
using SBThub.Domain.Repositories;
using SBThub.Domain.Shared;

namespace SBThub.Application.UseCases.UserProfiles.CreateUserProfile;

internal sealed class CreateUserProfileHandler(IRepository users, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateUserProfileCommand, UserProfileResponse>
{
    public async Task<ResultResponse<UserProfileResponse>> Handle(CreateUserProfileCommand command, CancellationToken cancellationToken)
    {
        var userResult = UserProfile.Create(command.UserId, command.Request.FullName, command.Request.Phone, command.Request.Email, command.Request.AvatarUrl);
        if (userResult.IsFailure)
            return ResultResponse.Failure<UserProfileResponse>(userResult.Error);

        await users.Add(userResult.Value, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ResultResponse.Success(userResult.Value.ToResponse());
    }
}