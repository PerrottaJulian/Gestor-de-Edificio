using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedBelgrano.Migrations
{
    /// <inheritdoc />
    public partial class tickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publicaciones_Usuario_UsuarioId",
                table: "Publicaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Residente_EstadoResidente_estadoId",
                table: "Residente");

            migrationBuilder.DropForeignKey(
                name: "FK_Residente_TipoResidente_tipoRId",
                table: "Residente");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacciones_Usuario_administradorId",
                table: "Transacciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Residente",
                table: "Residente");

            migrationBuilder.RenameTable(
                name: "Usuario",
                newName: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Residente",
                newName: "Residentes");

            migrationBuilder.RenameIndex(
                name: "IX_Residente_tipoRId",
                table: "Residentes",
                newName: "IX_Residentes_tipoRId");

            migrationBuilder.RenameIndex(
                name: "IX_Residente_estadoId",
                table: "Residentes",
                newName: "IX_Residentes_estadoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "usuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Residentes",
                table: "Residentes",
                column: "residenteId");

            migrationBuilder.CreateTable(
                name: "CategoriaTicket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaTicket", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstadoTicket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoTicket", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmisorId = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Contenido = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    EstadoTicketId = table.Column<int>(type: "int", nullable: false),
                    CategoriaTicketId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_CategoriaTicket_CategoriaTicketId",
                        column: x => x.CategoriaTicketId,
                        principalTable: "CategoriaTicket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_EstadoTicket_EstadoTicketId",
                        column: x => x.EstadoTicketId,
                        principalTable: "EstadoTicket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_Usuarios_EmisorId",
                        column: x => x.EmisorId,
                        principalTable: "Usuarios",
                        principalColumn: "usuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CategoriaTicketId",
                table: "Tickets",
                column: "CategoriaTicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_EmisorId",
                table: "Tickets",
                column: "EmisorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_EstadoTicketId",
                table: "Tickets",
                column: "EstadoTicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Publicaciones_Usuarios_UsuarioId",
                table: "Publicaciones",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "usuarioId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Residentes_EstadoResidente_estadoId",
                table: "Residentes",
                column: "estadoId",
                principalTable: "EstadoResidente",
                principalColumn: "estadoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Residentes_TipoResidente_tipoRId",
                table: "Residentes",
                column: "tipoRId",
                principalTable: "TipoResidente",
                principalColumn: "tipoRId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transacciones_Usuarios_administradorId",
                table: "Transacciones",
                column: "administradorId",
                principalTable: "Usuarios",
                principalColumn: "usuarioId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publicaciones_Usuarios_UsuarioId",
                table: "Publicaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Residentes_EstadoResidente_estadoId",
                table: "Residentes");

            migrationBuilder.DropForeignKey(
                name: "FK_Residentes_TipoResidente_tipoRId",
                table: "Residentes");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacciones_Usuarios_administradorId",
                table: "Transacciones");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "CategoriaTicket");

            migrationBuilder.DropTable(
                name: "EstadoTicket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Residentes",
                table: "Residentes");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Usuario");

            migrationBuilder.RenameTable(
                name: "Residentes",
                newName: "Residente");

            migrationBuilder.RenameIndex(
                name: "IX_Residentes_tipoRId",
                table: "Residente",
                newName: "IX_Residente_tipoRId");

            migrationBuilder.RenameIndex(
                name: "IX_Residentes_estadoId",
                table: "Residente",
                newName: "IX_Residente_estadoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario",
                column: "usuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Residente",
                table: "Residente",
                column: "residenteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Publicaciones_Usuario_UsuarioId",
                table: "Publicaciones",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "usuarioId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Residente_EstadoResidente_estadoId",
                table: "Residente",
                column: "estadoId",
                principalTable: "EstadoResidente",
                principalColumn: "estadoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Residente_TipoResidente_tipoRId",
                table: "Residente",
                column: "tipoRId",
                principalTable: "TipoResidente",
                principalColumn: "tipoRId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transacciones_Usuario_administradorId",
                table: "Transacciones",
                column: "administradorId",
                principalTable: "Usuario",
                principalColumn: "usuarioId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
