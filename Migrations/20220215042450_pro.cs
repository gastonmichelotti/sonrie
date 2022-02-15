using Microsoft.EntityFrameworkCore.Migrations;

namespace netCoreNew.Migrations
{
    public partial class pro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proyecto_Recuento_IdRecuento",
                table: "Proyecto");

            migrationBuilder.DropIndex(
                name: "IX_Proyecto_IdRecuento",
                table: "Proyecto");

            migrationBuilder.DropColumn(
                name: "IdRecuento",
                table: "Proyecto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdRecuento",
                table: "Proyecto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Proyecto_IdRecuento",
                table: "Proyecto",
                column: "IdRecuento");

            migrationBuilder.AddForeignKey(
                name: "FK_Proyecto_Recuento_IdRecuento",
                table: "Proyecto",
                column: "IdRecuento",
                principalTable: "Recuento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
