using Reservera.Models;

namespace Reservera.Services;

public interface IRoomService
{
    Task<List<Room>> GetAll();
    Task<Room> GetById(int id);
    Task<Room> Create(Room room);
    Task Delete(int id);
}
