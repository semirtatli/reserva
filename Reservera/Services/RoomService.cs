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

    public async Task<List<Room>> GetAll()
        => await _repository.GetAll();

    public async Task<Room> GetById(int id)
    {
        var room = await _repository.GetById(id);
        if (room is null) throw new NotFoundException($"Id={id} olan oda bulunamadı.");
        return room;
    }

    public async Task<Room> Create(Room room)
        => await _repository.Add(room);

    public async Task Delete(int id)
    {
        var deleted = await _repository.Delete(id);
        if (!deleted) throw new NotFoundException($"Id={id} olan oda bulunamadı.");
    }
}
