using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservera.Models;

namespace Reservera.Data.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.GuestName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(r => r.TotalPrice)
            .HasPrecision(18, 2);

        builder.Property(r => r.Status)
            .HasConversion<string>();

        builder.HasCheckConstraint(
            "CK_Reservation_CheckOut_After_CheckIn",
            "\"CheckOut\" > \"CheckIn\"");

        builder.HasIndex(r => new { r.RoomId, r.CheckIn, r.CheckOut })
            .HasDatabaseName("IX_Reservations_RoomId_CheckIn_CheckOut");
    }
}
