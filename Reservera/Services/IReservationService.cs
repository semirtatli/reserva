using Reservera.DTOs;

namespace Reservera.Services;

public interface IReservationService
{
    Task<List<ReservationResponse>> GetAll();
    Task<ReservationResponse> GetById(int id);
    Task<ReservationResponse> Create(CreateReservationRequest request);
    Task Cancel(int id);
}
