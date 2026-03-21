namespace Reservera.Models;

public class Reservation
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public int UserId { get; set; }
    public string GuestName { get; set; } = string.Empty;
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public decimal TotalPrice { get; set; }
    public ReservationStatus Status { get; set; }
}
