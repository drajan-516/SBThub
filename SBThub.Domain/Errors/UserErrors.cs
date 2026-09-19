using SBThub.Domain.Shared;
using SBThub.Domain.ValueObjects.User;

namespace SBThub.Domain.Errors;

public static class UserErrors
{
    public static readonly Error NameRequired =
        Error.Validation("User.NameRequired", "Имя пользователя обязательно.");

    public static readonly Error NameTooLong =
        Error.Validation("User.NameTooLong", $"Имя пользователя не длиннее {UserName.MaxLength} символов.");

    public static readonly Error NotFound =
        Error.NotFound("User.NotFound", "Пользователь не найден.");
    
    public static readonly Error InvalidCredentials = Error.Validation(
        "User.InvalidCredentials", 
        "Неверный email или пароль.");
    // Стоило ли писать такую ошибку...
    public static readonly Error InvalidRefreshToken = Error.Validation(
        "User.InvalidRefreshToken", 
        "Недействительный refresh-токен.");
    
    public static readonly Error EmailRequired = Error.Validation(
        "User.EmailRequired", 
        "Email обязателен.");
    
    public static readonly Error PasswordRequired = Error.Validation(
        "User.PasswordRequired",
        "Пароль обязателен.");

    public static readonly Error EmailAlreadyInUse = Error.Validation(
        "User.EmailAlreadyInUse",
        "Почта уже занята.");
}
