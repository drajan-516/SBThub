using FluentValidation;
using SBThub.Domain.ValueObjects;
using SBThub.Domain.ValueObjects.UserProfile;

namespace SBThub.Application.UseCases.UserProfiles.CreateUserProfile;

internal sealed class CreateUserProfileCommandValidator : AbstractValidator<CreateUserProfileCommand>;