﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using netCoreNew.Data;

namespace netCoreNew.Migrations
{
    [DbContext(typeof(NetCoreNewContext))]
    [Migration("20220716011741_init2")]
    partial class init2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("netCoreNew.Models.Atencion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdEstadoAtencion")
                        .HasColumnType("int");

                    b.Property<int>("IdFormadePago")
                        .HasColumnType("int");

                    b.Property<int>("IdPaciente")
                        .HasColumnType("int");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdPaciente");

                    b.HasIndex("IdUsuario");

                    b.ToTable("Atencion");
                });

            modelBuilder.Entity("netCoreNew.Models.CategoriaPestacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Observaciones")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CategoriaPretacion");
                });

            modelBuilder.Entity("netCoreNew.Models.Insumo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Eliminado")
                        .HasColumnType("bit");

                    b.Property<string>("Etiquetas")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Observaciones")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PrecioDolar")
                        .HasColumnType("float");

                    b.Property<double>("UnidadVenta")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Insumo");
                });

            modelBuilder.Entity("netCoreNew.Models.InsumoxCategoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Cantidad")
                        .HasColumnType("real");

                    b.Property<int>("IdCategoriaPrestacion")
                        .HasColumnType("int");

                    b.Property<int>("IdInsumo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdCategoriaPrestacion");

                    b.HasIndex("IdInsumo");

                    b.ToTable("InsumoxCategoria");
                });

            modelBuilder.Entity("netCoreNew.Models.ObraSocial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DemoraPago")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Observaciones")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ObraSocial");
                });

            modelBuilder.Entity("netCoreNew.Models.Paciente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Dni")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaAltaPaciente")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdObraSocial")
                        .HasColumnType("int");

                    b.Property<string>("Mail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreApellido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumAfiliado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Observaciones")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OsPlan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdObraSocial");

                    b.ToTable("Paciente");
                });

            modelBuilder.Entity("netCoreNew.Models.Precio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("CoseguroPesos")
                        .HasColumnType("float");

                    b.Property<DateTime>("FechaUltimaActualizacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdObraSocial")
                        .HasColumnType("int");

                    b.Property<int>("IdPrestacion")
                        .HasColumnType("int");

                    b.Property<string>("Observaciones")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PrecioPesos")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("IdObraSocial");

                    b.HasIndex("IdPrestacion");

                    b.ToTable("Precio");
                });

            modelBuilder.Entity("netCoreNew.Models.Prestacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdCategoriaPrestacion")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Observaciones")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdCategoriaPrestacion");

                    b.ToTable("Prestacion");
                });

            modelBuilder.Entity("netCoreNew.Models.PrestacionxAtencion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Caras")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdAtencion")
                        .HasColumnType("int");

                    b.Property<int>("IdObraSocial")
                        .HasColumnType("int");

                    b.Property<int>("IdPieza")
                        .HasColumnType("int");

                    b.Property<int>("IdPrestacion")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdAtencion");

                    b.HasIndex("IdObraSocial");

                    b.HasIndex("IdPrestacion");

                    b.ToTable("PrestacionxAtencion");
                });

            modelBuilder.Entity("netCoreNew.Models.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Redirect")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Rol");
                });

            modelBuilder.Entity("netCoreNew.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaAlta")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdRol")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdRol");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("netCoreNew.Models.Atencion", b =>
                {
                    b.HasOne("netCoreNew.Models.Paciente", "Paciente")
                        .WithMany()
                        .HasForeignKey("IdPaciente")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("netCoreNew.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Paciente");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("netCoreNew.Models.InsumoxCategoria", b =>
                {
                    b.HasOne("netCoreNew.Models.CategoriaPestacion", "CategoriaPestacion")
                        .WithMany()
                        .HasForeignKey("IdCategoriaPrestacion")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("netCoreNew.Models.Insumo", "Insumo")
                        .WithMany()
                        .HasForeignKey("IdInsumo")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CategoriaPestacion");

                    b.Navigation("Insumo");
                });

            modelBuilder.Entity("netCoreNew.Models.Paciente", b =>
                {
                    b.HasOne("netCoreNew.Models.ObraSocial", "ObraSocial")
                        .WithMany()
                        .HasForeignKey("IdObraSocial")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ObraSocial");
                });

            modelBuilder.Entity("netCoreNew.Models.Precio", b =>
                {
                    b.HasOne("netCoreNew.Models.ObraSocial", "ObraSocial")
                        .WithMany()
                        .HasForeignKey("IdObraSocial")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("netCoreNew.Models.Prestacion", "Prestacion")
                        .WithMany()
                        .HasForeignKey("IdPrestacion")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ObraSocial");

                    b.Navigation("Prestacion");
                });

            modelBuilder.Entity("netCoreNew.Models.Prestacion", b =>
                {
                    b.HasOne("netCoreNew.Models.CategoriaPestacion", "CategoriaPestacion")
                        .WithMany("Prestaciones")
                        .HasForeignKey("IdCategoriaPrestacion")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CategoriaPestacion");
                });

            modelBuilder.Entity("netCoreNew.Models.PrestacionxAtencion", b =>
                {
                    b.HasOne("netCoreNew.Models.Atencion", "Atencion")
                        .WithMany("Detalles")
                        .HasForeignKey("IdAtencion")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("netCoreNew.Models.ObraSocial", "ObraSocial")
                        .WithMany()
                        .HasForeignKey("IdObraSocial")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("netCoreNew.Models.Prestacion", "Prestacion")
                        .WithMany()
                        .HasForeignKey("IdPrestacion")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Atencion");

                    b.Navigation("ObraSocial");

                    b.Navigation("Prestacion");
                });

            modelBuilder.Entity("netCoreNew.Models.Usuario", b =>
                {
                    b.HasOne("netCoreNew.Models.Rol", "Rol")
                        .WithMany("Usuarios")
                        .HasForeignKey("IdRol")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("netCoreNew.Models.Atencion", b =>
                {
                    b.Navigation("Detalles");
                });

            modelBuilder.Entity("netCoreNew.Models.CategoriaPestacion", b =>
                {
                    b.Navigation("Prestaciones");
                });

            modelBuilder.Entity("netCoreNew.Models.Rol", b =>
                {
                    b.Navigation("Usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}
