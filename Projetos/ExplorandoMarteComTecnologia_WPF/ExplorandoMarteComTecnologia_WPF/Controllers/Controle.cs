using ExplorandoMarteComTecnologia_WPF.DTO;
using ExplorandoMarteComTecnologia_WPF.Modelos;
using ExplorandoMarteComTecnologia_WPF.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ExplorandoMarteComTecnologia_WPF.Controllers
{
    internal class Controle
    {
        public int ConverterStringParaInt(string valor)
        {
            Validacao validacao = new Validacao();
            return validacao.ConverterStringParaInt(valor);
        }

        public void ValidarResposta(int idPergunta, string resposta)
        {
            Validacao validacao = new Validacao();
            switch(validacao.ValidarResposta(idPergunta, resposta))
            {
                case 0:
                    Estatico.questionarioRespostas.Add(new QuestionarioRespostasModel { Acertos = 0, Erros = 1 });
                    break;

                case 1:
                    Estatico.questionarioRespostas.Add(new QuestionarioRespostasModel { Acertos = 1, Erros = 0 });
                    break;
            }
        }

        public string MontarJsonQuestionarioAvaliacao(List<QuestionarioRespostasModel> questionarioRespostas, List<AvaliacaoRespostasDTO> avaliacaoRespostas, string sugestao)
        {
            List<QuestionarioRespostasDTO> questionarioDTO = new List<QuestionarioRespostasDTO>();
            for (int i = 0; i < questionarioRespostas.Count; i++)
            {
                if (i >= 5)
                {
                    return null;
                }
                string pergunta = IdentificarPergunta(i);
                questionarioDTO.Add(new QuestionarioRespostasDTO
                {
                    Id = i + 1,
                    Pergunta = pergunta,
                    Acertos = questionarioRespostas[i].Acertos,
                    Erros = questionarioRespostas[i].Erros
                });
            }

            if (string.IsNullOrEmpty(sugestao))
            {
                var requestSemSugestao = new QuestionarioAvaliacaoSemSugestao
                {
                    QuestionarioRespostas = questionarioDTO,
                    AvaliacaoRespostas = avaliacaoRespostas
                };

                return JsonConvert.SerializeObject(requestSemSugestao, Formatting.Indented);
            }

            var request = new QuestionarioAvaliacaoRequest
            {
                QuestionarioRespostas = questionarioDTO,
                AvaliacaoRespostas = avaliacaoRespostas,
                AvaliacaoSugestao = string.IsNullOrEmpty(sugestao) ? null : new AvaliacaoSugestao { Sugestao = sugestao }
            };

            return JsonConvert.SerializeObject(request, Formatting.Indented);
        }
        

        private string IdentificarPergunta(int id)
        {
            switch (id)
            {
                case 0:
                    return "VulcaoSistemaSolar";

                case 1:
                    return "FundadorSpaceX";

                case 2:
                    return "FenomenoVermelho";

                case 3:
                    return "VidaPassada";

                case 4:
                    return "MonteOlimpo";

                default:
                    return "";
            }
        }

        public async void AtualizarValoresAcertosErrosPerguntas(string endpoint)
        {
            ControllService controllService = new ControllService();
            List<QuestionarioRespostasDTO> questionarioRespostas = await controllService.GetDataAsync(endpoint);

            if(Estatico.ERROMENSAGEM == "ERRO")
            {
                return;
            }

            
            Estatico.QUANTIDADEACERTOSQUEST1 = questionarioRespostas[0].Acertos;
            Estatico.QUANTIDADEERROSQUEST1 = questionarioRespostas[0].Erros;
            Estatico.QUANTIDADEACERTOSQUEST2 = questionarioRespostas[1].Acertos;
            Estatico.QUANTIDADEERROSQUEST2 = questionarioRespostas[1].Erros;
            Estatico.QUANTIDADEACERTOSQUEST3 = questionarioRespostas[2].Acertos;
            Estatico.QUANTIDADEERROSQUEST3 = questionarioRespostas[2].Erros;
            Estatico.QUANTIDADEACERTOSQUEST4 = questionarioRespostas[3].Acertos;
            Estatico.QUANTIDADEERROSQUEST4 = questionarioRespostas[3].Erros;
            Estatico.QUANTIDADEACERTOSQUEST5 = questionarioRespostas[4].Acertos;
            Estatico.QUANTIDADEERROSQUEST5 = questionarioRespostas[4].Erros;

        }

        public async Task ArmazenarRespostas(string endpoint, string json)
        {
            ControllService controllService = new ControllService();
            if(endpoint != null && json != null)
            {
                await controllService.PutDataAsync(endpoint, json);
            }
            
        }

        #region Teclado
        public string Teclado(int KeyID)
        {
            switch (KeyID)
            {
                #region Letras Maiusculas
                case 1: return "A"; break;
                case 2: return "B"; break;
                case 3: return "C"; break;
                case 4: return "D"; break;
                case 5: return "E"; break;
                case 6: return "F"; break;
                case 7: return "G"; break;
                case 8: return "H"; break;
                case 9: return "I"; break;
                case 10: return "J"; break;
                case 11: return "K"; break;
                case 12: return "L"; break;
                case 13: return "M"; break;
                case 14: return "N"; break;
                case 15: return "O"; break;
                case 16: return "P"; break;
                case 17: return "Q"; break;
                case 18: return "R"; break;
                case 19: return "S"; break;
                case 20: return "T"; break;
                case 21: return "U"; break;
                case 22: return "V"; break;
                case 23: return "W"; break;
                case 24: return "X"; break;
                case 25: return "Y"; break;
                case 26: return "Z"; break;
                #endregion

                #region Letras Minusculas
                case 27: return "a"; break;
                case 28: return "b"; break;
                case 29: return "c"; break;
                case 30: return "d"; break;
                case 31: return "e"; break;
                case 32: return "f"; break;
                case 33: return "g"; break;
                case 34: return "h"; break;
                case 35: return "i"; break;
                case 36: return "j"; break;
                case 37: return "k"; break;
                case 38: return "l"; break;
                case 39: return "m"; break;
                case 40: return "n"; break;
                case 41: return "o"; break;
                case 42: return "p"; break;
                case 43: return "q"; break;
                case 44: return "r"; break;
                case 45: return "s"; break;
                case 46: return "t"; break;
                case 47: return "u"; break;
                case 48: return "v"; break;
                case 49: return "w"; break;
                case 50: return "x"; break;
                case 51: return "y"; break;
                case 52: return "z"; break;
                #endregion

                #region Teclas Especiais
                case 53: return ","; break;
                case 54: return "."; break;
                case 55: return " "; break;
                case 56: return "Ç"; break;
                case 57: return "ç"; break;
                #endregion

                #region Numeros
                case 58: return "1"; break;
                case 59: return "2"; break;
                case 60: return "3"; break;
                case 61: return "4"; break;
                case 62: return "5"; break;
                case 63: return "6"; break;
                case 64: return "7"; break;
                case 65: return "8"; break;
                case 66: return "9"; break;
                case 67: return "0"; break;
                #endregion

                default: return " "; break;
            }

            return " ";
        }
        #endregion
    }
}
