using System.ComponentModel.DataAnnotations;

namespace ExplorandoMarteComTecnologia_API.Models
{
    public class IngressoModel
    {
        public int Id { get; set; }

        [MaxLength(9)]
        public string Key { get; set; }

        [MaxLength(100)]
        public string Nome { get; set; }

        [MaxLength(30)]
        public string Tipo { get; set; }

    }
}
