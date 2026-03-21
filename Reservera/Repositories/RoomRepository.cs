using Microsoft.EntityFrameworkCore;
using Reservera.Data;
using Reservera.Models;

namespace Reservera.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly ReserveraDbContext _context;

    public RoomRepository(ReserveraDbContext context)
    {
        _context = context;
    }

    public async Task<List<Room>> GetAll()
        => await _context.Rooms.ToListAsync();

    public async Task<Room?> GetById(int id)
        => await _context.Rooms.FindAsync(id);

    public async Task<Room> Add(Room room)
    {
        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();
        return room;
    }

    public async Task<Room?> Update(Room updated)
    {
        var existing = await GetById(updated.Id);
        if (existing is null) return null;

        existing.Name = updated.Name;
        existing.Description = updated.Description;
        existing.PricePerNight = updated.PricePerNight;
        existing.Capacity = updated.Capacity;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> Delete(int id)
    {
        var room = await GetById(id);
        if (room is null) return false;

        _context.Rooms.Remove(room);
        await _context.SaveChangesAsync();
        return true;
    }
}
