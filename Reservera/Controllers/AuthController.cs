using Microsoft.AspNetCore.Mvc;
using Reservera.DTOs;
using Reservera.Services;

namespace Reservera.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var user = await _authService.Register(request);
        return CreatedAtAction(nameof(Register), user);
    }
}
