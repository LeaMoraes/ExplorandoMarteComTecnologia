using ExplorandoMarteComTecnologia_WPF.DTO;
using ExplorandoMarteComTecnologia_WPF.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplorandoMarteComTecnologia_WPF.Controllers
{
    internal static class Estatico
    {

       public static string LINKAPI = "";
        public static string ERROMENSAGEM = "";

        public static int RELATORIOPAGINA = 1; //Gambiarra para resolver um bug

       public static List<QuestionarioRespostasModel> questionarioRespostas = new List<QuestionarioRespostasModel>();
       public static List<AvaliacaoRespostasDTO> avaliacaoRespostas = new List<AvaliacaoRespostasDTO>();
       public static string COMENTARIO = "";

       public static int QUANTIDADEACERTOSQUEST1 = 0;
       public static int QUANTIDADEACERTOSQUEST2 = 0;
       public static int QUANTIDADEACERTOSQUEST3 = 0;
       public static int QUANTIDADEACERTOSQUEST4 = 0;
       public static int QUANTIDADEACERTOSQUEST5 = 0;

       public static int QUANTIDADEERROSQUEST1 = 0;
       public static int QUANTIDADEERROSQUEST2 = 0;
       public static int QUANTIDADEERROSQUEST3 = 0;
       public static int QUANTIDADEERROSQUEST4 = 0;
       public static int QUANTIDADEERROSQUEST5 = 0;

        public static string TEMPRESPOSTAQUEST1 = "";
       public static string TEMPRESPOSTAQUEST2 = "";
       public static string TEMPRESPOSTAQUEST3 = "";
       public static string TEMPRESPOSTAQUEST4 = "";
       public static string TEMPRESPOSTAQUEST5 = "";

       public static string MENSAGEMCOMUNICACAO = "";

    }
}
