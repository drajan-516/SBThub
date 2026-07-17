using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Contracts.Requests.Product;

namespace SBThub.Application.UseCases.Products.CreateProduct;

public sealed record CreateProductCommand(CreateProductRequest Request) : ICommand;