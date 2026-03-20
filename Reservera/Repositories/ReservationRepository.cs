using Reservera.Models;

namespace Reservera.Repositories;

public class ReservationRepository
{
    private static readonly List<Reservation> _reservations = new();
    private static int _nextId = 1;

    public List<Reservation> GetAll() => _reservations;

    public Reservation? GetById(int id) => _reservations.FirstOrDefault(r => r.Id == id);

    public Reservation Add(Reservation reservation)
    {
        reservation.Id = _nextId++;
        reservation.Status = ReservationStatus.Confirmed;
        _reservations.Add(reservation);
        return reservation;
    }

    public bool Cancel(int id)
    {
        var reservation = GetById(id);
        if (reservation is null) return false;

        reservation.Status = ReservationStatus.Cancelled;
        return true;
    }

    // Verilen oda ve tarih aralığında çakışan rezervasyon var mı?
    public bool HasOverlap(int roomId, DateTime checkIn, DateTime checkOut)
    {
        return _reservations.Any(r =>
            r.RoomId == roomId &&
            r.Status == ReservationStatus.Confirmed &&
            r.CheckIn < checkOut &&
            r.CheckOut > checkIn);
    }
}
