using Microsoft.EntityFrameworkCore.Migrations;

namespace netCoreNew.Migrations
{
    public partial class CodigoProveedor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CodigoProveedor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdArticulo = table.Column<int>(type: "int", nullable: false),
                    IdProveedor = table.Column<int>(type: "int", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrecioProveedor = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodigoProveedor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CodigoProveedor_Articulo_IdArticulo",
                        column: x => x.IdArticulo,
                        principalTable: "Articulo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CodigoProveedor_Proveedor_IdProveedor",
                        column: x => x.IdProveedor,
                        principalTable: "Proveedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CodigoProveedor_IdArticulo",
                table: "CodigoProveedor",
                column: "IdArticulo");

            migrationBuilder.CreateIndex(
                name: "IX_CodigoProveedor_IdProveedor",
                table: "CodigoProveedor",
                column: "IdProveedor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CodigoProveedor");
        }
    }
}
