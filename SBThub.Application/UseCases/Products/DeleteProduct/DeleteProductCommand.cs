using SBThub.Application.Abstractions.Messaging;

namespace SBThub.Application.UseCases.Products.DeleteProduct;

public sealed record DeleteProductCommand(Guid Uuid) : ICommand;