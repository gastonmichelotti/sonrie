using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace netCoreNew.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Insumo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnidadVenta = table.Column<double>(type: "float", nullable: false),
                    PrecioDolar = table.Column<double>(type: "float", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false),
                    Etiquetas = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insumo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ObraSocial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DemoraPago = table.Column<int>(type: "int", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObraSocial", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Redirect = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prestacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCategoriaPrestacion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prestacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prestacion_CategoriaPretacion_IdCategoriaPrestacion",
                        column: x => x.IdCategoriaPrestacion,
                        principalTable: "CategoriaPretacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InsumoxCategoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCategoriaPrestacion = table.Column<int>(type: "int", nullable: false),
                    IdInsumo = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsumoxCategoria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InsumoxCategoria_CategoriaPretacion_IdCategoriaPrestacion",
                        column: x => x.IdCategoriaPrestacion,
                        principalTable: "CategoriaPretacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InsumoxCategoria_Insumo_IdInsumo",
                        column: x => x.IdInsumo,
                        principalTable: "Insumo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Paciente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dni = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreApellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdObraSocial = table.Column<int>(type: "int", nullable: false),
                    OsPlan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumAfiliado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaAltaPaciente = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paciente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Paciente_ObraSocial_IdObraSocial",
                        column: x => x.IdObraSocial,
                        principalTable: "ObraSocial",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    IdRol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Rol_IdRol",
                        column: x => x.IdRol,
                        principalTable: "Rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Precio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPrestacion = table.Column<int>(type: "int", nullable: false),
                    IdObraSocial = table.Column<int>(type: "int", nullable: false),
                    PrecioPesos = table.Column<double>(type: "float", nullable: false),
                    CoseguroPesos = table.Column<double>(type: "float", nullable: false),
                    FechaUltimaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Precio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Precio_ObraSocial_IdObraSocial",
                        column: x => x.IdObraSocial,
                        principalTable: "ObraSocial",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Precio_Prestacion_IdPrestacion",
                        column: x => x.IdPrestacion,
                        principalTable: "Prestacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Atencion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdPaciente = table.Column<int>(type: "int", nullable: false),
                    IdFormadePago = table.Column<int>(type: "int", nullable: false),
                    IdEstadoAtencion = table.Column<int>(type: "int", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atencion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Atencion_Paciente_IdPaciente",
                        column: x => x.IdPaciente,
                        principalTable: "Paciente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Atencion_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PrestacionxAtencion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPrestacion = table.Column<int>(type: "int", nullable: false),
                    IdAtencion = table.Column<int>(type: "int", nullable: false),
                    IdPieza = table.Column<int>(type: "int", nullable: false),
                    Caras = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdObraSocial = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrestacionxAtencion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrestacionxAtencion_Atencion_IdAtencion",
                        column: x => x.IdAtencion,
                        principalTable: "Atencion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrestacionxAtencion_ObraSocial_IdObraSocial",
                        column: x => x.IdObraSocial,
                        principalTable: "ObraSocial",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrestacionxAtencion_Prestacion_IdPrestacion",
                        column: x => x.IdPrestacion,
                        principalTable: "Prestacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Atencion_IdPaciente",
                table: "Atencion",
                column: "IdPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_Atencion_IdUsuario",
                table: "Atencion",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_InsumoxCategoria_IdCategoriaPrestacion",
                table: "InsumoxCategoria",
                column: "IdCategoriaPrestacion");

            migrationBuilder.CreateIndex(
                name: "IX_InsumoxCategoria_IdInsumo",
                table: "InsumoxCategoria",
                column: "IdInsumo");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_IdObraSocial",
                table: "Paciente",
                column: "IdObraSocial");

            migrationBuilder.CreateIndex(
                name: "IX_Precio_IdObraSocial",
                table: "Precio",
                column: "IdObraSocial");

            migrationBuilder.CreateIndex(
                name: "IX_Precio_IdPrestacion",
                table: "Precio",
                column: "IdPrestacion");

            migrationBuilder.CreateIndex(
                name: "IX_Prestacion_IdCategoriaPrestacion",
                table: "Prestacion",
                column: "IdCategoriaPrestacion");

            migrationBuilder.CreateIndex(
                name: "IX_PrestacionxAtencion_IdAtencion",
                table: "PrestacionxAtencion",
                column: "IdAtencion");

            migrationBuilder.CreateIndex(
                name: "IX_PrestacionxAtencion_IdObraSocial",
                table: "PrestacionxAtencion",
                column: "IdObraSocial");

            migrationBuilder.CreateIndex(
                name: "IX_PrestacionxAtencion_IdPrestacion",
                table: "PrestacionxAtencion",
                column: "IdPrestacion");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdRol",
                table: "Usuario",
                column: "IdRol");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InsumoxCategoria");

            migrationBuilder.DropTable(
                name: "Precio");

            migrationBuilder.DropTable(
                name: "PrestacionxAtencion");

            migrationBuilder.DropTable(
                name: "Insumo");

            migrationBuilder.DropTable(
                name: "Atencion");

            migrationBuilder.DropTable(
                name: "Prestacion");

            migrationBuilder.DropTable(
                name: "Paciente");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "CategoriaPretacion");

            migrationBuilder.DropTable(
                name: "ObraSocial");

            migrationBuilder.DropTable(
                name: "Rol");
        }
    }
}
