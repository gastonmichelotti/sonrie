using Microsoft.EntityFrameworkCore.Migrations;

namespace netCoreNew.Migrations
{
    public partial class montos_Atencion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrestacionxAtencion_ObraSocial_IdObraSocial",
                table: "PrestacionxAtencion");

            migrationBuilder.DropIndex(
                name: "IX_PrestacionxAtencion_IdObraSocial",
                table: "PrestacionxAtencion");

            migrationBuilder.DropColumn(
                name: "IdObraSocial",
                table: "PrestacionxAtencion");

            migrationBuilder.AddColumn<bool>(
                name: "Particular",
                table: "PrestacionxAtencion",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<float>(
                name: "MontoEfectivo",
                table: "Atencion",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "MontoOS",
                table: "Atencion",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Particular",
                table: "PrestacionxAtencion");

            migrationBuilder.DropColumn(
                name: "MontoEfectivo",
                table: "Atencion");

            migrationBuilder.DropColumn(
                name: "MontoOS",
                table: "Atencion");

            migrationBuilder.AddColumn<int>(
                name: "IdObraSocial",
                table: "PrestacionxAtencion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PrestacionxAtencion_IdObraSocial",
                table: "PrestacionxAtencion",
                column: "IdObraSocial");

            migrationBuilder.AddForeignKey(
                name: "FK_PrestacionxAtencion_ObraSocial_IdObraSocial",
                table: "PrestacionxAtencion",
                column: "IdObraSocial",
                principalTable: "ObraSocial",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
