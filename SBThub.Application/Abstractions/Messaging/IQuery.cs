using SBThub.Domain.Shared;
using MediatR;

namespace SBThub.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<ResultResponse<TResponse>>;
