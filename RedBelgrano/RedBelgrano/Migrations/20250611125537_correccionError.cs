using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedBelgrano.Migrations
{
    /// <inheritdoc />
    public partial class correccionError : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "cateogriaId",
                table: "CategoriaTransaccion",
                newName: "categoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "categoriaId",
                table: "CategoriaTransaccion",
                newName: "cateogriaId");
        }
    }
}
