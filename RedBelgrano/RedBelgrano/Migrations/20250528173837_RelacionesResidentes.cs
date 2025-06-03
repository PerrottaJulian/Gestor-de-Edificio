using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedBelgrano.Migrations
{
    /// <inheritdoc />
    public partial class RelacionesResidentes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Residente_estadoId",
                table: "Residente",
                column: "estadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Residente_tipoRId",
                table: "Residente",
                column: "tipoRId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Residente_EstadoResidente_estadoId",
                table: "Residente");

            migrationBuilder.DropForeignKey(
                name: "FK_Residente_TipoResidente_tipoRId",
                table: "Residente");

            migrationBuilder.DropIndex(
                name: "IX_Residente_estadoId",
                table: "Residente");

            migrationBuilder.DropIndex(
                name: "IX_Residente_tipoRId",
                table: "Residente");
        }
    }
}
