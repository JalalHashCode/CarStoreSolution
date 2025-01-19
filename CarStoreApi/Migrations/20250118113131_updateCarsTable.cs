using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarStoreApi.Migrations
{
    /// <inheritdoc />
    public partial class updateCarsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 18, 13, 31, 30, 826, DateTimeKind.Local).AddTicks(9251));

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 1, 18, 13, 31, 30, 826, DateTimeKind.Local).AddTicks(9290), new DateTime(2025, 1, 18, 13, 31, 30, 826, DateTimeKind.Local).AddTicks(9292) });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 18, 13, 31, 30, 826, DateTimeKind.Local).AddTicks(9294));

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 18, 13, 31, 30, 826, DateTimeKind.Local).AddTicks(9326));

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 18, 13, 31, 30, 826, DateTimeKind.Local).AddTicks(9329));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 18, 13, 29, 50, 560, DateTimeKind.Local).AddTicks(7837));

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 1, 18, 13, 29, 50, 560, DateTimeKind.Local).AddTicks(7878), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 18, 13, 29, 50, 560, DateTimeKind.Local).AddTicks(7881));

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 18, 13, 29, 50, 560, DateTimeKind.Local).AddTicks(7883));

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 18, 13, 29, 50, 560, DateTimeKind.Local).AddTicks(7886));
        }
    }
}
