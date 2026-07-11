using SBThub.Domain.Shared;
using SBThub.Domain.ValueObjects;
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
}
