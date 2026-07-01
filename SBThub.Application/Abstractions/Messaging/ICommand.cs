using SBThub.Domain.Shared;
using MediatR;

namespace SBThub.Application.Abstractions.Messaging;

public interface ICommand : IRequest<ResultResponse>;

public interface ICommand<TResponse> : IRequest<ResultResponse<TResponse>>;
