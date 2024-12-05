using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoResiduosApi.Migrations
{
    /// <inheritdoc />
    public partial class AddUsuarioTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_usuario",
                columns: table => new
                {
                    usuario_id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nome = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    senha = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    role = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_usuario", x => x.usuario_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_usuario");
        }
    }
}
