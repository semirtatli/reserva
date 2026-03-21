using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservera.Migrations
{
    /// <inheritdoc />
    public partial class AddConstraintsAndIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckOut",
                table: "Reservations",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckIn",
                table: "Reservations",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_RoomId_CheckIn_CheckOut",
                table: "Reservations",
                columns: new[] { "RoomId", "CheckIn", "CheckOut" });

            migrationBuilder.AddCheckConstraint(
                name: "CK_Reservation_CheckOut_After_CheckIn",
                table: "Reservations",
                sql: "\"CheckOut\" > \"CheckIn\"");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reservations_RoomId_CheckIn_CheckOut",
                table: "Reservations");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Reservation_CheckOut_After_CheckIn",
                table: "Reservations");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckOut",
                table: "Reservations",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckIn",
                table: "Reservations",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }
    }
}
