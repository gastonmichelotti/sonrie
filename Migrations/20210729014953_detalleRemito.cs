using Microsoft.EntityFrameworkCore.Migrations;

namespace netCoreNew.Migrations
{
    public partial class detalleRemito : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RemitoId",
                table: "DetalleCompra",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCompra_RemitoId",
                table: "DetalleCompra",
                column: "RemitoId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleCompra_Remito_RemitoId",
                table: "DetalleCompra",
                column: "RemitoId",
                principalTable: "Remito",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleCompra_Remito_RemitoId",
                table: "DetalleCompra");

            migrationBuilder.DropIndex(
                name: "IX_DetalleCompra_RemitoId",
                table: "DetalleCompra");

            migrationBuilder.DropColumn(
                name: "RemitoId",
                table: "DetalleCompra");
        }
    }
}
