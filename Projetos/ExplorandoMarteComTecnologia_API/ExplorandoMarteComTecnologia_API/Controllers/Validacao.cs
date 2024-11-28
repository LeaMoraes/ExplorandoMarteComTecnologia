using ExplorandoMarteComTecnologia_API.DTO;
using ExplorandoMarteComTecnologia_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ExplorandoMarteComTecnologia_API.Controllers
{
    public class Validacao
    {

        public (int ano, int mes, int dia) SepararConverterAnoMesDia(string anoMesDia)
        {
            string anoString = SepararAnoMesDia(anoMesDia).anoString;
            string mesString = SepararAnoMesDia(anoMesDia).mesString;
            string diaString = SepararAnoMesDia(anoMesDia).diaString;

            int ano = ConverterINTAnoMesDia(anoString, mesString, diaString).ano;
            int mes = ConverterINTAnoMesDia(anoString, mesString, diaString).mes;
            int dia = ConverterINTAnoMesDia(anoString, mesString, diaString).dia;
            return (ano, mes, dia);
        }

        public (decimal preco, int TotalIngressosInteiro, int TotalIngressosMeia, int TotalIngressosIsento) CalcularPreco(List<IngressoDTO> ingressosDTO)
        {
            decimal preco = 0;
            int TotalIngressosInteiro = 0;
            int TotalIngressosMeia = 0;
            int TotalIngressosIsento = 0;

            //Pega um ingresso, verifica o tipo escolhido
            //da o preço correspondente ao tipo, e adiciona na variavel Preco
            foreach (var ingressoObjectDTO in ingressosDTO)
            {
                switch (ingressoObjectDTO.Tipo)
                {
                    case "Inteiro":
                        preco += 20;
                        TotalIngressosInteiro += 1;
                        break;

                    case "Meia":
                        preco += 10;
                        TotalIngressosMeia += 1;
                        break;

                    case "Isento":
                        preco += 0;
                        TotalIngressosIsento += 1;
                        break;

                    default:
                        preco += 20;
                        TotalIngressosInteiro += 1;
                        break;
                }
            }

            //Retorna o preço cheio
            return (preco, TotalIngressosInteiro, TotalIngressosMeia, TotalIngressosIsento);
        }

        public bool VerificarCampos(NovaVendaDTO novavendaDTO)
        {
            List<IngressoDTO> ingressosValidar = novavendaDTO.ingressoDTO;
            VendaModel vendaValidar = novavendaDTO.venda;
            int totalIngressos = 0;

            foreach( var ingresso in ingressosValidar)
            {
                if (string.IsNullOrWhiteSpace(ingresso.Nome) || string.IsNullOrWhiteSpace(ingresso.Tipo))
                {
                    return false;
                }

                if (ingresso.Nome.Length < 3 || ingresso.Nome.Length > 100)
                {
                    return false;
                }

                if (ingresso.Tipo != "Inteiro" && ingresso.Tipo != "Meia" && ingresso.Tipo != "Isento")
                {
                    return false;
                }

                totalIngressos += 1;
            }

            if (string.IsNullOrWhiteSpace(vendaValidar.Email) || vendaValidar.Email.Length < 3)
            {
                return false;
            }

            if (!Regex.IsMatch(vendaValidar.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return false;
            }

            if (vendaValidar.Quantidade != totalIngressos)
            {
                return false;
            }

            if (vendaValidar.MetodoPagamento != "Credito" && vendaValidar.MetodoPagamento != "Debito" && vendaValidar.MetodoPagamento != "Pix")
            {
                return false;
            }

            return true;

        }

        public bool ValidarEmailKey(EmailKeyDTO emailKeyDTO)
        {
            if (string.IsNullOrWhiteSpace(emailKeyDTO.Email) || string.IsNullOrWhiteSpace(emailKeyDTO.Key))
            {
                return false;
            }

            if (emailKeyDTO.Email.Length < 3 || emailKeyDTO.Key.Length != 9)
            {
                return false;
            }

            if (!Regex.IsMatch(emailKeyDTO.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return false;
            }

            return true;
        }

        public (string email, string key) SepararEmailKey(string emailKey)
        {
            // Verifica se a string tem pelo menos o tamanho da key
            if (string.IsNullOrWhiteSpace(emailKey) || emailKey.Length <= 9)
            {
                throw new ArgumentException("O valor fornecido para emailKey não é válido.");
            }

            // Pega os últimos 9 caracteres para a key
            string key = emailKey.Substring(emailKey.Length - 9);

            // Pega o restante da string para o email
            string email = emailKey.Substring(0, emailKey.Length - 9);

            // Converte email para minúsculas e key para maiúsculas, se necessário
            email = ConverterParaMinusculas(email);
            key = ConverterParaMaiuscula(key);

            return (email, key);
        }

        public string ConverterParaMinusculas(string input)
        {
            return string.IsNullOrWhiteSpace(input) ? input : input.ToLowerInvariant();
        }
        public string ConverterParaMaiuscula(string input)
        {
            return string.IsNullOrWhiteSpace(input) ? input : input.ToUpperInvariant();
        }

        private (string anoString, string mesString, string diaString) SepararAnoMesDia(string anoMesDia)
        {
            string anoString = anoMesDia.Substring(0, 4);
            string mesString = anoMesDia.Substring(4, 2);
            string diaString = anoMesDia.Substring(6, 2);

            return (anoString, mesString, diaString);
        }

        private (int ano, int mes, int dia) ConverterINTAnoMesDia(string anoString, string mesString, string diaString)
        {
            int ano = Convert.ToInt32(anoString);
            int mes = Convert.ToInt32(mesString);
            int dia = Convert.ToInt32(diaString);

            return (ano, mes, dia);
        }

        public bool PerguntaExiste(List<QuestionarioRespostasModel> perguntas, string pergunta)
        {            
            var perguntaAchada = perguntas.SingleOrDefault(p => p.Pergunta.ToLower() == pergunta.ToLower());

            if (perguntaAchada == null)
            {
                return false;
            }

            return true;
        }

        public bool PerguntaExiste(List<AvaliacaoRespostasModel> perguntas, string pergunta)
        {
            var perguntaAchada = perguntas.SingleOrDefault(p => p.Pergunta.ToLower() == pergunta.ToLower());

            if (perguntaAchada == null)
            {
                return false;
            }

            return true;
        }

        public bool PerguntaIdIgual(List<QuestionarioRespostasModel> perguntas, int id, string pergunta)
        {
            
            var perguntaAchada = perguntas.SingleOrDefault(p => p.Id == id);

            if (perguntaAchada == null)
            {
                return false; // Não encontrou uma pergunta com o ID especificado
            }

            if (!perguntaAchada.Pergunta.ToLower().Equals(pergunta.ToLower()))
            {
                return false;
            }
            return true;
        }

        public bool PerguntaIdIgual(List<AvaliacaoRespostasModel> perguntas, int id, string pergunta)
        {

            var perguntaAchada = perguntas.SingleOrDefault(p => p.Id == id);

            if (perguntaAchada == null)
            {
                return false; // Não encontrou uma pergunta com o ID especificado
            }

            if (!perguntaAchada.Pergunta.ToLower().Equals(pergunta.ToLower()))
            {
                return false;
            }
            return true;
        }


    }
}
