using Reservera.DTOs;
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

    public async Task<List<ReservationResponse>> GetAll()
    {
        var reservations = await _reservationRepository.GetAll();
        return reservations.Select(r => ToResponse(r)).ToList();
    }

    public async Task<ReservationResponse> GetById(int id)
    {
        var reservation = await _reservationRepository.GetById(id);
        if (reservation is null) throw new NotFoundException($"Id={id} olan rezervasyon bulunamadı.");
        return ToResponse(reservation);
    }

    public async Task<ReservationResponse> Create(CreateReservationRequest request)
    {
        var room = await _roomRepository.GetById(request.RoomId);
        if (room is null) throw new NotFoundException($"Id={request.RoomId} olan oda bulunamadı.");

        var hasOverlap = await _reservationRepository.HasOverlap(
            request.RoomId, request.CheckIn, request.CheckOut);
        if (hasOverlap) throw new RoomNotAvailableException("Bu oda seçilen tarihlerde dolu.");

        var nights = (request.CheckOut - request.CheckIn).Days;

        var reservation = new Reservation
        {
            RoomId = request.RoomId,
            GuestName = request.GuestName,
            CheckIn = request.CheckIn,
            CheckOut = request.CheckOut,
            TotalPrice = room.PricePerNight * nights
        };

        var created = await _reservationRepository.Add(reservation);
        return ToResponse(created, room.Name);
    }

    public async Task Cancel(int id)
    {
        var cancelled = await _reservationRepository.Cancel(id);
        if (!cancelled) throw new NotFoundException($"Id={id} olan rezervasyon bulunamadı.");
    }

    private static ReservationResponse ToResponse(Reservation r, string roomName = "") => new()
    {
        Id = r.Id,
        RoomId = r.RoomId,
        RoomName = roomName,
        GuestName = r.GuestName,
        CheckIn = r.CheckIn,
        CheckOut = r.CheckOut,
        TotalPrice = r.TotalPrice,
        Status = r.Status.ToString()
    };
}
