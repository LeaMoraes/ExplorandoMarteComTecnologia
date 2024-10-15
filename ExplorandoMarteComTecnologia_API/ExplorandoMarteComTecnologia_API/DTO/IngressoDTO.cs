using System.ComponentModel.DataAnnotations;

namespace ExplorandoMarteComTecnologia_API.DTO
{
    public class IngressoDTO
    {
        public string? Key { get; set; }
        [MaxLength(100)]
        public string Nome { get; set; }
        public string Tipo { get; set; }
    }
}
