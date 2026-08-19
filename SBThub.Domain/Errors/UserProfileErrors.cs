using SBThub.Domain.Shared;
using SBThub.Domain.ValueObjects.UserProfile;

namespace SBThub.Domain.Errors;

public static class UserProfileErrors
{
    public static readonly Error NotFound =
        Error.NotFound("UserProfile.NotFound", "Профиль пользователя не найден");

    public static readonly Error AlreadyExists =
        Error.Conflict("UserProfile.AlreadyExists", "Профиль пользователя уже существует");

    public static Error InvalidUserId(Guid userId) =>
        Error.Validation("UserProfile.InvalidUserId", $"Некорректный идентификатор пользователя: {userId}");

    public static readonly Error UpdateFailed =
        Error.Failure("UserProfile.UpdateFailed", "Не удалось обновить профиль пользователя");
    
    public static readonly Error NameRequired =
        Error.Validation("UserProfile.NameRequired", "Имя пользователя обязательно.");

    public static readonly Error NameTooLong =
        Error.Validation("UserProfile.NameTooLong", $"Имя пользователя не длиннее {UserProfileName.MaxLength} символов.");
    
    public static readonly Error PhoneRequired =
        Error.Validation("UserProfile.PhoneRequired", "Номер телефона обязателен.");

    public static readonly Error PhoneInvalidFormat =
        Error.Validation("UserProfile.PhoneInvalidFormat", "Некорректный формат номера телефона.");

    public static readonly Error PhoneAlreadyExists =
        Error.Conflict("UserProfile.PhoneAlreadyExists", "Этот номер телефона уже зарегистрирован.");

    public static readonly Error EmailRequired =
        Error.Validation("UserProfile.EmailRequired", "Електронная почта обязательна");

    public static readonly Error EmailInvalidFormat =
        Error.Validation("UserProfile.EmailInvalidFormat", "Некорректный формат електронной почты");

    public static readonly Error EmailAlreadyExists =
        Error.Conflict("UserProfile.EmailAlreadyExists", "Эта електронная почта уже занята");

}
