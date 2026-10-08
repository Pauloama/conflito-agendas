using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConflitosAgendaApi.Migrations
{
    /// <inheritdoc />
    public partial class MigrationMain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalDias",
                table: "Solicitacoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdColaborador",
                table: "Periodos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalDias",
                table: "Solicitacoes");

            migrationBuilder.DropColumn(
                name: "IdColaborador",
                table: "Periodos");
        }
    }
}
