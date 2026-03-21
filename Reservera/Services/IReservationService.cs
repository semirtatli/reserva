using Reservera.Models;

namespace Reservera.Services;

public interface IReservationService
{
    Task<List<Reservation>> GetAll();
    Task<Reservation> GetById(int id);
    Task<Reservation> Create(Reservation reservation);
    Task Cancel(int id);
}
