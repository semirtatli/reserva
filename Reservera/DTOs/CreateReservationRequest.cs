namespace Reservera.DTOs;

public class CreateReservationRequest
{
    public int RoomId { get; set; }
    public string GuestName { get; set; } = string.Empty;
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
}
