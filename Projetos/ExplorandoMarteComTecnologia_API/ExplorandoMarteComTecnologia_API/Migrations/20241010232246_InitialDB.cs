using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExplorandoMarteComTecnologia_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingresso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingresso", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IngressoPasse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key_Ingresso = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    ObrasLidas = table.Column<bool>(type: "bit", nullable: false),
                    QuestionarioFeito = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngressoPasse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IngressoVenda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Venda = table.Column<int>(type: "int", nullable: false),
                    Key_Ingresso = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngressoVenda", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Passe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PassesAtivos = table.Column<int>(type: "int", nullable: false),
                    PassesTotal = table.Column<int>(type: "int", nullable: false),
                    PassesObrasLidas = table.Column<int>(type: "int", nullable: false),
                    PassesQuestionarioFeito = table.Column<int>(type: "int", nullable: false),
                    PassesUsados = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RelatorioIngressos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RelatorioData = table.Column<DateOnly>(type: "date", nullable: false),
                    TotalIngressosVendidos = table.Column<int>(type: "int", nullable: false),
                    TotalIngressosInteiro = table.Column<int>(type: "int", nullable: false),
                    TotalIngressosMeia = table.Column<int>(type: "int", nullable: false),
                    TotalIngressosIsentos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatorioIngressos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RelatorioVendas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RelatorioData = table.Column<DateOnly>(type: "date", nullable: false),
                    DinheiroTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuantidadePagamentoCredito = table.Column<int>(type: "int", nullable: false),
                    QuantidadePagamentoDebito = table.Column<int>(type: "int", nullable: false),
                    QuantidadePagamentoPix = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatorioVendas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Venda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    MetodoPagamento = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venda", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingresso");

            migrationBuilder.DropTable(
                name: "IngressoPasse");

            migrationBuilder.DropTable(
                name: "IngressoVenda");

            migrationBuilder.DropTable(
                name: "Passe");

            migrationBuilder.DropTable(
                name: "RelatorioIngressos");

            migrationBuilder.DropTable(
                name: "RelatorioVendas");

            migrationBuilder.DropTable(
                name: "Venda");
        }
    }
}
