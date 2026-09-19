using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Contracts.Responses;

namespace SBThub.Application.UseCases.Authorization.Refresh;

public sealed record RefreshTokenCommand(string RefreshToken) : ICommand<LoginResult>;