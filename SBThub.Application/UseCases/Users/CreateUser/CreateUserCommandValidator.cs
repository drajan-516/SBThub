using FluentValidation;
using SBThub.Domain.ValueObjects;
using SBThub.Domain.ValueObjects.User;

namespace SBThub.Application.UseCases.Users.CreateUser;

internal sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(command => command.Request.FullName)
            .NotEmpty().WithMessage("Имя юзера обязательно.")
            .MaximumLength(UserName.MaxLength);
        
        RuleFor(command => command.Request.Phone)
            .NotEmpty().WithMessage("Номер телефона обязательно.")
            .Matches(@"^\+?\d{9,15}$").WithMessage("Некорректный формат номера телефона.");

        RuleFor(command => command.Request.Email)
            .NotEmpty().WithMessage("Электронная почта обязательна.")
            .EmailAddress().WithMessage("Некорректный формат электронной почты.")
            .MaximumLength(320).WithMessage("Электронная почта не должна превышать 320 символов.");
        
        RuleFor(command => command.Request.Password)
            .NotEmpty().WithMessage("Пароль обязателен.")
            .MinimumLength(8).WithMessage("Пароль должен содержать минимум 8 символов.");
    }
}
