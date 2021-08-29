using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace netCoreNew.Migrations
{
    public partial class historialremito : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistorialRemito",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Propiedad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Anterior = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nuevo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Responsable = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdRemito = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialRemito", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistorialRemito_Remito_IdRemito",
                        column: x => x.IdRemito,
                        principalTable: "Remito",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistorialRemito_IdRemito",
                table: "HistorialRemito",
                column: "IdRemito");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistorialRemito");
        }
    }
}
