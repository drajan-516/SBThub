using SBThub.Domain.Common;
using SBThub.Domain.Errors;
using SBThub.Domain.Shared;
using SBThub.Domain.ValueObjects.User;

namespace SBThub.Domain.Entities;

public sealed class User : Entity
{
    private User() { }
    
    public UserName FullName { get; private set; } = null!;
    public string? Phone { get; private set; }
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;

    private User(Guid uuid, UserName fullName, string? phone, string email, string passwordHash) : base(uuid)
    {
        FullName = fullName;
        Phone = phone;
        Email = email;
        PasswordHash = passwordHash;
    }
    
    public static ResultResponse<User> Create(string? fullName, string? phone, string? email, string? passwordHash)
    {
        var nameResult = UserName.Create(fullName);
        if (nameResult.IsFailure)
            return ResultResponse.Failure<User>(nameResult.Error);

        if (string.IsNullOrWhiteSpace(email))
            return ResultResponse.Failure<User>(UserErrors.EmailRequired);

        if (string.IsNullOrWhiteSpace(passwordHash))
            return ResultResponse.Failure<User>(UserErrors.PasswordRequired);

        return ResultResponse.Success(new User(Guid.NewGuid(), nameResult.Value, phone, email, passwordHash));
    }

    public ResultResponse Update(string? fullName, string? phone)
    {
        var nameResult = UserName.Create(fullName);
        if (nameResult.IsFailure) return ResultResponse.Failure(nameResult.Error);
        FullName = nameResult.Value;
        Phone = phone;
        
        return ResultResponse.Success();
    }
}