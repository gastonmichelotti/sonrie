using Microsoft.EntityFrameworkCore.Migrations;

namespace netCoreNew.Migrations
{
    public partial class eliminar_proveedor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "Proveedor",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "Proveedor");
        }
    }
}
