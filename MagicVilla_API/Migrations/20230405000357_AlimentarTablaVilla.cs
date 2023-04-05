using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_API.Migrations
{
    public partial class AlimentarTablaVilla : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenidad", "Detalle", "FechaActualizacion", "FechaCreacion", "ImagenUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[] { 1, "", "Detalle de la Villa...", new DateTime(2023, 4, 4, 18, 3, 56, 981, DateTimeKind.Local).AddTicks(1621), new DateTime(2023, 4, 4, 18, 3, 56, 981, DateTimeKind.Local).AddTicks(1609), "", 50, "Villa Real", 5, 200.0 });

            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenidad", "Detalle", "FechaActualizacion", "FechaCreacion", "ImagenUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[] { 2, "", "Detalle de la Villa...", new DateTime(2023, 4, 4, 18, 3, 56, 981, DateTimeKind.Local).AddTicks(1625), new DateTime(2023, 4, 4, 18, 3, 56, 981, DateTimeKind.Local).AddTicks(1624), "", 40, "Premium Vista a la Piscina", 4, 150.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
