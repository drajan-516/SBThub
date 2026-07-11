using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Responses;

namespace SBThub.Application.UseCases.Products.GetProductCreator;

public sealed record GetProductCreatorQuery(Guid ProductUuid) : IQuery<UserResponse>;