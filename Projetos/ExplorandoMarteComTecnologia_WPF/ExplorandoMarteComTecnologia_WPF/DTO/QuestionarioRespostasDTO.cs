using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplorandoMarteComTecnologia_WPF.DTO
{
    internal class QuestionarioRespostasDTO
    {
        public int Id { get; set; }
        public string Pergunta { get; set; }
        public int Acertos { get; set; }
        public int Erros { get; set; }
    }
}
