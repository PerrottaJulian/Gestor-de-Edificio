using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedBelgrano.Migrations
{
    /// <inheritdoc />
    public partial class addCategoriaTransaccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "categoriaId",
                table: "Transacciones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CategoriaTransaccion",
                columns: table => new
                {
                    cateogriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaTransaccion", x => x.cateogriaId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_categoriaId",
                table: "Transacciones",
                column: "categoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacciones_CategoriaTransaccion_categoriaId",
                table: "Transacciones",
                column: "categoriaId",
                principalTable: "CategoriaTransaccion",
                principalColumn: "cateogriaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacciones_CategoriaTransaccion_categoriaId",
                table: "Transacciones");

            migrationBuilder.DropTable(
                name: "CategoriaTransaccion");

            migrationBuilder.DropIndex(
                name: "IX_Transacciones_categoriaId",
                table: "Transacciones");

            migrationBuilder.DropColumn(
                name: "categoriaId",
                table: "Transacciones");
        }
    }
}
