using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Requests.Product;
using SBThub.Application.Contracts.Responses;

namespace SBThub.Application.UseCases.Products.CreateProduct;

public sealed record CreateProductCommand(CreateProductRequest Request) : ICommand<ProductResponse>;