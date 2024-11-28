using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplorandoMarteComTecnologia_WPF.DTO
{
    internal class AvaliacaoRespostasDTO
    {
        public int Id { get; set; }
        public string Pergunta { get; set; }
        public int Excelente { get; set; }
        public int Bom { get; set; }
        public int Regular { get; set; }
        public int Ruim { get; set; }
        public int Pessimo { get; set; }
    }
}
