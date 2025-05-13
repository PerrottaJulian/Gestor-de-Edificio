using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedBelgrano.Migrations
{
    /// <inheritdoc />
    public partial class newinit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    usuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dni = table.Column<int>(type: "int", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.usuarioId);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioAdmin",
                columns: table => new
                {
                    usuarioId = table.Column<int>(type: "int", nullable: false),
                    clave = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioAdmin", x => x.usuarioId);
                    table.ForeignKey(
                        name: "FK_UsuarioAdmin_Usuario_usuarioId",
                        column: x => x.usuarioId,
                        principalTable: "Usuario",
                        principalColumn: "usuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioEncargado",
                columns: table => new
                {
                    usuarioId = table.Column<int>(type: "int", nullable: false),
                    clave = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioEncargado", x => x.usuarioId);
                    table.ForeignKey(
                        name: "FK_UsuarioEncargado_Usuario_usuarioId",
                        column: x => x.usuarioId,
                        principalTable: "Usuario",
                        principalColumn: "usuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioResidente",
                columns: table => new
                {
                    usuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioResidente", x => x.usuarioId);
                    table.ForeignKey(
                        name: "FK_UsuarioResidente_Usuario_usuarioId",
                        column: x => x.usuarioId,
                        principalTable: "Usuario",
                        principalColumn: "usuarioId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioAdmin");

            migrationBuilder.DropTable(
                name: "UsuarioEncargado");

            migrationBuilder.DropTable(
                name: "UsuarioResidente");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
