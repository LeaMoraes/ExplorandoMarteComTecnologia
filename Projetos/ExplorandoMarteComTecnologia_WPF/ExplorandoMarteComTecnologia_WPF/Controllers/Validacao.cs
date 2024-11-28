using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplorandoMarteComTecnologia_WPF.Controllers
{
    internal class Validacao
    {
        internal Controle Controle
        {
            get => default;
            set
            {
            }
        }

        public int ConverterStringParaInt(string valor)
        {
            try
            {
                return Convert.ToInt32(valor);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int ValidarResposta(int idPergunta, string Resposta)
        {

            switch (idPergunta)
            {

                case 1:
                    Estatico.TEMPRESPOSTAQUEST1 = Resposta;
                    if (Resposta.Equals("D) Monte Olimpo, com 27 km de altura"))
                    {
                        return 1;
                    }

                    break;

                case 2:
                    Estatico.TEMPRESPOSTAQUEST2 = Resposta;
                    if (Resposta.Equals("C) Elon Musk"))
                    {
                        return 1;
                    }
                    break;

                case 3:
                    Estatico.TEMPRESPOSTAQUEST3 = Resposta;
                    if (Resposta.Equals("B) A presença de óxido de ferro em sua superfície"))
                    {
                        return 1;
                    }
                    break;

                case 4:
                    Estatico.TEMPRESPOSTAQUEST4 = Resposta;
                    if (Resposta.Equals("A) Evidências de água líquida em rios e lagos secos."))
                    {
                        return 1;
                    }
                    break;

                case 5:
                    Estatico.TEMPRESPOSTAQUEST5 = Resposta;
                    if (Resposta.Equals("C) Missões robóticas para mapear a superfície."))
                    {
                        return 1;
                    }
                    break;

                default:

                    break;

            }

            return 0;

        }


    }
}
