using Microsoft.EntityFrameworkCore.Migrations;

namespace netCoreNew.Migrations
{
    public partial class eliminar_articulo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "Articulo",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "Articulo");
        }
    }
}
