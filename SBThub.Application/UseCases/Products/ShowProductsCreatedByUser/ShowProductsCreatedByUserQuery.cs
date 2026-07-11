using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Responses;

namespace SBThub.Application.UseCases.Products.ShowProductsCreatedByUser;

public sealed record ShowProductsCreatedByUserQuery(Guid UserUuid) : IQuery<IReadOnlyList<ProductResponse>>;