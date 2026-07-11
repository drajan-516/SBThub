using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Requests;
using SBThub.Application.Contracts.Requests.Product;
using SBThub.Application.Contracts.Responses;

namespace SBThub.Application.UseCases.Products.UpdateProduct;

public sealed record UpdateProductCommand(Guid Uuid, UpdateProductRequest Request) : ICommand<ProductResponse>;