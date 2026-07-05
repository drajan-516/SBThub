using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Requests;
using SBThub.Application.Contracts.Responses;

namespace SBThub.Application.UseCases.Users.UpdateUser;

public sealed record UpdateUserCommand(Guid Uuid, UpdateUserRequest Request) : ICommand<UserResponse>;