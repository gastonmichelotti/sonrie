using Microsoft.EntityFrameworkCore.Migrations;

namespace netCoreNew.Migrations
{
    public partial class detallerecuento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnidadMedida",
                table: "Recuento");

            migrationBuilder.AlterColumn<int>(
                name: "IdProyecto",
                table: "Recuento",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "Recuento",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "DetalleRecuento",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Precio",
                table: "DetalleRecuento",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "UnidadMedida",
                table: "DetalleRecuento",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "Recuento");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "DetalleRecuento");

            migrationBuilder.DropColumn(
                name: "Precio",
                table: "DetalleRecuento");

            migrationBuilder.DropColumn(
                name: "UnidadMedida",
                table: "DetalleRecuento");

            migrationBuilder.AlterColumn<int>(
                name: "IdProyecto",
                table: "Recuento",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnidadMedida",
                table: "Recuento",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
