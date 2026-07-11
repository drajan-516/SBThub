using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Requests.User;
using SBThub.Application.Contracts.Responses;

namespace SBThub.Application.UseCases.Users.CreateUser;

public sealed record CreateUserCommand(CreateUserRequest Request) : ICommand<UserResponse>;