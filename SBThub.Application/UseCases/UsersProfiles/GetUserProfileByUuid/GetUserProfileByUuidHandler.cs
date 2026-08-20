using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Responses;
using SBThub.Application.Mapping;
using SBThub.Domain.Entities;
using SBThub.Domain.Errors;
using SBThub.Domain.Repositories;
using SBThub.Domain.Shared;

namespace SBThub.Application.UseCases.UsersProfiles.GetUserProfileByUuid;

internal sealed class GetUserProfileByUuidHandler(IRepository repository)
    : IQueryHandler<GetUserProfileByUserIdQuery, UserProfileResponse>
{
    public async Task<ResultResponse<UserProfileResponse>> Handle(GetUserProfileByUserIdQuery query, CancellationToken cancellationToken)
    {
        var profile = await repository.GetSingleAsync<UserProfile>(p => p.UserId == query.UserId, cancellationToken);

        return profile is null
                ? ResultResponse.Failure<UserProfileResponse>(UserProfileErrors.NotFound)
                :  ResultResponse.Success(profile.ToResponse());
    }
}