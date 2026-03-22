using Reservera.DTOs;

namespace Reservera.Services;

public interface IReservationService
{
    Task<List<ReservationResponse>> GetAll(int? userId = null);
    Task<ReservationResponse> GetById(int id);
    Task<ReservationResponse> Create(CreateReservationRequest request, int userId);
    Task Cancel(int id, int userId, bool isAdmin);
}
