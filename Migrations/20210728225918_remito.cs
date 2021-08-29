using Microsoft.EntityFrameworkCore.Migrations;

namespace netCoreNew.Migrations
{
    public partial class remito : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Remito_Proveedor_IdProveedor",
                table: "Remito");

            migrationBuilder.DropIndex(
                name: "IX_Remito_IdProveedor",
                table: "Remito");

            migrationBuilder.DropColumn(
                name: "IdProveedor",
                table: "Remito");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdProveedor",
                table: "Remito",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Remito_IdProveedor",
                table: "Remito",
                column: "IdProveedor");

            migrationBuilder.AddForeignKey(
                name: "FK_Remito_Proveedor_IdProveedor",
                table: "Remito",
                column: "IdProveedor",
                principalTable: "Proveedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
