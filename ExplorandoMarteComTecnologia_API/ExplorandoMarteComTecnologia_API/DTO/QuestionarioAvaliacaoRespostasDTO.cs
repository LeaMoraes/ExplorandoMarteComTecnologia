using ExplorandoMarteComTecnologia_API.Models;

namespace ExplorandoMarteComTecnologia_API.DTO
{
    public class QuestionarioAvaliacaoRespostasDTO
    {
        public List<QuestionarioRespostasModel> questionarioRespostas { get; set; }
        public List<AvaliacaoRespostasModel> avaliacaoRespostas { get; set; }
        public AvaliacaoSugestaoModel ?avaliacaoSugestao { get; set; }
    }
}
