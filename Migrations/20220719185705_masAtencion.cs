using Microsoft.EntityFrameworkCore.Migrations;

namespace netCoreNew.Migrations
{
    public partial class masAtencion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Observaciones",
                table: "PrestacionxAtencion",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<double>(
                name: "MontoOS",
                table: "Atencion",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "MontoEfectivo",
                table: "Atencion",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "Atencion",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Observaciones",
                table: "Atencion",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EstadoAtencion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoAtencion", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Atencion_IdEstadoAtencion",
                table: "Atencion",
                column: "IdEstadoAtencion");

            migrationBuilder.AddForeignKey(
                name: "FK_Atencion_EstadoAtencion_IdEstadoAtencion",
                table: "Atencion",
                column: "IdEstadoAtencion",
                principalTable: "EstadoAtencion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atencion_EstadoAtencion_IdEstadoAtencion",
                table: "Atencion");

            migrationBuilder.DropTable(
                name: "EstadoAtencion");

            migrationBuilder.DropIndex(
                name: "IX_Atencion_IdEstadoAtencion",
                table: "Atencion");

            migrationBuilder.DropColumn(
                name: "Observaciones",
                table: "PrestacionxAtencion");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "Atencion");

            migrationBuilder.DropColumn(
                name: "Observaciones",
                table: "Atencion");

            migrationBuilder.AlterColumn<float>(
                name: "MontoOS",
                table: "Atencion",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<float>(
                name: "MontoEfectivo",
                table: "Atencion",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
