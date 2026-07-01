using SBThub.Domain.Shared;
using SBThub.Domain.ValueObjects;

namespace SBThub.Domain.Errors;

public static class UserErrors
{
    public static readonly Error NameRequired =
        Error.Validation("Guest.NameRequired", "Имя гостя обязательно.");

    public static readonly Error NameTooLong =
        Error.Validation("Guest.NameTooLong", $"Имя гостя не длиннее {UserName.MaxLength} символов.");

    public static readonly Error NotFound =
        Error.NotFound("Guest.NotFound", "Гость не найден.");
}
