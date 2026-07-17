
using SBThub.Application.UseCases.Products.CreateProduct;
using SBThub.Application.UseCases.Products.GetProductCreator;
using SBThub.Application.UseCases.Products.ShowProductsCreatedByUser;
using SBThub.Application.UseCases.Products.DeleteProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SBThub.Application.Contracts.Contracts.Requests.Product;
using SBThub.Application.Contracts.Requests.Product;
using SBThub.Application.UseCases.Products.UpdateProduct;
using Swashbuckle.AspNetCore.Annotations;

namespace SBThub.WebApi.Controllers;

[Route("api/products")]
public sealed class ProductsController(ISender sender) : BaseApiController(sender)
{
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
    
    // TODO : replace create date from entity
    // and remove from request
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [SwaggerResponse(201, "Organization was successfully created.")]
    [SwaggerResponse(400, "Invalid request. Ensure the provided data is correct.", typeof(CreateProductRequest))]
    [SwaggerResponse(409, "Conflict. Organization with similar details already exists.", typeof(CreateProductRequest))]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new CreateProductCommand(request), cancellationToken);
        return result.IsFailure ? HandleFailure(result) : NoContent();
    }
    
    
    [HttpPut("{uuid:guid}")]
    public async Task<IActionResult> UpdateProduct(Guid uuid, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new UpdateProductCommand(uuid, request), cancellationToken);
        return result.IsFailure ? HandleFailure(result) : NoContent();
    }
    
    [HttpDelete("{uuid:guid}")]
    public async Task<IActionResult> DeleteProduct(Guid uuid, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new DeleteProductCommand(uuid), cancellationToken);
        return result.IsFailure ? HandleFailure(result) : NoContent();
    }
}