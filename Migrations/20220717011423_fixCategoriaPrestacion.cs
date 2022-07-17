using Microsoft.EntityFrameworkCore.Migrations;

namespace netCoreNew.Migrations
{
    public partial class fixCategoriaPrestacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsumoxCategoria_CategoriaPretacion_IdCategoriaPrestacion",
                table: "InsumoxCategoria");

            migrationBuilder.DropForeignKey(
                name: "FK_Prestacion_CategoriaPretacion_IdCategoriaPrestacion",
                table: "Prestacion");

            migrationBuilder.DropTable(
                name: "CategoriaPretacion");

            migrationBuilder.CreateTable(
                name: "CategoriaPrestacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaPrestacion", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_InsumoxCategoria_CategoriaPrestacion_IdCategoriaPrestacion",
                table: "InsumoxCategoria",
                column: "IdCategoriaPrestacion",
                principalTable: "CategoriaPrestacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Prestacion_CategoriaPrestacion_IdCategoriaPrestacion",
                table: "Prestacion",
                column: "IdCategoriaPrestacion",
                principalTable: "CategoriaPrestacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsumoxCategoria_CategoriaPrestacion_IdCategoriaPrestacion",
                table: "InsumoxCategoria");

            migrationBuilder.DropForeignKey(
                name: "FK_Prestacion_CategoriaPrestacion_IdCategoriaPrestacion",
                table: "Prestacion");

            migrationBuilder.DropTable(
                name: "CategoriaPrestacion");

            migrationBuilder.CreateTable(
                name: "CategoriaPretacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaPretacion", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_InsumoxCategoria_CategoriaPretacion_IdCategoriaPrestacion",
                table: "InsumoxCategoria",
                column: "IdCategoriaPrestacion",
                principalTable: "CategoriaPretacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Prestacion_CategoriaPretacion_IdCategoriaPrestacion",
                table: "Prestacion",
                column: "IdCategoriaPrestacion",
                principalTable: "CategoriaPretacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
