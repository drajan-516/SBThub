using SBThub.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SBThub.WebApi.Controllers;

[ApiController]
public abstract class BaseApiController(ISender sender) : ControllerBase
{
    protected ISender Sender { get; } = sender;

    protected IActionResult HandleFailure(ResultResponse result)
    {
        if (result.IsSuccess)
            throw new InvalidOperationException("HandleFailure нельзя вызывать для успешного результата.");

        var statusCode = result.Error.Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status400BadRequest
        };

        var problem = new ProblemDetails
        {
            Status = statusCode,
            Title = result.Error.Code,
            Detail = result.Error.Description
        };

        problem.Extensions["errors"] = result.Errors
            .Select(error => new { error.Code, error.Description })
            .ToArray();

        return new ObjectResult(problem) { StatusCode = statusCode };
    }
}
