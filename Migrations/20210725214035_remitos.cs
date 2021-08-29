using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace netCoreNew.Migrations
{
    public partial class remitos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Remito",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaRecepcion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Adjunto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCompra = table.Column<int>(type: "int", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdProveedor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Remito", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Remito_Compra_IdCompra",
                        column: x => x.IdCompra,
                        principalTable: "Compra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Remito_Proveedor_IdProveedor",
                        column: x => x.IdProveedor,
                        principalTable: "Proveedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Remito_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetalleRemito",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cantidad = table.Column<double>(type: "float", nullable: false),
                    IdDetalleCompra = table.Column<int>(type: "int", nullable: false),
                    IdRemito = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleRemito", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleRemito_DetalleCompra_IdDetalleCompra",
                        column: x => x.IdDetalleCompra,
                        principalTable: "DetalleCompra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetalleRemito_Remito_IdRemito",
                        column: x => x.IdRemito,
                        principalTable: "Remito",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalleRemito_IdDetalleCompra",
                table: "DetalleRemito",
                column: "IdDetalleCompra");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleRemito_IdRemito",
                table: "DetalleRemito",
                column: "IdRemito");

            migrationBuilder.CreateIndex(
                name: "IX_Remito_IdCompra",
                table: "Remito",
                column: "IdCompra");

            migrationBuilder.CreateIndex(
                name: "IX_Remito_IdProveedor",
                table: "Remito",
                column: "IdProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Remito_IdUsuario",
                table: "Remito",
                column: "IdUsuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleRemito");

            migrationBuilder.DropTable(
                name: "Remito");
        }
    }
}
