using Microsoft.AspNetCore.Mvc;
using Reservera.Data;
using Reservera.Models;
using Reservera.Repositories;

namespace Reservera.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomsController : ControllerBase
{
    private readonly RoomRepository _repository;

    public RoomsController(ReserveraDbContext context)
    {
        _repository = new RoomRepository(context);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _repository.GetAll());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var room = await _repository.GetById(id);
        if (room is null) return NotFound();
        return Ok(room);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Room room)
    {
        var created = await _repository.Add(room);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _repository.Delete(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
