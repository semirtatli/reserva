using Microsoft.AspNetCore.Mvc;
using Reservera.Models;

namespace Reservera.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        var rooms = new List<Room>
        {
            new() { Id = 1, Name = "Deniz Manzaralı Suite", Description = "Muhteşem deniz manzarası", PricePerNight = 250.00m, Capacity = 2 },
            new() { Id = 2, Name = "Bahçe Odası", Description = "Sakin bahçe görünümlü oda", PricePerNight = 120.00m, Capacity = 2 },
            new() { Id = 3, Name = "Aile Odası", Description = "Geniş aile odası", PricePerNight = 180.00m, Capacity = 4 }
        };

        return Ok(rooms);
    }
}
