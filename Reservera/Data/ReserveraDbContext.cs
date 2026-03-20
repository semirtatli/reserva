using Microsoft.EntityFrameworkCore;
using Reservera.Models;

namespace Reservera.Data;

public class ReserveraDbContext : DbContext
{
    public ReserveraDbContext(DbContextOptions<ReserveraDbContext> options) : base(options)
    {
    }

    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<Reservation> Reservations => Set<Reservation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.Name).IsRequired().HasMaxLength(100);
            entity.Property(r => r.Description).HasMaxLength(500);
            entity.Property(r => r.PricePerNight).HasPrecision(18, 2);
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.GuestName).IsRequired().HasMaxLength(100);
            entity.Property(r => r.TotalPrice).HasPrecision(18, 2);
            entity.Property(r => r.Status).HasConversion<string>();
        });
    }
}
