using SBThub.Application.Contracts.Requests.UserProfile;
using SBThub.Application.UseCases.UsersProfiles.CreateUserProfile;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;


namespace SBThub.WebApi.Controllers;

[Route("api/usersProfiles")]
public sealed class UsersProfilesController(ISender sender) : BaseApiController(sender)
{
    [HttpPost("{userId:guid}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [SwaggerResponse(201, "User profile was successfully created.")]
    [SwaggerResponse(400, "Invalid request. Ensure the provided data is correct.", typeof(CreateUserProfileRequest))]
    [SwaggerResponse(409, "Conflict. User Profile with similar details already exists.", typeof(CreateUserProfileRequest))]
    public async Task<IActionResult> CreateUserProfile(Guid userId, [FromBody] CreateUserProfileRequest request, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new CreateUserProfileCommand(userId, request), cancellationToken);
        return result.IsFailure ? HandleFailure(result) : Ok(result.Value);
    }
}
