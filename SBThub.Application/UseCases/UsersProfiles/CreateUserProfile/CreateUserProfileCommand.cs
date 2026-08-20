using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Responses;
using SBThub.Application.Contracts.Requests.UserProfile;

namespace SBThub.Application.UseCases.UsersProfiles.CreateUserProfile;

public sealed record CreateUserProfileCommand(Guid UserId, CreateUserProfileRequest Request) : ICommand<UserProfileResponse>;