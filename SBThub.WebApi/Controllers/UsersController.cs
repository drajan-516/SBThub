using SBThub.Application.Contracts.Requests;
using SBThub.Application.UseCases.Users.CreateUser;
using SBThub.Application.UseCases.Users.GetUsers;
using SBThub.Application.UseCases.Users.UpdateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SBThub.Application.Contracts.Requests.User;

namespace SBThub.WebApi.Controllers;

[Route("api/users")]
public sealed class UsersController(ISender sender) : BaseApiController(sender)
{
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new CreateUserCommand(request), cancellationToken);
        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetUsersQuery(), cancellationToken);
        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }
    
    [HttpPut("{uuid:guid}")]
    public async Task<IActionResult> UpdateUser(Guid uuid, [FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new UpdateUserCommand(uuid, request), cancellationToken);
        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }
}
