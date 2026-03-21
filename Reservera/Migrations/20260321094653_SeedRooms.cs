using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservera.Migrations
{
    /// <inheritdoc />
    public partial class SeedRooms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Rooms",
                columns: ["Name", "Description", "PricePerNight", "Capacity"],
                values: new object[,]
                {
                    { "Deniz Manzaralı Suite", "Muhteşem deniz manzarası", 250.00m, 2 },
                    { "Bahçe Odası", "Sakin bahçe görünümlü oda", 120.00m, 2 },
                    { "Aile Odası", "Geniş aile odası", 180.00m, 4 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "Rooms", keyColumn: "Name", keyValues:
                ["Deniz Manzaralı Suite", "Bahçe Odası", "Aile Odası"]);
        }
    }
}
