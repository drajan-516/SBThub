using FluentValidation;
using SBThub.Domain.ValueObjects;

namespace SBThub.Application.UseCases.Guests.CreateGuest;

internal sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(command => command.Request.FullName)
            .NotEmpty().WithMessage("Имя гостя обязательно.")
            .MaximumLength(UserName.MaxLength);
    }
}
