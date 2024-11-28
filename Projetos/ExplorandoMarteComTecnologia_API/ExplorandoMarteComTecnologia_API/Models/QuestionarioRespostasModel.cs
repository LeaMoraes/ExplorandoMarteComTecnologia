namespace ExplorandoMarteComTecnologia_API.Models
{
    public class QuestionarioRespostasModel
    {
        public int Id { get; set; }
        public required string Pergunta { get; set; }
        public int Acertos { get; set; }
        public int Erros { get; set; }


    }
}
