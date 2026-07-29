using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Responses;

namespace SBThub.Application.UseCases.Users.GetUserByUuid;

public sealed record GetUserByUuidQuery(Guid Uuid) : IQuery<UserResponse>;