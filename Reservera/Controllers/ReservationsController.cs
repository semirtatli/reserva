using Microsoft.AspNetCore.Mvc;
using Reservera.Models;

namespace Reservera.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(new List<Reservation>());
    }
}
