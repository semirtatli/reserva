using Reservera.DTOs;

namespace Reservera.Services;

public interface IRoomService
{
    Task<List<RoomResponse>> GetAll();
    Task<RoomResponse> GetById(int id);
    Task<RoomResponse> Create(CreateRoomRequest request);
    Task Delete(int id);
}
