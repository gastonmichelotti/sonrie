using Microsoft.EntityFrameworkCore.Migrations;

namespace netCoreNew.Migrations
{
    public partial class presupuestos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Presupuesto",
                table: "Compra",
                newName: "Presupuesto3");

            migrationBuilder.AddColumn<string>(
                name: "Presupuesto1",
                table: "Compra",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Presupuesto2",
                table: "Compra",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Presupuesto1",
                table: "Compra");

            migrationBuilder.DropColumn(
                name: "Presupuesto2",
                table: "Compra");

            migrationBuilder.RenameColumn(
                name: "Presupuesto3",
                table: "Compra",
                newName: "Presupuesto");
        }
    }
}
