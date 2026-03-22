using Microsoft.EntityFrameworkCore;
using Reservera.Data;
using Reservera.Models;

namespace Reservera.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly ReserveraDbContext _context;

    public ReservationRepository(ReserveraDbContext context)
    {
        _context = context;
    }

    public async Task<List<Reservation>> GetAll(int? userId = null)
    {
        var query = _context.Reservations.AsQueryable();
        if (userId.HasValue)
            query = query.Where(r => r.UserId == userId.Value);
        return await query.ToListAsync();
    }

    public async Task<Reservation?> GetById(int id)
        => await _context.Reservations.FindAsync(id);

    public async Task<Reservation> Add(Reservation reservation)
    {
        reservation.Status = ReservationStatus.Confirmed;
        _context.Reservations.Add(reservation);
        await _context.SaveChangesAsync();
        return reservation;
    }

    public async Task<bool> Cancel(int id)
    {
        var reservation = await GetById(id);
        if (reservation is null) return false;

        reservation.Status = ReservationStatus.Cancelled;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> HasOverlap(int roomId, DateTime checkIn, DateTime checkOut)
        => await _context.Reservations.AnyAsync(r =>
            r.RoomId == roomId &&
            r.Status == ReservationStatus.Confirmed &&
            r.CheckIn < checkOut &&
            r.CheckOut > checkIn);
}
