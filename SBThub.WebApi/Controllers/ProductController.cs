using SBThub.Application.UseCases.Products.CreateProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SBThub.Application.Contracts.Contracts.Requests.Product;
using SBThub.Application.Contracts.Requests.Product;
using SBThub.Application.UseCases.Products.UpdateProduct;
using SBThub.Application.UseCases.Users.GetUserByUuid;
using Swashbuckle.AspNetCore.Annotations;

namespace SBThub.WebApi.Controllers;

[Route("api/products")]
public sealed class ProductsController(ISender sender) : BaseApiController(sender)
{   
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [SwaggerResponse(201, "Product was successfully created.")]
    [SwaggerResponse(400, "Invalid request. Ensure the provided data is correct.", typeof(CreateProductRequest))]
    [SwaggerResponse(409, "Conflict. Product with similar details already exists.", typeof(CreateProductRequest))]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new CreateProductCommand(request), cancellationToken);
        return result.IsFailure ? HandleFailure(result) : NoContent();
    }
    
    
    [HttpPut("{uuid:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerResponse(204, "Product updated successfully.")]
    [SwaggerResponse(400, "Invalid request. Ensure the provided data is correct.", typeof(CreateProductRequest))]
    [SwaggerResponse(404, "Product not found.")]
    public async Task<IActionResult> UpdateProduct(Guid uuid, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new UpdateProductCommand(uuid, request), cancellationToken);
        return result.IsFailure ? HandleFailure(result) : NoContent();
    }
}