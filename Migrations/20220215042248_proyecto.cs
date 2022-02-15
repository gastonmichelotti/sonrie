using Microsoft.EntityFrameworkCore.Migrations;

namespace netCoreNew.Migrations
{
    public partial class proyecto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdRecuento",
                table: "Proyecto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ItemsLoad",
                table: "Proyecto",
                type: "nvarchar(max)",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "ItemsLoad",
                table: "Proyecto");
        }
    }
}
