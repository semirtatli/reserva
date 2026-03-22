using AutoMapper;
using Reservera.DTOs;
using Reservera.Exceptions;
using Reservera.Models;
using Reservera.Repositories;

namespace Reservera.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IMapper _mapper;

    public ReservationService(
        IReservationRepository reservationRepository,
        IRoomRepository roomRepository,
        IMapper mapper)
    {
        _reservationRepository = reservationRepository;
        _roomRepository = roomRepository;
        _mapper = mapper;
    }

    public async Task<List<ReservationResponse>> GetAll(int? userId = null)
    {
        var reservations = await _reservationRepository.GetAll(userId);
        return _mapper.Map<List<ReservationResponse>>(reservations);
    }

    public async Task<ReservationResponse> GetById(int id)
    {
        var reservation = await _reservationRepository.GetById(id);
        if (reservation is null) throw new NotFoundException($"Id={id} olan rezervasyon bulunamadı.");
        return _mapper.Map<ReservationResponse>(reservation);
    }

    public async Task<ReservationResponse> Create(CreateReservationRequest request, int userId)
    {
        var room = await _roomRepository.GetById(request.RoomId);
        if (room is null) throw new NotFoundException($"Id={request.RoomId} olan oda bulunamadı.");

        var hasOverlap = await _reservationRepository.HasOverlap(
            request.RoomId, request.CheckIn, request.CheckOut);
        if (hasOverlap) throw new RoomNotAvailableException("Bu oda seçilen tarihlerde dolu.");

        var nights = (request.CheckOut - request.CheckIn).Days;
        var reservation = _mapper.Map<Reservation>(request);
        reservation.UserId = userId;
        reservation.TotalPrice = room.PricePerNight * nights;

        var created = await _reservationRepository.Add(reservation);

        var response = _mapper.Map<ReservationResponse>(created);
        response.RoomName = room.Name;
        return response;
    }

    public async Task Cancel(int id, int userId, bool isAdmin)
    {
        var reservation = await _reservationRepository.GetById(id);
        if (reservation is null) throw new NotFoundException($"Id={id} olan rezervasyon bulunamadı.");

        if (!isAdmin && reservation.UserId != userId)
            throw new ForbiddenException("Bu rezervasyonu iptal etme yetkiniz yok.");

        await _reservationRepository.Cancel(id);
    }
}
