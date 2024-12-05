using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoResiduosApi.Migrations
{
    /// <inheritdoc />
    public partial class AddRecipientes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_caminhao",
                columns: table => new
                {
                    id_caminhao = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ds_placa = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    ds_capacidade = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_caminhao", x => x.id_caminhao);
                });

            migrationBuilder.CreateTable(
                name: "t_recipiente",
                columns: table => new
                {
                    id_recipiente = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    max_capacidade = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    atual_nvl = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    status = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ultima_att = table.Column<DateTime>(type: "date", nullable: false),
                    latitude_recip = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    longitude_recip = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_recipiente", x => x.id_recipiente);
                });

            migrationBuilder.CreateTable(
                name: "t_emergencia",
                columns: table => new
                {
                    id_emergencia = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    dt_emergencia = table.Column<DateTime>(type: "date", nullable: false),
                    status = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ds_emergencia = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    T_RECIPIENTE_ID_RECIPIENTE = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    T_CAMINHAO_ID_CAMINHAO = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_emergencia", x => x.id_emergencia);
                    table.ForeignKey(
                        name: "FK_t_emergencia_t_caminhao_T_CAMINHAO_ID_CAMINHAO",
                        column: x => x.T_CAMINHAO_ID_CAMINHAO,
                        principalTable: "t_caminhao",
                        principalColumn: "id_caminhao",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_emergencia_t_recipiente_T_RECIPIENTE_ID_RECIPIENTE",
                        column: x => x.T_RECIPIENTE_ID_RECIPIENTE,
                        principalTable: "t_recipiente",
                        principalColumn: "id_recipiente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_rota",
                columns: table => new
                {
                    id_rota = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    dt_rota = table.Column<DateTime>(type: "date", nullable: false),
                    T_CAMINHAO_id_caminhao = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    T_RECIPIENTE_id_recipiente = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_rota", x => x.id_rota);
                    table.ForeignKey(
                        name: "FK_t_rota_t_caminhao_T_CAMINHAO_id_caminhao",
                        column: x => x.T_CAMINHAO_id_caminhao,
                        principalTable: "t_caminhao",
                        principalColumn: "id_caminhao",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_rota_t_recipiente_T_RECIPIENTE_id_recipiente",
                        column: x => x.T_RECIPIENTE_id_recipiente,
                        principalTable: "t_recipiente",
                        principalColumn: "id_recipiente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_agendamento",
                columns: table => new
                {
                    id_agendamento = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    dt_agendamento = table.Column<DateTime>(type: "date", nullable: false),
                    status_agendamento = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    T_ROTA_id_rota = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_agendamento", x => x.id_agendamento);
                    table.ForeignKey(
                        name: "FK_t_agendamento_t_rota_T_ROTA_id_rota",
                        column: x => x.T_ROTA_id_rota,
                        principalTable: "t_rota",
                        principalColumn: "id_rota",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_agendamento_T_ROTA_id_rota",
                table: "t_agendamento",
                column: "T_ROTA_id_rota");

            migrationBuilder.CreateIndex(
                name: "IX_t_emergencia_T_CAMINHAO_ID_CAMINHAO",
                table: "t_emergencia",
                column: "T_CAMINHAO_ID_CAMINHAO");

            migrationBuilder.CreateIndex(
                name: "IX_t_emergencia_T_RECIPIENTE_ID_RECIPIENTE",
                table: "t_emergencia",
                column: "T_RECIPIENTE_ID_RECIPIENTE");

            migrationBuilder.CreateIndex(
                name: "IX_t_rota_T_CAMINHAO_id_caminhao",
                table: "t_rota",
                column: "T_CAMINHAO_id_caminhao");

            migrationBuilder.CreateIndex(
                name: "IX_t_rota_T_RECIPIENTE_id_recipiente",
                table: "t_rota",
                column: "T_RECIPIENTE_id_recipiente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_agendamento");

            migrationBuilder.DropTable(
                name: "t_emergencia");

            migrationBuilder.DropTable(
                name: "t_rota");

            migrationBuilder.DropTable(
                name: "t_caminhao");

            migrationBuilder.DropTable(
                name: "t_recipiente");
        }
    }
}
