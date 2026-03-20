using Reservera.Models;

namespace Reservera.Repositories;

public class RoomRepository
{
    private static readonly List<Room> _rooms = new()
    {
        new() { Id = 1, Name = "Deniz Manzaralı Suite", Description = "Muhteşem deniz manzarası", PricePerNight = 250.00m, Capacity = 2 },
        new() { Id = 2, Name = "Bahçe Odası", Description = "Sakin bahçe görünümlü oda", PricePerNight = 120.00m, Capacity = 2 },
        new() { Id = 3, Name = "Aile Odası", Description = "Geniş aile odası", PricePerNight = 180.00m, Capacity = 4 }
    };

    private static int _nextId = 4;

    public List<Room> GetAll() => _rooms;

    public Room? GetById(int id) => _rooms.FirstOrDefault(r => r.Id == id);

    public Room Add(Room room)
    {
        room.Id = _nextId++;
        _rooms.Add(room);
        return room;
    }

    public Room? Update(Room updated)
    {
        var existing = GetById(updated.Id);
        if (existing is null) return null;

        existing.Name = updated.Name;
        existing.Description = updated.Description;
        existing.PricePerNight = updated.PricePerNight;
        existing.Capacity = updated.Capacity;

        return existing;
    }

    public bool Delete(int id)
    {
        var room = GetById(id);
        if (room is null) return false;

        _rooms.Remove(room);
        return true;
    }
}
