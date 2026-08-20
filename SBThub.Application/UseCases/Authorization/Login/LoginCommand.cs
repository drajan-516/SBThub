using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Contracts.Requests.Authorization;
using SBThub.Application.Contracts.Responses;

namespace SBThub.Application.UseCases.Authorization.Login;

public sealed record LoginCommand(LoginRequest Request) : ICommand<UserResponse>;