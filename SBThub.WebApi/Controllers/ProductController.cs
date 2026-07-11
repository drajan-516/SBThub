
using SBThub.Application.UseCases.Products.CreateProduct;
using SBThub.Application.UseCases.Products.GetProductCreator;
// using SBThub.Application.UseCases.Products.UpdateUser;
// using SBThub.Application.UseCases.Products.DeleteUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SBThub.Application.Contracts.Requests.Product;

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
}