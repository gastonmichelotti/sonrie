using Microsoft.EntityFrameworkCore.Migrations;

namespace netCoreNew.Migrations
{
    public partial class fixPrestacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FechaUltimaActualizacion",
                table: "Precio",
                newName: "Fecha");

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "Prestacion",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "Prestacion");

            migrationBuilder.RenameColumn(
                name: "Fecha",
                table: "Precio",
                newName: "FechaUltimaActualizacion");
        }
    }
}
