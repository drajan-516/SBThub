namespace SBThub.Application.Contracts.Contracts.Requests.Registration;

public sealed record RegistrationRequest(string FullName, string? Phone, string? Email, string? Password);