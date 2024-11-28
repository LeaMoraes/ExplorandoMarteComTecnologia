using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplorandoMarteComTecnologia_WPF.DTO
{
    internal class QuestionarioAvaliacaoRequest
    {
        public List<QuestionarioRespostasDTO> QuestionarioRespostas { get; set; }
        public List<AvaliacaoRespostasDTO> AvaliacaoRespostas { get; set; }
        public AvaliacaoSugestao? AvaliacaoSugestao { get; set; }
    }
}
