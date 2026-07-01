using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Requests;
using SBThub.Application.Contracts.Responses;

namespace SBThub.Application.UseCases.Guests.CreateGuest;


public sealed record CreateUserCommand(CreateUserRequest Request) : ICommand<GuestResponse>;
