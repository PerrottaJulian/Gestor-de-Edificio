using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedBelgrano.Migrations
{
    /// <inheritdoc />
    public partial class finanzas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoTransaccion",
                columns: table => new
                {
                    tipoTId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoTransaccion", x => x.tipoTId);
                });

            migrationBuilder.CreateTable(
                name: "Transacciones",
                columns: table => new
                {
                    transaccionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    monto = table.Column<double>(type: "float", nullable: false),
                    detalle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    administradorId = table.Column<int>(type: "int", nullable: false),
                    tipoId = table.Column<int>(type: "int", nullable: false),
                    fecha = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacciones", x => x.transaccionId);
                    table.ForeignKey(
                        name: "FK_Transacciones_TipoTransaccion_tipoId",
                        column: x => x.tipoId,
                        principalTable: "TipoTransaccion",
                        principalColumn: "tipoTId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transacciones_Usuario_administradorId",
                        column: x => x.administradorId,
                        principalTable: "Usuario",
                        principalColumn: "usuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_administradorId",
                table: "Transacciones",
                column: "administradorId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_tipoId",
                table: "Transacciones",
                column: "tipoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transacciones");

            migrationBuilder.DropTable(
                name: "TipoTransaccion");
        }
    }
}
