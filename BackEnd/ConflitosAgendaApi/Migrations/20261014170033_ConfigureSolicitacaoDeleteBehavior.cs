using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConflitosAgendaApi.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureSolicitacaoDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solicitacoes_Colaboradores_ColaboradorId",
                table: "Solicitacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Solicitacoes_Colaboradores_CoordenadorId",
                table: "Solicitacoes");

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacoes_Colaboradores_ColaboradorId",
                table: "Solicitacoes",
                column: "ColaboradorId",
                principalTable: "Colaboradores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacoes_Colaboradores_CoordenadorId",
                table: "Solicitacoes",
                column: "CoordenadorId",
                principalTable: "Colaboradores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solicitacoes_Colaboradores_ColaboradorId",
                table: "Solicitacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Solicitacoes_Colaboradores_CoordenadorId",
                table: "Solicitacoes");

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacoes_Colaboradores_ColaboradorId",
                table: "Solicitacoes",
                column: "ColaboradorId",
                principalTable: "Colaboradores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacoes_Colaboradores_CoordenadorId",
                table: "Solicitacoes",
                column: "CoordenadorId",
                principalTable: "Colaboradores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
