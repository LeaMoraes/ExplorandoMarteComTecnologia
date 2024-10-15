namespace ExplorandoMarteComTecnologia_API.DTO
{
    public class RelatorioVendasDTO
    {
        public string RelatorioData { get; set; }  // String para o formato yyyy-MM-dd
        public decimal DinheiroTotal { get; set; }
        public int QuantidadePagamentoCredito { get; set; }
        public int QuantidadePagamentoDebito { get; set; }
        public int QuantidadePagamentoPix { get; set; }
    }
}
