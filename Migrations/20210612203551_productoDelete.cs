using Microsoft.EntityFrameworkCore.Migrations;

namespace netCoreNew.Migrations
{
    public partial class productoDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleCompra_Producto_IdProducto",
                table: "DetalleCompra");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.RenameColumn(
                name: "IdProducto",
                table: "DetalleCompra",
                newName: "IdArticulo");

            migrationBuilder.RenameIndex(
                name: "IX_DetalleCompra_IdProducto",
                table: "DetalleCompra",
                newName: "IX_DetalleCompra_IdArticulo");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleCompra_Articulo_IdArticulo",
                table: "DetalleCompra",
                column: "IdArticulo",
                principalTable: "Articulo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleCompra_Articulo_IdArticulo",
                table: "DetalleCompra");

            migrationBuilder.RenameColumn(
                name: "IdArticulo",
                table: "DetalleCompra",
                newName: "IdProducto");

            migrationBuilder.RenameIndex(
                name: "IX_DetalleCompra_IdArticulo",
                table: "DetalleCompra",
                newName: "IX_DetalleCompra_IdProducto");

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Precio = table.Column<double>(type: "float", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleCompra_Producto_IdProducto",
                table: "DetalleCompra",
                column: "IdProducto",
                principalTable: "Producto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
