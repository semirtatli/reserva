using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Reservera.Models;

namespace Reservera.Data;

public static class DbSeeder
{
    public static async Task SeedAdminAsync(ReserveraDbContext context)
    {
        var adminExists = await context.Users.AnyAsync(u => u.Role == UserRole.Admin);
        if (adminExists) return;

        using var hmac = new HMACSHA512();

        var admin = new User
        {
            Username = "admin",
            Email = "admin@reservera.com",
            PasswordSalt = hmac.Key,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Admin123!")),
            Role = UserRole.Admin
        };

        context.Users.Add(admin);
        await context.SaveChangesAsync();
    }
}
