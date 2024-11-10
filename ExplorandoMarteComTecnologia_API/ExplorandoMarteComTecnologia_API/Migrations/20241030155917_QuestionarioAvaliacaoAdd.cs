using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExplorandoMarteComTecnologia_API.Migrations
{
    /// <inheritdoc />
    public partial class QuestionarioAvaliacaoAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AvaliacaoRespostas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pergunta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Excelente = table.Column<int>(type: "int", nullable: true),
                    Bom = table.Column<int>(type: "int", nullable: true),
                    Regular = table.Column<int>(type: "int", nullable: true),
                    Ruim = table.Column<int>(type: "int", nullable: true),
                    Pessimo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvaliacaoRespostas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionarioRespostas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pergunta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Acertos = table.Column<int>(type: "int", nullable: false),
                    Erros = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionarioRespostas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvaliacaoRespostas");

            migrationBuilder.DropTable(
                name: "QuestionarioRespostas");
        }
    }
}
