using SBThub.Application.Contracts.Requests;
using SBThub.Application.UseCases.Users.CreateUser;
using SBThub.Application.UseCases.Users.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SBThub.Application.Contracts.Requests.User;
using Swashbuckle.AspNetCore.Annotations;

namespace SBThub.WebApi.Controllers;

[Route("api/users")]
public sealed class UsersController(ISender sender) : BaseApiController(sender)
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [SwaggerResponse(201, "User was successfully created.")]
    [SwaggerResponse(400, "Invalid request. Ensure the provided data is correct.", typeof(CreateUserRequest))]
    [SwaggerResponse(409, "Conflict. User with similar details already exists.", typeof(CreateUserRequest))]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new CreateUserCommand(request), cancellationToken);
        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(200, "Users were successfully retrieved.")]
    [SwaggerResponse(400, "Invalid request.", typeof(CreateUserRequest))]
    public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetUsersQuery(), cancellationToken);
        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }
}
