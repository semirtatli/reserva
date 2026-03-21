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
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReserveraDbContext).Assembly);
    }
}
