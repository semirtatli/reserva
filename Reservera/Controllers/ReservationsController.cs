using Microsoft.AspNetCore.Mvc;
using Reservera.Data;
using Reservera.Models;
using Reservera.Repositories;

namespace Reservera.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly RoomRepository _roomRepository;
    private readonly ReservationRepository _reservationRepository;

    public ReservationsController(ReserveraDbContext context)
    {
        _roomRepository = new RoomRepository(context);
        _reservationRepository = new ReservationRepository(context);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _reservationRepository.GetAll());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var reservation = await _reservationRepository.GetById(id);
        if (reservation is null) return NotFound();
        return Ok(reservation);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Reservation reservation)
    {
        var room = await _roomRepository.GetById(reservation.RoomId);
        if (room is null) return BadRequest("Oda bulunamadı.");

        var hasOverlap = await _reservationRepository.HasOverlap(
            reservation.RoomId, reservation.CheckIn, reservation.CheckOut);
        if (hasOverlap) return Conflict("Bu oda seçilen tarihlerde dolu.");

        var nights = (reservation.CheckOut - reservation.CheckIn).Days;
        reservation.TotalPrice = room.PricePerNight * nights;

        var created = await _reservationRepository.Add(reservation);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPatch("{id}/cancel")]
    public async Task<IActionResult> Cancel(int id)
    {
        var cancelled = await _reservationRepository.Cancel(id);
        if (!cancelled) return NotFound();
        return NoContent();
    }
}
