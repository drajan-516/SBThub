using SBThub.Domain.Common;
using SBThub.Domain.Shared;
using SBThub.Domain.ValueObjects;

namespace SBThub.Domain.Entities;

public sealed class User : Entity
{
    private User() { }

    private User(Guid uuid, UserName fullName, string? phone) : base(uuid)
    {
        FullName = fullName;
        Phone = phone;
    }

    public UserName FullName { get; private set; } = null!;

    public string? Phone { get; private set; }
    
    public static ResultResponse Create(string? fullName, string? phone)
    {
        var nameResult = UserName.Create(fullName);
        if (nameResult.IsFailure)
            return ResultResponse.Failure<User>(nameResult.Error);

        return ResultResponse.Success(new User(Guid.NewGuid(), nameResult.Value, phone));
    }
}