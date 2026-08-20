using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Requests.UserProfile;
using SBThub.Application.Contracts.Responses;

namespace SBThub.Application.UseCases.UserProfiles.UpdateUserProfileVisibility;

public sealed record UpdateUserProfileVisibilityCommand(Guid UserId, UpdateUserProfileVisibilityRequest Request) : ICommand<UserProfileResponse>;