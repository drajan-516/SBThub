using SBThub.Domain.Shared;
using MediatR;

namespace SBThub.Application.Abstractions.Messaging;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, ResultResponse>
    where TCommand : ICommand;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, ResultResponse<TResponse>>
    where TCommand : ICommand<TResponse>;
