using FluentValidation;
using SBThub.Domain.ValueObjects;

namespace SBThub.Application.UseCases.Users.UpdateUser;

internal sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(command => command.Uuid)
            .NotEmpty().WithMessage("Идентификатор пользователя обязательно.");
        RuleFor(command => command.Request.FullName)
            .NotEmpty().WithMessage("Имя юзера обязательно.")
            .MaximumLength(UserName.MaxLength);
    }
}
