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
    [HttpGet("{uuid:guid}")]
    public async Task<IActionResult> GetUserByUuid(Guid uuid, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetUserByUuidQuery(uuid), cancellationToken);
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
}