using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Requests.UserProfile;
using SBThub.Application.Contracts.Responses;

namespace SBThub.Application.UseCases.UserProfiles.UpdateUserProfile;

public sealed record UpdateUserProfileCommand(Guid UserId, UpdateUserProfileRequest Request) : ICommand<UserProfileResponse>;