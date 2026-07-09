using SBThub.Application.Contracts.Responses;
using SBThub.Domain.Entities;

namespace SBThub.Application.Mapping;

public static class UserMappings
{
    public static UserResponse ToResponse(this User user) =>
        new(user.Uuid, user.FullName.Value, user.Phone);
}
