using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Responses;
using SBThub.Application.Mapping;
using SBThub.Domain.Entities;
using SBThub.Domain.Errors;
using SBThub.Domain.Repositories;
using SBThub.Domain.Shared;

// UserSSSSS ProfileSSSSSS not User Profile, cuz 1 user = 1 profile, not 1 user = many profiles

namespace SBThub.Application.UseCases.UsersProfiles.CreateUserProfile;

// Edited var's and IRepository names

internal sealed class CreateUserProfileHandler(IRepository usersProfiles, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateUserProfileCommand, UserProfileResponse>
{
    public async Task<ResultResponse<UserProfileResponse>> Handle(CreateUserProfileCommand command, CancellationToken cancellationToken)
    {
        
        // Get user data 

        var user = await usersProfiles.GetByUuidAsync<User>(command.UserId, cancellationToken);
        if (user is null)
            return ResultResponse.Failure<UserProfileResponse>(UserErrors.NotFound);
        
        //Add USERPROFILE in USERPROFILES repository instead of USERPROFILE in USERS repository
        
        var userProfileResult = UserProfile.Create(
            user.Uuid,
            user.FullName.Value,
            user.Phone,
            command.Request.Email,
            command.Request.AvatarUrl);
        
        if (userProfileResult.IsFailure)
            return ResultResponse.Failure<UserProfileResponse>(userProfileResult.Error);
        
        await usersProfiles.Add(userProfileResult.Value, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ResultResponse.Success(userProfileResult.Value.ToResponse());
    }
}