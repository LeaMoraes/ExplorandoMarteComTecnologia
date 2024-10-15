namespace ExplorandoMarteComTecnologia_API.Models
{
    public class RelatorioVendasModel
    {
        public int Id { get; set; }
        public DateOnly RelatorioData { get; set; }
        public decimal DinheiroTotal { get; set; }
        public int QuantidadePagamentoCredito { get; set; }
        public int QuantidadePagamentoDebito { get; set; }
        public int QuantidadePagamentoPix { get; set; }

    }
}
