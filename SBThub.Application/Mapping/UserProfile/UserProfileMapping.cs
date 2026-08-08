using SBThub.Application.Contracts.Responses;
using SBThub.Domain.Entities;

namespace SBThub.Application.Mapping;

public static class UserProfileMappings
{
    public static UserProfileResponse ToResponse(this UserProfile userprofile) =>
        new(userprofile.Uuid, userprofile.FullName.Value, userprofile.Phone, userprofile.Email);
}