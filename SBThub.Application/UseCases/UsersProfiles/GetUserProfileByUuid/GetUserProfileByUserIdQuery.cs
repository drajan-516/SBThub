using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Responses;

namespace SBThub.Application.UseCases.UsersProfiles.GetUserProfileByUuid;

public sealed record GetUserProfileByUserIdQuery(Guid UserId) : IQuery<UserProfileResponse>;