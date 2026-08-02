namespace SBThub.Application.Contracts.Contracts.Requests.Product;

public sealed record CreateProductRequest(string FullTitle, string Description, decimal Price, Guid UserUuid);