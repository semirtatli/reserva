using Reservera.Exceptions;
using Reservera.Models;
using Reservera.Repositories;

namespace Reservera.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IRoomRepository _roomRepository;

    public ReservationService(IReservationRepository reservationRepository, IRoomRepository roomRepository)
    {
        _reservationRepository = reservationRepository;
        _roomRepository = roomRepository;
    }

    public async Task<List<Reservation>> GetAll()
        => await _reservationRepository.GetAll();

    public async Task<Reservation> GetById(int id)
    {
        var reservation = await _reservationRepository.GetById(id);
        if (reservation is null) throw new NotFoundException($"Id={id} olan rezervasyon bulunamadı.");
        return reservation;
    }

    public async Task<Reservation> Create(Reservation reservation)
    {
        var room = await _roomRepository.GetById(reservation.RoomId);
        if (room is null) throw new NotFoundException($"Id={reservation.RoomId} olan oda bulunamadı.");

        var hasOverlap = await _reservationRepository.HasOverlap(
            reservation.RoomId, reservation.CheckIn, reservation.CheckOut);
        if (hasOverlap) throw new RoomNotAvailableException("Bu oda seçilen tarihlerde dolu.");

        var nights = (reservation.CheckOut - reservation.CheckIn).Days;
        reservation.TotalPrice = room.PricePerNight * nights;

        return await _reservationRepository.Add(reservation);
    }

    public async Task Cancel(int id)
    {
        var cancelled = await _reservationRepository.Cancel(id);
        if (!cancelled) throw new NotFoundException($"Id={id} olan rezervasyon bulunamadı.");
    }
}
