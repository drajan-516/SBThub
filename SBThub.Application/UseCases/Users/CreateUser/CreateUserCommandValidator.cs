using FluentValidation;
using SBThub.Domain.ValueObjects;

namespace SBThub.Application.UseCases.Users.CreateUser;

internal sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(command => command.Request.FullName)
            .NotEmpty().WithMessage("Имя юзера обязательно.")
            .MaximumLength(UserName.MaxLength);
    }
}
