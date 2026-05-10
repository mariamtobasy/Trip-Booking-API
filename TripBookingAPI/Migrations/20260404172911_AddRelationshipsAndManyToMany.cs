using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripBookingAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationshipsAndManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PassengerId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PassengerTrip",
                columns: table => new
                {
                    PassengersId = table.Column<int>(type: "int", nullable: false),
                    TripsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassengerTrip", x => new { x.PassengersId, x.TripsId });
                    table.ForeignKey(
                        name: "FK_PassengerTrip_Passengers_PassengersId",
                        column: x => x.PassengersId,
                        principalTable: "Passengers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PassengerTrip_Trips_TripsId",
                        column: x => x.TripsId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                column: "DepartureDate",
                value: new DateTime(2026, 4, 11, 20, 29, 10, 574, DateTimeKind.Local).AddTicks(606));

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 2,
                column: "DepartureDate",
                value: new DateTime(2026, 4, 14, 20, 29, 10, 574, DateTimeKind.Local).AddTicks(628));

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_PassengerId",
                table: "Tickets",
                column: "PassengerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_TripId",
                table: "Reviews",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_PassengerTrip_TripsId",
                table: "PassengerTrip",
                column: "TripsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Trips_TripId",
                table: "Reviews",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Passengers_PassengerId",
                table: "Tickets",
                column: "PassengerId",
                principalTable: "Passengers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Trips_TripId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Passengers_PassengerId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "PassengerTrip");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_PassengerId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_TripId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "PassengerId",
                table: "Tickets");

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                column: "DepartureDate",
                value: new DateTime(2026, 4, 2, 19, 31, 7, 359, DateTimeKind.Local).AddTicks(496));

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 2,
                column: "DepartureDate",
                value: new DateTime(2026, 4, 5, 19, 31, 7, 359, DateTimeKind.Local).AddTicks(535));
        }
    }
}
