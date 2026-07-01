using HotelDemo.Application.Abstractions.Messaging;
using HotelDemo.Application.Contracts.Responses;

namespace HotelDemo.Application.UseCases.Guests.GetGuests;

/// <summary>Запрос: получить список всех гостей.</summary>
public sealed record GetGuestsQuery : IQuery<IReadOnlyList<GuestResponse>>;
