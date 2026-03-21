using Reservera.DTOs;
using Reservera.Exceptions;
using Reservera.Models;
using Reservera.Repositories;

namespace Reservera.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _repository;

    public RoomService(IRoomRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<RoomResponse>> GetAll()
    {
        var rooms = await _repository.GetAll();
        return rooms.Select(ToResponse).ToList();
    }

    public async Task<RoomResponse> GetById(int id)
    {
        var room = await _repository.GetById(id);
        if (room is null) throw new NotFoundException($"Id={id} olan oda bulunamadı.");
        return ToResponse(room);
    }

    public async Task<RoomResponse> Create(CreateRoomRequest request)
    {
        var room = new Room
        {
            Name = request.Name,
            Description = request.Description,
            PricePerNight = request.PricePerNight,
            Capacity = request.Capacity
        };

        var created = await _repository.Add(room);
        return ToResponse(created);
    }

    public async Task Delete(int id)
    {
        var deleted = await _repository.Delete(id);
        if (!deleted) throw new NotFoundException($"Id={id} olan oda bulunamadı.");
    }

    private static RoomResponse ToResponse(Room room) => new()
    {
        Id = room.Id,
        Name = room.Name,
        Description = room.Description,
        PricePerNight = room.PricePerNight,
        Capacity = room.Capacity
    };
}
