using Reservera.Models;

namespace Reservera.Repositories;

public interface IRoomRepository
{
    Task<List<Room>> GetAll();
    Task<Room?> GetById(int id);
    Task<Room> Add(Room room);
    Task<Room?> Update(Room updated);
    Task<bool> Delete(int id);
}
