namespace ExplorandoMarteComTecnologia_API.DTO
{
    public class QuestionarioRespostasDTO
    {
        public required string Pergunta { get; set; }
        public int Acertos { get; set; }
        public int Erros { get; set; }
    }
}
