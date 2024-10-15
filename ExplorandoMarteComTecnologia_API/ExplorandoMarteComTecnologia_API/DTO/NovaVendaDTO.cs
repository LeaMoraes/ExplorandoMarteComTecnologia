using ExplorandoMarteComTecnologia_API.Models;

namespace ExplorandoMarteComTecnologia_API.DTO
{
    public class NovaVendaDTO
    {
        public List<IngressoDTO> ingressoDTO { get; set; }
        public VendaModel venda { get; set; }
    }
}
