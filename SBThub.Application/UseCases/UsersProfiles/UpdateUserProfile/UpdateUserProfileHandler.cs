using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Responses;
using SBThub.Application.Mapping;
using SBThub.Domain.Entities;
using SBThub.Domain.Repositories;
using SBThub.Domain.Shared;

namespace SBThub.Application.UseCases.UserProfiles.UpdateUserProfile;


internal sealed class UpdateUserProfileHandler(IRepository users, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateUserProfileCommand, UserProfileResponse>
{
    public async Task<ResultResponse<UserProfileResponse>> Handle(UpdateUserProfileCommand command, CancellationToken cancellationToken)
    {
        var profile = await users.GetSingleAsync<UserProfile>(p => p.UserId == command.UserId, cancellationToken);
        if (profile is null)
            return ResultResponse.Failure<UserProfileResponse>(Error.NotFound("UserProfile.NotFound", "Профиль не найден"));
       
        var updateResult = profile.Update(command.Request.FullName, command.Request.Phone, command.Request.Email, command.Request.AvatarUrl);
        if (updateResult.IsFailure)
            return ResultResponse.Failure<UserProfileResponse>(updateResult.Error);
        
        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ResultResponse.Success(profile.ToResponse());
    }
}