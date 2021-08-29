using Microsoft.EntityFrameworkCore.Migrations;

namespace netCoreNew.Migrations
{
    public partial class centroCostos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CentroCostos",
                table: "Compra");

            migrationBuilder.AddColumn<int>(
                name: "IdCentroCostos",
                table: "Compra",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CentroCostos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentroCostos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compra_IdCentroCostos",
                table: "Compra",
                column: "IdCentroCostos");

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_CentroCostos_IdCentroCostos",
                table: "Compra",
                column: "IdCentroCostos",
                principalTable: "CentroCostos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compra_CentroCostos_IdCentroCostos",
                table: "Compra");

            migrationBuilder.DropTable(
                name: "CentroCostos");

            migrationBuilder.DropIndex(
                name: "IX_Compra_IdCentroCostos",
                table: "Compra");

            migrationBuilder.DropColumn(
                name: "IdCentroCostos",
                table: "Compra");

            migrationBuilder.AddColumn<string>(
                name: "CentroCostos",
                table: "Compra",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
