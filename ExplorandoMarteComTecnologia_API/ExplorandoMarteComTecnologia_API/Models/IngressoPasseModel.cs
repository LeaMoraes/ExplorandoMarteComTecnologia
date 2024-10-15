using System.ComponentModel.DataAnnotations;

namespace ExplorandoMarteComTecnologia_API.Models
{
    public class IngressoPasseModel
    {

        public int Id { get; set; }

        [MaxLength(9)]
        public string Key_Ingresso { get; set; }
        public bool ObrasLidas { get; set; }
        public bool QuestionarioFeito { get; set; }

    }
}
