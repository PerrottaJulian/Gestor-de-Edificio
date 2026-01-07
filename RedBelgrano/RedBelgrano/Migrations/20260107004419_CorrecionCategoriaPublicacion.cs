using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedBelgrano.Migrations
{
    /// <inheritdoc />
    public partial class CorrecionCategoriaPublicacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publicaciones_CategoriasPublicacion_CategoriaPublicacionId",
                table: "Publicaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoriasPublicacion",
                table: "CategoriasPublicacion");

            migrationBuilder.RenameTable(
                name: "CategoriasPublicacion",
                newName: "CategoriaPublicacion");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoriaPublicacion",
                table: "CategoriaPublicacion",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Publicaciones_CategoriaPublicacion_CategoriaPublicacionId",
                table: "Publicaciones",
                column: "CategoriaPublicacionId",
                principalTable: "CategoriaPublicacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publicaciones_CategoriaPublicacion_CategoriaPublicacionId",
                table: "Publicaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoriaPublicacion",
                table: "CategoriaPublicacion");

            migrationBuilder.RenameTable(
                name: "CategoriaPublicacion",
                newName: "CategoriasPublicacion");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoriasPublicacion",
                table: "CategoriasPublicacion",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Publicaciones_CategoriasPublicacion_CategoriaPublicacionId",
                table: "Publicaciones",
                column: "CategoriaPublicacionId",
                principalTable: "CategoriasPublicacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
