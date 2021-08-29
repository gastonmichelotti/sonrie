using Microsoft.EntityFrameworkCore.Migrations;

namespace netCoreNew.Migrations
{
    public partial class chauProveedor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articulo_Proveedor_IdProveedor",
                table: "Articulo");

            migrationBuilder.DropIndex(
                name: "IX_Articulo_IdProveedor",
                table: "Articulo");

            migrationBuilder.DropColumn(
                name: "IdProveedor",
                table: "Articulo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdProveedor",
                table: "Articulo",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Articulo_IdProveedor",
                table: "Articulo",
                column: "IdProveedor");

            migrationBuilder.AddForeignKey(
                name: "FK_Articulo_Proveedor_IdProveedor",
                table: "Articulo",
                column: "IdProveedor",
                principalTable: "Proveedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
