using SBThub.Domain.Common;
using SBThub.Domain.Shared;
using SBThub.Domain.ValueObjects.User;

namespace SBThub.Domain.Entities;

public sealed class User : Entity
{
    private User() { }
    
    public UserName FullName { get; private set; } = null!;
    public string? Phone { get; private set; }

    private User(Guid uuid, UserName fullName, string? phone) : base(uuid)
    {
        FullName = fullName;
        Phone = phone;
    }
    
    public static ResultResponse<User> Create(string? fullName, string? phone)
    {
        var nameResult = UserName.Create(fullName);
        if (nameResult.IsFailure)
            return ResultResponse.Failure<User>(nameResult.Error);

        return ResultResponse.Success(new User(Guid.NewGuid(), nameResult.Value, phone));
    }

    public ResultResponse Update(string? fullName, string? phone)
    {
        var nameResult = UserName.Create(fullName);
        if (nameResult.IsFailure) return ResultResponse.Failure(nameResult.Error);
        FullName = nameResult.Value;
        Phone = phone;
        
        return  ResultResponse.Success();
    }
}