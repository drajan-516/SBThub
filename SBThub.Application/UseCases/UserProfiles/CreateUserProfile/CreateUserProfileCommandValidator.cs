using FluentValidation;
using SBThub.Domain.ValueObjects;
using SBThub.Domain.ValueObjects.User;

namespace SBThub.Application.UseCases.UserProfiles.CreateUserProfile;

internal sealed class CreateUserProfileCommandValidator : AbstractValidator<CreateUserProfileCommand>
{
    public CreateUserProfileCommandValidator()
    {
        RuleFor(command => command.Request.FullName)
            .NotEmpty().WithMessage("Имя юзера обязательно.")
            .MaximumLength(UserName.MaxLength);
        
        RuleFor(command => command.Request.Phone)
            .NotEmpty().WithMessage("Номер телефона обязательно.")
            .Matches(@"^\+?\d{9,15}$").WithMessage("Некорректный формат номера телефона.");

        RuleFor(command => command.Request.Email)
            .NotEmpty().WithMessage("Почта обязательна.")
            .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$").WithMessage("Некорректный формат почты.");
    }
}