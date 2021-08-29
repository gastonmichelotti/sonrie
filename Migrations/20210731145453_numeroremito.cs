using Microsoft.EntityFrameworkCore.Migrations;

namespace netCoreNew.Migrations
{
    public partial class numeroremito : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Remito");

            migrationBuilder.AddColumn<string>(
                name: "NumeroRemito",
                table: "Remito",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PuntoVenta",
                table: "Remito",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroRemito",
                table: "Remito");

            migrationBuilder.DropColumn(
                name: "PuntoVenta",
                table: "Remito");

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "Remito",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
