namespace ExplorandoMarteComTecnologia_API.DTO
{
    public class RelatorioIngressosDTO
    {
        public string RelatorioData { get; set; }
        public int TotalIngressosVendidos { get; set; }
        public int TotalIngressosInteiro { get; set; }
        public int TotalIngressosMeia { get; set; }
        public int TotalIngressosIsentos { get; set; }
    }
}
