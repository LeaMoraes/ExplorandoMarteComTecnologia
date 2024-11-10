namespace ExplorandoMarteComTecnologia_API.Models
{
    public class AvaliacaoRespostasModel
    {
        public int Id { get; set; }
        public required string Pergunta { get; set; }
        public int Excelente { get; set; }
        public int Bom { get; set; }
        public int Regular { get; set; }
        public int Ruim { get; set; }
        public int Pessimo { get; set; }
    }
}
