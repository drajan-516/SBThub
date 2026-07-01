using HotelDemo.Application.Abstractions.Messaging;
using HotelDemo.Application.Contracts.Responses;
using HotelDemo.Application.Mapping;
using HotelDemo.Domain.Repositories;
using HotelDemo.Domain.Shared;

namespace HotelDemo.Application.UseCases.Guests.GetGuests;

internal sealed class GetGuestsHandler(IGuestRepository guests)
    : IQueryHandler<GetGuestsQuery, IReadOnlyList<GuestResponse>>
{
    public async Task<ResultResponse<IReadOnlyList<GuestResponse>>> Handle(GetGuestsQuery query, CancellationToken cancellationToken)
    {
        var allGuests = await guests.GetAllAsync(cancellationToken);
        IReadOnlyList<GuestResponse> response = allGuests.Select(guest => guest.ToResponse()).ToList();
        return ResultResponse.Success(response);
    }
}
