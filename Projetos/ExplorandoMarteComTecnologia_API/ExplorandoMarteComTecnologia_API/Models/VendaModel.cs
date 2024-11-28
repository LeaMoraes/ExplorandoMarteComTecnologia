using System.ComponentModel.DataAnnotations;

namespace ExplorandoMarteComTecnologia_API.Models
{
    public class VendaModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int Quantidade { get; set; }

        [MaxLength(30)]
        public string MetodoPagamento { get; set; }
        public decimal Preco { get; set; }
    }
}
