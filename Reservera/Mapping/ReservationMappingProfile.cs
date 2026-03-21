using AutoMapper;
using Reservera.DTOs;
using Reservera.Models;

namespace Reservera.Mapping;

public class ReservationMappingProfile : Profile
{
    public ReservationMappingProfile()
    {
        // CreateReservationRequest → Reservation
        CreateMap<CreateReservationRequest, Reservation>();

        // Reservation → ReservationResponse
        // Status enum → string dönüşümü özel tanımlanıyor
        CreateMap<Reservation, ReservationResponse>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.RoomName, opt => opt.Ignore());
    }
}
