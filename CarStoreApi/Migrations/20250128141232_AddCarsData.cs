using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarStoreApi.Migrations
{
    /// <inheritdoc />
    public partial class AddCarsData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocalUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalUsers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Color", "CreatedDate", "ImageUrl", "Model", "Name", "Price", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "red", new DateTime(2025, 1, 28, 16, 12, 31, 661, DateTimeKind.Local).AddTicks(2267), "https://img.freepik.com/premium-photo/photo-supper-shine-bmw-series-stylish-design_1025753-53043.jpg?w=360", "BMW", "Foo", 3000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "black", new DateTime(2025, 1, 28, 16, 12, 31, 661, DateTimeKind.Local).AddTicks(2316), "https://img.freepik.com/premium-photo/photo-supper-shine-bmw-series-stylish-design_1025753-52982.jpg?w=996", "Mercedes", "Foo", 5500m, new DateTime(2025, 1, 28, 16, 12, 31, 661, DateTimeKind.Local).AddTicks(2318) },
                    { 3, "bluce", new DateTime(2025, 1, 28, 16, 12, 31, 661, DateTimeKind.Local).AddTicks(2320), "https://img.freepik.com/premium-photo/photo-supper-shine-bmw-series-stylish-design_1025753-52413.jpg?w=996", "Volvo", "Foo", 7000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Red", new DateTime(2025, 1, 28, 16, 12, 31, 661, DateTimeKind.Local).AddTicks(2323), "https://img.freepik.com/premium-photo/photo-supper-shine-bmw-series-stylish-design_1025753-53043.jpg?w=360", "Nissan", "Foo", 1000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Black", new DateTime(2025, 1, 28, 16, 12, 31, 661, DateTimeKind.Local).AddTicks(2325), "https://img.freepik.com/premium-photo/photo-supper-shine-bmw-series-stylish-design_1025753-52441.jpg?w=360", "Qere", "Foo", 6000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "LocalUsers");
        }
    }
}
