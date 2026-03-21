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

    public async Task<List<ReservationResponse>> GetAll()
    {
        var reservations = await _reservationRepository.GetAll();
        return _mapper.Map<List<ReservationResponse>>(reservations);
    }

    public async Task<ReservationResponse> GetById(int id)
    {
        var reservation = await _reservationRepository.GetById(id);
        if (reservation is null) throw new NotFoundException($"Id={id} olan rezervasyon bulunamadı.");
        return _mapper.Map<ReservationResponse>(reservation);
    }

    public async Task<ReservationResponse> Create(CreateReservationRequest request)
    {
        var room = await _roomRepository.GetById(request.RoomId);
        if (room is null) throw new NotFoundException($"Id={request.RoomId} olan oda bulunamadı.");

        var hasOverlap = await _reservationRepository.HasOverlap(
            request.RoomId, request.CheckIn, request.CheckOut);
        if (hasOverlap) throw new RoomNotAvailableException("Bu oda seçilen tarihlerde dolu.");

        var nights = (request.CheckOut - request.CheckIn).Days;
        var reservation = _mapper.Map<Reservation>(request);
        reservation.TotalPrice = room.PricePerNight * nights;

        var created = await _reservationRepository.Add(reservation);

        var response = _mapper.Map<ReservationResponse>(created);
        response.RoomName = room.Name;
        return response;
    }

    public async Task Cancel(int id)
    {
        var cancelled = await _reservationRepository.Cancel(id);
        if (!cancelled) throw new NotFoundException($"Id={id} olan rezervasyon bulunamadı.");
    }
}
