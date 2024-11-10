using ExplorandoMarteComTecnologia_API.Models;
using Microsoft.EntityFrameworkCore;

namespace ExplorandoMarteComTecnologia_API.Data
{
    public class ExplorarMarteDBContext : DbContext
    {

        public ExplorarMarteDBContext(DbContextOptions<ExplorarMarteDBContext> options) : base(options) { }

        public DbSet<IngressoModel> Ingresso { get; set; }
        public DbSet<PasseModel> Passe { get; set; }
        public DbSet<VendaModel> Venda { get; set; }

        public DbSet<IngressoPasseModel> IngressoPasse { get; set; }
        public DbSet<IngressoVendaModel> IngressoVenda { get; set; }

        public DbSet<RelatorioIngressosModel> RelatorioIngressos { get; set; }
        public DbSet<RelatorioVendasModel> RelatorioVendas { get; set; }

        public DbSet<QuestionarioRespostasModel> QuestionarioRespostas { get; set; }
        public DbSet<AvaliacaoRespostasModel> AvaliacaoRespostas { get; set; }
        public DbSet<AvaliacaoSugestaoModel> AvaliacaoSugestao { get; set; }

    }
}
