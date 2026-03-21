using Microsoft.EntityFrameworkCore;
using Reservera.Data;
using Reservera.Models;

namespace Reservera.Repositories;

public class ReservationRepository
{
    private readonly ReserveraDbContext _context;

    public ReservationRepository(ReserveraDbContext context)
    {
        _context = context;
    }

    public async Task<List<Reservation>> GetAll()
        => await _context.Reservations.ToListAsync();

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
