using FluentValidation;
using SBThub.Domain.ValueObjects;
using SBThub.Domain.ValueObjects.UserProfile;

namespace SBThub.Application.UseCases.UsersProfiles.CreateUserProfile;

internal sealed class CreateUserProfileCommandValidator : AbstractValidator<CreateUserProfileCommand>;