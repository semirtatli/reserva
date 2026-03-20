using Microsoft.AspNetCore.Mvc;
using Reservera.Models;
using Reservera.Repositories;

namespace Reservera.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomsController : ControllerBase
{
    private readonly RoomRepository _repository = new();

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_repository.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var room = _repository.GetById(id);
        if (room is null) return NotFound();
        return Ok(room);
    }

    [HttpPost]
    public IActionResult Create(Room room)
    {
        var created = _repository.Add(room);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var deleted = _repository.Delete(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
