using Reservera.Models;

namespace Reservera.Repositories;

public interface IReservationRepository
{
    Task<List<Reservation>> GetAll(int? userId = null);
    Task<Reservation?> GetById(int id);
    Task<Reservation> Add(Reservation reservation);
    Task<bool> Cancel(int id);
    Task<bool> HasOverlap(int roomId, DateTime checkIn, DateTime checkOut);
}
