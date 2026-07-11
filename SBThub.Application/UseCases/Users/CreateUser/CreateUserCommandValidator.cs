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
    }
}
