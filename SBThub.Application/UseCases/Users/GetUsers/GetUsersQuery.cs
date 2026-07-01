using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Responses;

namespace SBThub.Application.UseCases.Users.GetUsers;

public sealed record GetUsersQuery : IQuery<IReadOnlyList<UserResponse>>;
