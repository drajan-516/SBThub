using SBThub.Application.Abstractions.Messaging;

namespace SBThub.Application.UseCases.Users.DeleteUser;

public sealed record DeleteUserCommand(Guid Uuid) : ICommand;