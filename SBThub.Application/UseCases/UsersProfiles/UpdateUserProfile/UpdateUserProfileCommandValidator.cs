using FluentValidation;
using SBThub.Domain.ValueObjects;
using SBThub.Domain.ValueObjects.User;

namespace SBThub.Application.UseCases.UserProfiles.UpdateUserProfile;

internal sealed class UpdateUserProfileCommandValidator : AbstractValidator<UpdateUserProfileCommand>
{
    public UpdateUserProfileCommandValidator()
    {
        RuleFor(command => command.Request.FullName)
            .MaximumLength(UserName.MaxLength)
            .When(command => !string.IsNullOrEmpty(command.Request.FullName));
        
        RuleFor(command => command.Request.Phone)
            .Matches(@"^\+?\d{9,15}$").WithMessage("Некорректный формат номера телефона.")
            .When(command => !string.IsNullOrEmpty(command.Request.Phone));

        RuleFor(command => command.Request.Email)
            .EmailAddress().WithMessage("Некорректный формат почты.")
            .When(command => !string.IsNullOrEmpty(command.Request.Email));

        RuleFor(command => command.Request.AvatarUrl)
            .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
            .WithMessage("Некорректный формат ссылки на аватар.")
            .When(command => !string.IsNullOrEmpty(command.Request.AvatarUrl));
    }
}