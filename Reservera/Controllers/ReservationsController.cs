using Microsoft.AspNetCore.Mvc;
using Reservera.Models;
using Reservera.Services;

namespace Reservera.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _service;

    public ReservationsController(IReservationService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAll());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
        => Ok(await _service.GetById(id));

    [HttpPost]
    public async Task<IActionResult> Create(Reservation reservation)
    {
        var created = await _service.Create(reservation);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPatch("{id}/cancel")]
    public async Task<IActionResult> Cancel(int id)
    {
        await _service.Cancel(id);
        return NoContent();
    }
}
