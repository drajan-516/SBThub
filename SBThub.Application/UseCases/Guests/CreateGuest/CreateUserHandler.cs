using HotelDemo.Application.Abstractions.Messaging;
using HotelDemo.Application.Contracts.Responses;
using HotelDemo.Application.Mapping;
using HotelDemo.Domain.Entities;
using HotelDemo.Domain.Repositories;
using HotelDemo.Domain.Shared;
using SBThub.Application.UseCases.Guests.CreateGuest;
using SBThub.Domain.Repositories;

namespace HotelDemo.Application.UseCases.Guests.CreateGuest;

internal sealed class CreateUserHandler(IUserRepository users, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateUserCommand, UserResponse>
{
    public async Task<ResultResponce<UserResponse>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var userResult = Guest.Create(command.Request.FullName, command.Request.Phone);
        if (userResult.IsFailure)
            return ResultResponse.Failure<UserResponse>(userResult.Error);

        user.Add(userResult.Value);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ResultResponse.Success(userResult.Value.ToResponse());
    }
}
