using Microsoft.AspNetCore.Mvc;
using Reservera.Models;
using Reservera.Repositories;

namespace Reservera.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly RoomRepository _roomRepository = new();
    private readonly ReservationRepository _reservationRepository = new();

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_reservationRepository.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var reservation = _reservationRepository.GetById(id);
        if (reservation is null) return NotFound();
        return Ok(reservation);
    }

    [HttpPost]
    public IActionResult Create(Reservation reservation)
    {
        var room = _roomRepository.GetById(reservation.RoomId);
        if (room is null) return BadRequest("Oda bulunamadı.");

        var hasOverlap = _reservationRepository.HasOverlap(
            reservation.RoomId, reservation.CheckIn, reservation.CheckOut);

        if (hasOverlap) return Conflict("Bu oda seçilen tarihlerde dolu.");

        var nights = (reservation.CheckOut - reservation.CheckIn).Days;
        reservation.TotalPrice = room.PricePerNight * nights;

        var created = _reservationRepository.Add(reservation);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPatch("{id}/cancel")]
    public IActionResult Cancel(int id)
    {
        var cancelled = _reservationRepository.Cancel(id);
        if (!cancelled) return NotFound();
        return NoContent();
    }
}
