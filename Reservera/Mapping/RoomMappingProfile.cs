using AutoMapper;
using Reservera.DTOs;
using Reservera.Models;

namespace Reservera.Mapping;

public class RoomMappingProfile : Profile
{
    public RoomMappingProfile()
    {
        // Room → RoomResponse: isimler aynı, otomatik map
        CreateMap<Room, RoomResponse>();

        // CreateRoomRequest → Room: Id yok, geri kalanı aynı isimde
        CreateMap<CreateRoomRequest, Room>();
    }
}
