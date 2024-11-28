using System.ComponentModel.DataAnnotations;

namespace ExplorandoMarteComTecnologia_API.Models
{
    public class IngressoVendaModel
    {
        public int Id { get; set; }
        public int Id_Venda { get; set; }

        [MaxLength(9)]
        public string Key_Ingresso { get; set; }

    }
}
