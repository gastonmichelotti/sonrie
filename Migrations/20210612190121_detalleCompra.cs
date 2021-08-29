using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace netCoreNew.Migrations
{
    public partial class detalleCompra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compra_Producto_IdProducto",
                table: "Compra");

            migrationBuilder.DropColumn(
                name: "Monto",
                table: "Compra");

            migrationBuilder.DropColumn(
                name: "Referencia",
                table: "Compra");

            migrationBuilder.RenameColumn(
                name: "IdProducto",
                table: "Compra",
                newName: "IdEstadoCompra");

            migrationBuilder.RenameIndex(
                name: "IX_Compra_IdProducto",
                table: "Compra",
                newName: "IX_Compra_IdEstadoCompra");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEntrega",
                table: "Compra",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdProveedor",
                table: "Compra",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DetalleCompra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    IdCompra = table.Column<int>(type: "int", nullable: false),
                    IdProducto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleCompra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleCompra_Compra_IdCompra",
                        column: x => x.IdCompra,
                        principalTable: "Compra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetalleCompra_Producto_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "Producto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EstadoCompra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoCompra", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compra_IdProveedor",
                table: "Compra",
                column: "IdProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCompra_IdCompra",
                table: "DetalleCompra",
                column: "IdCompra");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCompra_IdProducto",
                table: "DetalleCompra",
                column: "IdProducto");

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_EstadoCompra_IdEstadoCompra",
                table: "Compra",
                column: "IdEstadoCompra",
                principalTable: "EstadoCompra",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_Proveedor_IdProveedor",
                table: "Compra",
                column: "IdProveedor",
                principalTable: "Proveedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compra_EstadoCompra_IdEstadoCompra",
                table: "Compra");

            migrationBuilder.DropForeignKey(
                name: "FK_Compra_Proveedor_IdProveedor",
                table: "Compra");

            migrationBuilder.DropTable(
                name: "DetalleCompra");

            migrationBuilder.DropTable(
                name: "EstadoCompra");

            migrationBuilder.DropIndex(
                name: "IX_Compra_IdProveedor",
                table: "Compra");

            migrationBuilder.DropColumn(
                name: "FechaEntrega",
                table: "Compra");

            migrationBuilder.DropColumn(
                name: "IdProveedor",
                table: "Compra");

            migrationBuilder.RenameColumn(
                name: "IdEstadoCompra",
                table: "Compra",
                newName: "IdProducto");

            migrationBuilder.RenameIndex(
                name: "IX_Compra_IdEstadoCompra",
                table: "Compra",
                newName: "IX_Compra_IdProducto");

            migrationBuilder.AddColumn<double>(
                name: "Monto",
                table: "Compra",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Referencia",
                table: "Compra",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_Producto_IdProducto",
                table: "Compra",
                column: "IdProducto",
                principalTable: "Producto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
