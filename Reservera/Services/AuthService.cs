using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Reservera.Data;
using Reservera.DTOs;
using Reservera.Exceptions;
using Reservera.Models;

namespace Reservera.Services;

public class AuthService
{
    private readonly ReserveraDbContext _context;

    public AuthService(ReserveraDbContext context)
    {
        _context = context;
    }

    public async Task<UserResponse> Register(RegisterRequest request)
    {
        var emailExists = await _context.Users.AnyAsync(u => u.Email == request.Email);
        if (emailExists) throw new BadRequestException("Bu email adresi zaten kayıtlı.");

        using var hmac = new HMACSHA512();

        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordSalt = hmac.Key,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)),
            Role = UserRole.User
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new UserResponse
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role.ToString()
        };
    }
}
