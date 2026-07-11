
using SBThub.Application.UseCases.Products.CreateProduct;
using SBThub.Application.UseCases.Products.GetProductCreator;
using SBThub.Application.UseCases.Products.ShowProductsCreatedByUser;
using SBThub.Application.UseCases.Products.DeleteProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SBThub.Application.Contracts.Requests.Product;
using SBThub.Application.UseCases.Products.UpdateProduct;

namespace SBThub.WebApi.Controllers;

[Route("api/products")]
public sealed class ProductsController(ISender sender) : BaseApiController(sender)
{
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new CreateProductCommand(request), cancellationToken);
        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }
    
    [HttpGet("{uuid:guid}/creator")]
    public async Task<IActionResult> GetProductCreator(Guid uuid, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetProductCreatorQuery(uuid), cancellationToken);
        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }
    
    [HttpGet("{uuid:guid}/products")]
    public async Task<IActionResult> ShowProductsCreatedByUser(Guid uuid, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new ShowProductsCreatedByUserQuery(uuid), cancellationToken);
        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }
    
    [HttpPut("{uuid:guid}")]
    public async Task<IActionResult> UpdateUser(Guid uuid, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new UpdateProductCommand(uuid, request), cancellationToken);
        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }
    
    [HttpDelete("{uuid:guid}")]
    public async Task<IActionResult> DeleteProduct(Guid uuid, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new DeleteProductCommand(uuid), cancellationToken);
        return result.IsFailure ? HandleFailure(result) : NoContent();
    }
}