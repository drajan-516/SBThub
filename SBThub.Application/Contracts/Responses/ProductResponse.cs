namespace SBThub.Application.Contracts.Responses;

//do we need creator id here??
public sealed record ProductResponse(Guid Uuid, string ProductTitle, string Description,  decimal Price, DateTime CreatedOn);