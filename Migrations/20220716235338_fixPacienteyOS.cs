using Microsoft.EntityFrameworkCore.Migrations;

namespace netCoreNew.Migrations
{
    public partial class fixPacienteyOS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NombreApellido",
                table: "Paciente",
                newName: "Nombre");

            migrationBuilder.AddColumn<string>(
                name: "Apellido",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "Paciente",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "ObraSocial",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apellido",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "ObraSocial");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Paciente",
                newName: "NombreApellido");
        }
    }
}
