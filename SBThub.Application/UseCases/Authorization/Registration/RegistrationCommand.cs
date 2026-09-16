using SBThub.Application.Abstractions.Messaging;
using SBThub.Application.Contracts.Contracts.Requests.Registration;
using SBThub.Application.Contracts.Contracts.Responses;


namespace SBThub.Application.UseCases.Authorization.Registration;

public sealed record RegistrationCommand(RegistrationRequest Request) : ICommand<RegistrationResponse>;