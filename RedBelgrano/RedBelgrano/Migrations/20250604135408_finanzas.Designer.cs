﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RedBelgrano.Context;

#nullable disable

namespace RedBelgrano.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20250604135408_finanzas")]
    partial class finanzas
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RedBelgrano.Models.EnumModels.EstadoResidente", b =>
                {
                    b.Property<int>("estadoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("estadoId"));

                    b.Property<string>("estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("estadoId");

                    b.ToTable("EstadoResidente", null, t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("RedBelgrano.Models.EnumModels.TipoResidente", b =>
                {
                    b.Property<int>("tipoRId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("tipoRId"));

                    b.Property<string>("tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("tipoRId");

                    b.ToTable("TipoResidente", null, t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("RedBelgrano.Models.EnumModels.TipoTransaccion", b =>
                {
                    b.Property<int>("tipoTId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("tipoTId"));

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("tipoTId");

                    b.ToTable("TipoTransaccion");
                });

            modelBuilder.Entity("RedBelgrano.Models.Residente", b =>
                {
                    b.Property<int>("residenteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("residenteId"));

                    b.Property<string>("apellido")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("departamento")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<int>("dni")
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("estadoId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("fechaBaja")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("fechaIngreso")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("piso")
                        .HasColumnType("int");

                    b.Property<int>("telefono")
                        .HasColumnType("int");

                    b.Property<int>("tipoRId")
                        .HasColumnType("int");

                    b.HasKey("residenteId");

                    b.HasIndex("estadoId");

                    b.HasIndex("tipoRId");

                    b.ToTable("Residente", (string)null);
                });

            modelBuilder.Entity("RedBelgrano.Models.Transaccion", b =>
                {
                    b.Property<int>("transaccionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("transaccionId"));

                    b.Property<int>("administradorId")
                        .HasColumnType("int");

                    b.Property<string>("detalle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("fecha")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<double>("monto")
                        .HasColumnType("float");

                    b.Property<int>("tipoId")
                        .HasColumnType("int");

                    b.HasKey("transaccionId");

                    b.HasIndex("administradorId");

                    b.HasIndex("tipoId");

                    b.ToTable("Transacciones");
                });

            modelBuilder.Entity("RedBelgrano.Models.Usuario", b =>
                {
                    b.Property<int>("usuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("usuarioId"));

                    b.Property<string>("clave")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("dni")
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("usuarioId");

                    b.ToTable("Usuario", (string)null);
                });

            modelBuilder.Entity("RedBelgrano.Models.Residente", b =>
                {
                    b.HasOne("RedBelgrano.Models.EnumModels.EstadoResidente", "estadoResidente")
                        .WithMany("Residentes")
                        .HasForeignKey("estadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RedBelgrano.Models.EnumModels.TipoResidente", "tipoResidente")
                        .WithMany("Residentes")
                        .HasForeignKey("tipoRId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("estadoResidente");

                    b.Navigation("tipoResidente");
                });

            modelBuilder.Entity("RedBelgrano.Models.Transaccion", b =>
                {
                    b.HasOne("RedBelgrano.Models.Usuario", "administrador")
                        .WithMany("Transacciones")
                        .HasForeignKey("administradorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RedBelgrano.Models.EnumModels.TipoTransaccion", "tipoTransaccion")
                        .WithMany("Transacciones")
                        .HasForeignKey("tipoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("administrador");

                    b.Navigation("tipoTransaccion");
                });

            modelBuilder.Entity("RedBelgrano.Models.EnumModels.EstadoResidente", b =>
                {
                    b.Navigation("Residentes");
                });

            modelBuilder.Entity("RedBelgrano.Models.EnumModels.TipoResidente", b =>
                {
                    b.Navigation("Residentes");
                });

            modelBuilder.Entity("RedBelgrano.Models.EnumModels.TipoTransaccion", b =>
                {
                    b.Navigation("Transacciones");
                });

            modelBuilder.Entity("RedBelgrano.Models.Usuario", b =>
                {
                    b.Navigation("Transacciones");
                });
#pragma warning restore 612, 618
        }
    }
}
