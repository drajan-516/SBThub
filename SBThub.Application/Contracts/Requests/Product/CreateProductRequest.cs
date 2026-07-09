namespace SBThub.Application.Contracts.Requests.Product;

public sealed record CreateProductRequest(string ProductTitle, string Description,  decimal Price, DateTime CreatedOn, Guid CreatedByUserId);