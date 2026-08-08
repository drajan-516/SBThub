using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Responses;
using SBThub.Application.Mapping;
using SBThub.Domain.Entities;
using SBThub.Domain.Repositories;
using SBThub.Domain.Shared;

namespace SBThub.Application.UseCases.UserProfiles.UpdateUserProfileVisibility;


internal sealed class UpdateUserProfileVisibilityHandler(IRepository users, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateUserProfileVisibilityCommand, UserProfileResponse>
{
    public async Task<ResultResponse<UserProfileResponse>> Handle(UpdateUserProfileVisibilityCommand command, CancellationToken cancellationToken)
    {
        var profile = await users.GetSingleAsync<UserProfile>(p => p.UserId == command.UserId, cancellationToken);
        if (profile is null)
            return ResultResponse.Failure<UserProfileResponse>(Error.NotFound("UserProfile.NotFound", "Профиль не найден"));
       
        var updateResult = profile.UpdateVisibility(command.Request.IsFullNameVisible, command.Request.IsPhoneVisible, command.Request.IsEmailVisible, command.Request.IsAvatarVisible);
        if (updateResult.IsFailure)
            return ResultResponse.Failure<UserProfileResponse>(updateResult.Error);
        
        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ResultResponse.Success(profile.ToResponse());
    }
}