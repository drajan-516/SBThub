using SBThub.Domain.Shared;
using MediatR;

namespace SBThub.Application.Abstractions.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, ResultResponse<TResponse>>
    where TQuery : IQuery<TResponse>;
