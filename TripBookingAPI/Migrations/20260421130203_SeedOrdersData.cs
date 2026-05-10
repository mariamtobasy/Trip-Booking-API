using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripBookingAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedOrdersData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "OrderDate", "TotalAmount", "TripId" },
                values: new object[] { 1, new DateTime(2026, 4, 21, 16, 2, 2, 27, DateTimeKind.Local).AddTicks(8434), 100m, 1 });

            migrationBuilder.InsertData(
                table: "Passengers",
                columns: new[] { "Id", "Email", "FirstName", "LastName" },
                values: new object[] { 1, "", "Mariam", "" });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                column: "DepartureDate",
                value: new DateTime(2026, 4, 28, 16, 2, 2, 27, DateTimeKind.Local).AddTicks(4633));

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 2,
                column: "DepartureDate",
                value: new DateTime(2026, 5, 1, 16, 2, 2, 27, DateTimeKind.Local).AddTicks(4719));

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "OrderId", "PassengerId", "Price", "SeatNumber" },
                values: new object[] { 1, 1, 1, 50m, "A1" });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_OrderId",
                table: "Tickets",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TripId",
                table: "Orders",
                column: "TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Trips_TripId",
                table: "Orders",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Orders_OrderId",
                table: "Tickets",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Trips_TripId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Orders_OrderId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_OrderId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Orders_TripId",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Passengers",
                keyColumn: "Id",
                keyValue: 1);

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
        }
    }
}
