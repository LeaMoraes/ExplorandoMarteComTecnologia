namespace ExplorandoMarteComTecnologia_API.Models
{
    public class RelatorioIngressosModel
    {

        public int Id { get; set; }
        public DateOnly RelatorioData { get; set; }
        public int TotalIngressosVendidos { get; set; }
        public int TotalIngressosInteiro { get; set; }
        public int TotalIngressosMeia { get; set; }
        public int TotalIngressosIsentos { get; set; }

    }
}
