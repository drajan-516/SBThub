namespace SBThub.Application.Contracts.Responses;

public sealed record UserResponse(Guid Uuid, string FullName, string? Phone);
