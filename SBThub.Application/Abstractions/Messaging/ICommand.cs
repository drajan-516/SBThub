using MediatR;
using SBThub.Domain.Shared;

namespace SBThub.Application.Abstractions.Messaging;

public interface ICommand : IRequest<ResultResponse>;

public interface ICommand<TResponse> : IRequest<ResultResponse<TResponse>>;
