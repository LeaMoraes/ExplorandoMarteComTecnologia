using ExplorandoMarteComTecnologia_WPF.Controllers;
using ExplorandoMarteComTecnologia_WPF.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ExplorandoMarteComTecnologia_WPF.Views
{
    /// <summary>
    /// Interação lógica para Avaliacao.xam
    /// </summary>
    public partial class Avaliacao : Page
    {

        DispatcherTimer timer = new DispatcherTimer();
        public Avaliacao()
        {
            InitializeComponent();
            pcbFundoElementos.Visibility = Visibility.Hidden;
            TecladoVisivel(false);
            AvancarPergunta(perguntaId);
            ConfigurarTimer();
        }

        public string mensagemMainFrame = "";
        int perguntaId = 1;
        int respostaRespondida = 0;
        string respostaSelecionada = "";
        string notaQualidadeExposicoes = "";
        string notaInteracaoTotemSite = "";
        string notaInformacoesClaras = "";
        int shiftOpcao = 1;


        private void ConfigurarTimer()
        {
            timer.Interval = TimeSpan.FromMinutes(1); // 1 minuto
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Código para retornar ao menu

            // Parar o timer após o tick
            timer.Stop(); 
            Window janela = Window.GetWindow(this);
            if (janela != null)
            {
                janela.Close();
            }
        }

        private void ResetarTimer()
        {
            timer.Stop(); // Para o timer
            timer.Start(); // Reinicia o timer, voltando a contar do zero
        }


        private void BotoesAvaliacaoVisivel(bool visivel)
        {
            switch (visivel)
            {
                case true:
                    pcbPessimo.Visibility = Visibility.Visible;
                    pcbRuim.Visibility = Visibility.Visible;
                    pcbRegular.Visibility = Visibility.Visible;
                    pcbBom.Visibility = Visibility.Visible;
                    pcbExcelente.Visibility = Visibility.Visible;
                    break;

                case false:
                    pcbPessimo.Visibility = Visibility.Hidden;
                    pcbRuim.Visibility = Visibility.Hidden;
                    pcbRegular.Visibility = Visibility.Hidden;
                    pcbBom.Visibility = Visibility.Hidden;
                    pcbExcelente.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void AtualizarBotoes(string alternativa)
        {
            string caminhoBotaoExcelente = "/Imagens/Botoes/Avaliacao/BotaoExcelente.png";
            string caminhoBotaoBom = "/Imagens/Botoes/Avaliacao/BotaoBom.png";
            string caminhoBotaoRegular = "/Imagens/Botoes/Avaliacao/BotaoRegular.png";
            string caminhoBotaoRuim = "/Imagens/Botoes/Avaliacao/BotaoRuim.png";
            string caminhoBotaoPessimo = "/Imagens/Botoes/Avaliacao/BotaoPessimo.png";

            pcbExcelente.Source = new BitmapImage(new Uri(caminhoBotaoExcelente, UriKind.Relative));
            pcbBom.Source = new BitmapImage(new Uri(caminhoBotaoBom, UriKind.Relative));
            pcbRegular.Source = new BitmapImage(new Uri(caminhoBotaoRegular, UriKind.Relative));
            pcbRuim.Source = new BitmapImage(new Uri(caminhoBotaoRuim, UriKind.Relative));
            pcbPessimo.Source = new BitmapImage(new Uri(caminhoBotaoPessimo, UriKind.Relative));


            switch (alternativa)
            {
                case "Excelente":
                    string caminhoBotaoExcelenteSelecionado = "/Imagens/Botoes/Avaliacao/BotaoExcelenteSelecionado.png";
                    pcbExcelente.Source = new BitmapImage(new Uri(caminhoBotaoExcelenteSelecionado, UriKind.Relative));
                    break;
                case "Bom":
                    string caminhoBotaoBomSelecionado = "/Imagens/Botoes/Avaliacao/BotaoBomSelecionado.png";
                    pcbBom.Source = new BitmapImage(new Uri(caminhoBotaoBomSelecionado, UriKind.Relative));
                    break;
                case "Regular":
                    string caminhoBotaoRegularSelecionado = "/Imagens/Botoes/Avaliacao/BotaoRegularSelecionado.png";
                    pcbRegular.Source = new BitmapImage(new Uri(caminhoBotaoRegularSelecionado, UriKind.Relative));
                    break;
                case "Ruim":
                    string caminhoBotaoRuimSelecionado = "/Imagens/Botoes/Avaliacao/BotaoRuimSelecionado.png";
                    pcbRuim.Source = new BitmapImage(new Uri(caminhoBotaoRuimSelecionado, UriKind.Relative));
                    break;
                case "Pessimo":
                    string caminhoBotaoPessimoSelecionado = "/Imagens/Botoes/Avaliacao/BotaoPessimoSelecionado.png";
                    pcbPessimo.Source = new BitmapImage(new Uri(caminhoBotaoPessimoSelecionado, UriKind.Relative));
                    break;
                default:
                    break;
            }
        }

        private void BotoesVisiveis(bool visivel)
        {
            switch (visivel)
            {
                case true:
                    pcbPessimo.Visibility = Visibility.Visible;
                    pcbRuim.Visibility = Visibility.Visible;
                    pcbRegular.Visibility = Visibility.Visible;
                    pcbBom.Visibility = Visibility.Visible;
                    pcbExcelente.Visibility = Visibility.Visible;
                    break;

                case false:
                    pcbPessimo.Visibility = Visibility.Hidden;
                    pcbRuim.Visibility = Visibility.Hidden;
                    pcbRegular.Visibility = Visibility.Hidden;
                    pcbBom.Visibility = Visibility.Hidden;
                    pcbExcelente.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void AvancarPergunta(int perguntaId)
        {
            switch (perguntaId)
            {
                case 1:
                    TecladoVisivel(false);
                    BotoesAvaliacaoVisivel(true);
                    AtualizarBotoes("Cair num Default");
                    respostaRespondida = 0;
                    respostaSelecionada = "";
                    lblPergunta.Content = "Como você avalia a qualidade das\nexposições apresentadas no museu?";

                    txbComentario.Visibility = Visibility.Hidden;
                    BotoesVisiveis(true);

                    lblMensagem.Visibility = Visibility.Hidden;
                    lblMensagem.Content = "";

                   
                    break;

                case 2:
                    TecladoVisivel(false);
                    BotoesAvaliacaoVisivel(true);
                    AtualizarBotoes("Cair num Default");
                    respostaRespondida = 0;
                    respostaSelecionada = "";
                    lblPergunta.Content = "Como você classifica a experiência\ngeral de interação com o totem/site?";

                    txbComentario.Visibility = Visibility.Hidden;
                    BotoesVisiveis(true);

                    lblMensagem.Visibility = Visibility.Hidden;
                    lblMensagem.Content = "";

                    
                    break;

                case 3:
                    TecladoVisivel(false);
                    BotoesAvaliacaoVisivel(true);
                    AtualizarBotoes("Cair num Default");
                    respostaRespondida = 0;
                    respostaSelecionada = "";
                    lblPergunta.Content = "Como você avalia a clareza das informações\nfornecidas nas descrições das obras?";

                    txbComentario.Visibility = Visibility.Hidden;
                    BotoesVisiveis(true);

                    lblMensagem.Visibility = Visibility.Hidden;
                    lblMensagem.Content = "";

                    AtualizarTeclas(shiftOpcao);

                   
                    break;

                case 4:
                    pcbFundoElementos.Visibility = Visibility.Visible;
                    TecladoVisivel(true);
                    AtualizarTeclas(shiftOpcao);
                    BotoesAvaliacaoVisivel(false);
                    AtualizarBotoes("Cair num Default");
                    lblPergunta.Content = "Sua opinião é importante para nós.\nDeixe seu comentário! (Opcional)";

                    txbComentario.Visibility = Visibility.Visible;
                    BotoesVisiveis(false);

                    lblMensagem.Visibility = Visibility.Hidden;
                    lblMensagem.Content = "";

                    
                    break;

                case 5:

                    Estatico.avaliacaoRespostas.Add(new AvaliacaoRespostasDTO
                    {
                        Id = 1,
                        Pergunta = "QualidadeExposicoes",
                        Excelente = notaQualidadeExposicoes == "Excelente" ? 1 : 0,
                        Bom = notaQualidadeExposicoes == "Bom" ? 1 : 0,
                        Regular = notaQualidadeExposicoes == "Regular" ? 1 : 0,
                        Ruim = notaQualidadeExposicoes == "Ruim" ? 1 : 0,
                        Pessimo = notaQualidadeExposicoes == "Pessimo" ? 1 : 0
                    });

                    Estatico.avaliacaoRespostas.Add(new AvaliacaoRespostasDTO
                    {
                        Id = 2,
                        Pergunta = "InteracaoTotemSite",
                        Excelente = notaInteracaoTotemSite == "Excelente" ? 1 : 0,
                        Bom = notaInteracaoTotemSite == "Bom" ? 1 : 0,
                        Regular = notaInteracaoTotemSite == "Regular" ? 1 : 0,
                        Ruim = notaInteracaoTotemSite == "Ruim" ? 1 : 0,
                        Pessimo = notaInteracaoTotemSite == "Pessimo" ? 1 : 0
                    });

                    Estatico.avaliacaoRespostas.Add(new AvaliacaoRespostasDTO
                    {
                        Id = 3,
                        Pergunta = "InformacoesClaras",
                        Excelente = notaInformacoesClaras == "Excelente" ? 1 : 0,
                        Bom = notaInformacoesClaras == "Bom" ? 1 : 0,
                        Regular = notaInformacoesClaras == "Regular" ? 1 : 0,
                        Ruim = notaInformacoesClaras == "Ruim" ? 1 : 0,
                        Pessimo = notaInformacoesClaras == "Pessimo" ? 1 : 0
                    });

                    if (!string.IsNullOrEmpty(txbComentario.Text))
                    {
                        Estatico.COMENTARIO = txbComentario.Text;
                    }





                    //tmrTempoAusencia.Enabled = false;
              


                    

                    Window janela = Window.GetWindow(this);

                    Estatico.MENSAGEMCOMUNICACAO = "ARMAZENAR";

                    if (janela != null)
                    {
                        janela.Close();
                    }

                    break;

                default:
                    perguntaId = 1;
                    AvancarPergunta(1);
                    break;

            }
        }

        private void pcbPessimo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            AtualizarBotoes("Pessimo");
            respostaRespondida = 1;
            respostaSelecionada = "Pessimo";

        }

        private void pcbRuim_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            AtualizarBotoes("Ruim");
            respostaRespondida = 1;
            respostaSelecionada = "Ruim";
        }

        private void pcbRegular_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            AtualizarBotoes("Regular");
            respostaRespondida = 1;
            respostaSelecionada = "Regular";
        }

        private void pcbBom_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            AtualizarBotoes("Bom");
            respostaRespondida = 1;
            respostaSelecionada = "Bom";
        }

        private void pcbExcelente_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            AtualizarBotoes("Excelente");
            respostaRespondida = 1;
            respostaSelecionada = "Excelente";
        }

        private void pcbContinuar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            if (!string.IsNullOrEmpty(respostaSelecionada))
            {

                switch (perguntaId)
                {
                    case 1:
                        notaQualidadeExposicoes = respostaSelecionada;
                        break;

                    case 2:
                        notaInteracaoTotemSite = respostaSelecionada;
                        break;

                    case 3:
                        notaInformacoesClaras = respostaSelecionada;
                        break;

                    default:
                        break;
                }

                perguntaId += 1;
                AvancarPergunta(perguntaId);
            }
            else
            {
                lblMensagem.Visibility = Visibility.Visible;
                lblMensagem.Content = "Por favor, escolha uma opção.";
            }
        }




        #region Teclado


        private void TecladoVisivel(bool visivel)
        {
            switch (visivel)
            {
                case true:
                    pcbKeyA.Visibility = Visibility.Visible;
                    pcbKeyB.Visibility = Visibility.Visible;
                    pcbKeyC.Visibility = Visibility.Visible;
                    pcbKeyD.Visibility = Visibility.Visible;
                    pcbKeyE.Visibility = Visibility.Visible;
                    pcbKeyF.Visibility = Visibility.Visible;
                    pcbKeyG.Visibility = Visibility.Visible;
                    pcbKeyH.Visibility = Visibility.Visible;
                    pcbKeyI.Visibility = Visibility.Visible;
                    pcbKeyJ.Visibility = Visibility.Visible;
                    pcbKeyK.Visibility = Visibility.Visible;
                    pcbKeyL.Visibility = Visibility.Visible;
                    pcbKeyM.Visibility = Visibility.Visible;
                    pcbKeyN.Visibility = Visibility.Visible;
                    pcbKeyO.Visibility = Visibility.Visible;
                    pcbKeyP.Visibility = Visibility.Visible;
                    pcbKeyQ.Visibility = Visibility.Visible;
                    pcbKeyR.Visibility = Visibility.Visible;
                    pcbKeyS.Visibility = Visibility.Visible;
                    pcbKeyT.Visibility = Visibility.Visible;
                    pcbKeyU.Visibility = Visibility.Visible;
                    pcbKeyV.Visibility = Visibility.Visible;
                    pcbKeyW.Visibility = Visibility.Visible;
                    pcbKeyX.Visibility = Visibility.Visible;
                    pcbKeyY.Visibility = Visibility.Visible;
                    pcbKeyZ.Visibility = Visibility.Visible;
                    pcbKeyÇ.Visibility = Visibility.Visible;
                    pcbKeyShift.Visibility = Visibility.Visible;
                    pcbKeySpace.Visibility = Visibility.Visible;
                    pcbKeyBackspace.Visibility = Visibility.Visible;
                    pcbKeyComma.Visibility = Visibility.Visible;
                    pcbKeyDot.Visibility = Visibility.Visible;
                    break;

                case false:
                    pcbKeyA.Visibility = Visibility.Hidden;
                    pcbKeyB.Visibility = Visibility.Hidden;
                    pcbKeyC.Visibility = Visibility.Hidden;
                    pcbKeyD.Visibility = Visibility.Hidden;
                    pcbKeyE.Visibility = Visibility.Hidden;
                    pcbKeyF.Visibility = Visibility.Hidden;
                    pcbKeyG.Visibility = Visibility.Hidden;
                    pcbKeyH.Visibility = Visibility.Hidden;
                    pcbKeyI.Visibility = Visibility.Hidden;
                    pcbKeyJ.Visibility = Visibility.Hidden;
                    pcbKeyK.Visibility = Visibility.Hidden;
                    pcbKeyL.Visibility = Visibility.Hidden;
                    pcbKeyM.Visibility = Visibility.Hidden;
                    pcbKeyN.Visibility = Visibility.Hidden;
                    pcbKeyO.Visibility = Visibility.Hidden;
                    pcbKeyP.Visibility = Visibility.Hidden;
                    pcbKeyQ.Visibility = Visibility.Hidden;
                    pcbKeyR.Visibility = Visibility.Hidden;
                    pcbKeyS.Visibility = Visibility.Hidden;
                    pcbKeyT.Visibility = Visibility.Hidden;
                    pcbKeyU.Visibility = Visibility.Hidden;
                    pcbKeyV.Visibility = Visibility.Hidden;
                    pcbKeyW.Visibility = Visibility.Hidden;
                    pcbKeyX.Visibility = Visibility.Hidden;
                    pcbKeyY.Visibility = Visibility.Hidden;
                    pcbKeyZ.Visibility = Visibility.Hidden;
                    pcbKeyÇ.Visibility = Visibility.Hidden;
                    pcbKeyShift.Visibility = Visibility.Hidden;
                    pcbKeySpace.Visibility = Visibility.Hidden;
                    pcbKeyBackspace.Visibility = Visibility.Hidden;
                    pcbKeyComma.Visibility = Visibility.Hidden;
                    pcbKeyDot.Visibility = Visibility.Hidden;

                    break;
            }
        }

        public void AtualizarTeclas(int shiftOpcao)
        {
            switch (shiftOpcao)
            {
                case 0:
                    pcbKeyA.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Minusculo/a.png", UriKind.Relative));
                    pcbKeyB.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Minusculo/b.png", UriKind.Relative));
                    pcbKeyC.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Minusculo/c.png", UriKind.Relative));
                    pcbKeyD.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Minusculo/d.png", UriKind.Relative));
                    pcbKeyE.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Minusculo/e.png", UriKind.Relative));
                    pcbKeyF.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Minusculo/f.png", UriKind.Relative));
                    pcbKeyG.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Minusculo/g.png", UriKind.Relative));
                    pcbKeyH.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Minusculo/h.png", UriKind.Relative));
                    pcbKeyI.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Minusculo/i.png", UriKind.Relative));
                    pcbKeyJ.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Minusculo/j.png", UriKind.Relative));
                    pcbKeyK.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Minusculo/k.png", UriKind.Relative));
                    pcbKeyL.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Minusculo/l.png", UriKind.Relative));
                    pcbKeyM.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Minusculo/m.png", UriKind.Relative));
                    pcbKeyN.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Minusculo/n.png", UriKind.Relative));
                    pcbKeyO.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Minusculo/o.png", UriKind.Relative));
                    pcbKeyP.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Minusculo/p.png", UriKind.Relative));
                    pcbKeyQ.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Minusculo/q.png", UriKind.Relative));
                    pcbKeyR.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Minusculo/r.png", UriKind.Relative));
                    pcbKeyS.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Minusculo/s.png", UriKind.Relative));
                    pcbKeyT.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Minusculo/t.png", UriKind.Relative));
                    pcbKeyU.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Minusculo/u.png", UriKind.Relative));
                    pcbKeyV.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Minusculo/v.png", UriKind.Relative));
                    pcbKeyW.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Minusculo/w.png", UriKind.Relative));
                    pcbKeyX.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Minusculo/x.png", UriKind.Relative));
                    pcbKeyY.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Minusculo/y.png", UriKind.Relative));
                    pcbKeyZ.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Minusculo/z.png", UriKind.Relative));
                    pcbKeyÇ.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Minusculo/ç.png", UriKind.Relative));
                    pcbKeyShift.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/ShiftDesligado.png", UriKind.Relative));
                    break;

                case 1:
                    pcbKeyA.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Maiusculas/A.png", UriKind.Relative));
                    pcbKeyB.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Maiusculas/B.png", UriKind.Relative));
                    pcbKeyC.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Maiusculas/C.png", UriKind.Relative));
                    pcbKeyD.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Maiusculas/D.png", UriKind.Relative));
                    pcbKeyE.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Maiusculas/E.png", UriKind.Relative));
                    pcbKeyF.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Maiusculas/F.png", UriKind.Relative));
                    pcbKeyG.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Maiusculas/G.png", UriKind.Relative));
                    pcbKeyH.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Maiusculas/H.png", UriKind.Relative));
                    pcbKeyI.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Maiusculas/I.png", UriKind.Relative));
                    pcbKeyJ.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Maiusculas/J.png", UriKind.Relative));
                    pcbKeyK.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Maiusculas/K.png", UriKind.Relative));
                    pcbKeyL.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Maiusculas/L.png", UriKind.Relative));
                    pcbKeyM.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Maiusculas/M.png", UriKind.Relative));
                    pcbKeyN.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Maiusculas/N.png", UriKind.Relative));
                    pcbKeyO.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Maiusculas/O.png", UriKind.Relative));
                    pcbKeyP.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Maiusculas/P.png", UriKind.Relative));
                    pcbKeyQ.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Maiusculas/Q.png", UriKind.Relative));
                    pcbKeyR.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Maiusculas/R.png", UriKind.Relative));
                    pcbKeyS.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Maiusculas/S.png", UriKind.Relative));
                    pcbKeyT.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Maiusculas/T.png", UriKind.Relative));
                    pcbKeyU.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Maiusculas/U.png", UriKind.Relative));
                    pcbKeyV.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Maiusculas/V.png", UriKind.Relative));
                    pcbKeyW.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Maiusculas/W.png", UriKind.Relative));
                    pcbKeyX.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Maiusculas/X.png", UriKind.Relative));
                    pcbKeyY.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Maiusculas/Y.png", UriKind.Relative));
                    pcbKeyZ.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Maiusculas/Z.png", UriKind.Relative));
                    pcbKeyÇ.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/Maiusculas/Ç.png", UriKind.Relative));
                    pcbKeyShift.Source = new BitmapImage(new Uri("/Imagens/Botoes/Teclado/ShiftAtivo.png", UriKind.Relative));
                    break;
            }
        }

        

        private void pcbKeyBackspace_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            if (txbComentario.Text.Length > 0)
            {
                txbComentario.Text = txbComentario.Text.Substring(0, txbComentario.Text.Length - 1);
            }
        }

        private void pcbKeyA_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            if (txbComentario.Text.Length < txbComentario.MaxLength)
            {
                Controle controle = new Controle();
                switch (shiftOpcao)
                {
                    case 0:
                        txbComentario.Text += controle.Teclado(27);
                        break;
                    case 1:
                        txbComentario.Text += controle.Teclado(1);
                        break;
                }
            }
        }

        private void pcbKeyB_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            if (txbComentario.Text.Length < txbComentario.MaxLength)
            {
                Controle controle = new Controle();
                switch (shiftOpcao)
                {
                    case 0:
                        txbComentario.Text += controle.Teclado(28);
                        break;
                    case 1:
                        txbComentario.Text += controle.Teclado(2);
                        break;
                }
            }
        }

        private void pcbKeyC_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            if (txbComentario.Text.Length < txbComentario.MaxLength)
            {
                Controle controle = new Controle();
                switch (shiftOpcao)
                {
                    case 0:
                        txbComentario.Text += controle.Teclado(29);
                        break;
                    case 1:
                        txbComentario.Text += controle.Teclado(3);
                        break;
                }
            }
        }

        private void pcbKeyD_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            if (txbComentario.Text.Length < txbComentario.MaxLength)
            {
                Controle controle = new Controle();
                switch (shiftOpcao)
                {
                    case 0:
                        txbComentario.Text += controle.Teclado(30);
                        break;
                    case 1:
                        txbComentario.Text += controle.Teclado(4);
                        break;
                }
            }
        }

        private void pcbKeyE_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            if (txbComentario.Text.Length < txbComentario.MaxLength)
            {
                Controle controle = new Controle();
                switch (shiftOpcao)
                {
                    case 0:
                        txbComentario.Text += controle.Teclado(31);
                        break;
                    case 1:
                        txbComentario.Text += controle.Teclado(5);
                        break;
                }
            }
        }

        private void pcbKeyF_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            if (txbComentario.Text.Length < txbComentario.MaxLength)
            {
                Controle controle = new Controle();
                switch (shiftOpcao)
                {
                    case 0:
                        txbComentario.Text += controle.Teclado(32);
                        break;
                    case 1:
                        txbComentario.Text += controle.Teclado(6);
                        break;
                }
            }
        }

        private void pcbKeyG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            if (txbComentario.Text.Length < txbComentario.MaxLength)
            {
                Controle controle = new Controle();
                switch (shiftOpcao)
                {
                    case 0:
                        txbComentario.Text += controle.Teclado(33);
                        break;
                    case 1:
                        txbComentario.Text += controle.Teclado(7);
                        break;
                }
            }
        }

        private void pcbKeyH_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            if (txbComentario.Text.Length < txbComentario.MaxLength)
            {
                Controle controle = new Controle();
                switch (shiftOpcao)
                {
                    case 0:
                        txbComentario.Text += controle.Teclado(34);
                        break;
                    case 1:
                        txbComentario.Text += controle.Teclado(8);
                        break;
                }
            }
        }

        private void pcbKeyI_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            if (txbComentario.Text.Length < txbComentario.MaxLength)
            {
                Controle controle = new Controle();
                switch (shiftOpcao)
                {
                    case 0:
                        txbComentario.Text += controle.Teclado(35);
                        break;
                    case 1:
                        txbComentario.Text += controle.Teclado(9);
                        break;
                }
            }
        }

        private void pcbKeyJ_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            if (txbComentario.Text.Length < txbComentario.MaxLength)
            {
                Controle controle = new Controle();
                switch (shiftOpcao)
                {
                    case 0:
                        txbComentario.Text += controle.Teclado(36);
                        break;
                    case 1:
                        txbComentario.Text += controle.Teclado(10);
                        break;
                }
            }
        }

        private void pcbKeyK_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            if (txbComentario.Text.Length < txbComentario.MaxLength)
            {
                Controle controle = new Controle();
                switch (shiftOpcao)
                {
                    case 0:
                        txbComentario.Text += controle.Teclado(37);
                        break;
                    case 1:
                        txbComentario.Text += controle.Teclado(11);
                        break;
                }
            }
        }




        private void pcbKeyL_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            if (txbComentario.Text.Length < txbComentario.MaxLength)
            {
                Controle controle = new Controle();
                switch (shiftOpcao)
                {
                    case 0:
                        txbComentario.Text += controle.Teclado(38);
                        break;

                    case 1:
                        txbComentario.Text += controle.Teclado(12);
                        break;
                }
            }
        }

        private void pcbKeyM_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            if (txbComentario.Text.Length < txbComentario.MaxLength)
            {
                Controle controle = new Controle();
                switch (shiftOpcao)
                {
                    case 0:
                        txbComentario.Text += controle.Teclado(39);
                        break;

                    case 1:
                        txbComentario.Text += controle.Teclado(13);
                        break;
                }
            }
        }

        private void pcbKeyN_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            if (txbComentario.Text.Length < txbComentario.MaxLength)
            {
                Controle controle = new Controle();
                switch (shiftOpcao)
                {
                    case 0:
                        txbComentario.Text += controle.Teclado(40);
                        break;

                    case 1:
                        txbComentario.Text += controle.Teclado(14);
                        break;
                }
            }
        }

        private void pcbKeyO_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            if (txbComentario.Text.Length < txbComentario.MaxLength)
            {
                Controle controle = new Controle();
                switch (shiftOpcao)
                {
                    case 0:
                        txbComentario.Text += controle.Teclado(41);
                        break;

                    case 1:
                        txbComentario.Text += controle.Teclado(15);
                        break;
                }
            }
        }

        private void pcbKeyP_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            if (txbComentario.Text.Length < txbComentario.MaxLength)
            {
                Controle controle = new Controle();
                switch (shiftOpcao)
                {
                    case 0:
                        txbComentario.Text += controle.Teclado(42);
                        break;

                    case 1:
                        txbComentario.Text += controle.Teclado(16);
                        break;
                }
            }
        }

        private void pcbKeyQ_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            if (txbComentario.Text.Length < txbComentario.MaxLength)
            {
                Controle controle = new Controle();
                switch (shiftOpcao)
                {
                    case 0:
                        txbComentario.Text += controle.Teclado(43);
                        break;

                    case 1:
                        txbComentario.Text += controle.Teclado(17);
                        break;
                }
            }
        }

        private void pcbKeyR_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            if (txbComentario.Text.Length < txbComentario.MaxLength)
            {
                Controle controle = new Controle();
                switch (shiftOpcao)
                {
                    case 0:
                        txbComentario.Text += controle.Teclado(44);
                        break;

                    case 1:
                        txbComentario.Text += controle.Teclado(18);
                        break;
                }
            }
        }

        private void pcbKeyS_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            if (txbComentario.Text.Length < txbComentario.MaxLength)
            {
                Controle controle = new Controle();
                switch (shiftOpcao)
                {
                    case 0:
                        txbComentario.Text += controle.Teclado(45);
                        break;

                    case 1:
                        txbComentario.Text += controle.Teclado(19);
                        break;
                }
            }
        }

        private void pcbKeyT_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            if (txbComentario.Text.Length < txbComentario.MaxLength)
            {
                Controle controle = new Controle();
                switch (shiftOpcao)
                {
                    case 0:
                        txbComentario.Text += controle.Teclado(46);
                        break;

                    case 1:
                        txbComentario.Text += controle.Teclado(20);
                        break;
                }
            }
        }

        private void pcbKeyU_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            if (txbComentario.Text.Length < txbComentario.MaxLength)
            {
                Controle controle = new Controle();
                switch (shiftOpcao)
                {
                    case 0:
                        txbComentario.Text += controle.Teclado(47);
                        break;

                    case 1:
                        txbComentario.Text += controle.Teclado(21);
                        break;
                }
            }
        }

        private void pcbKeyV_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            if (txbComentario.Text.Length < txbComentario.MaxLength)
            {
                Controle controle = new Controle();
                switch (shiftOpcao)
                {
                    case 0:
                        txbComentario.Text += controle.Teclado(48);
                        break;

                    case 1:
                        txbComentario.Text += controle.Teclado(22);
                        break;
                }
            }
        }

        private void pcbKeyW_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            if (txbComentario.Text.Length < txbComentario.MaxLength)
            {
                Controle controle = new Controle();
                switch (shiftOpcao)
                {
                    case 0:
                        txbComentario.Text += controle.Teclado(49);
                        break;

                    case 1:
                        txbComentario.Text += controle.Teclado(23);
                        break;
                }
            }
        }

        private void pcbKeyX_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            if (txbComentario.Text.Length < txbComentario.MaxLength)
            {
                Controle controle = new Controle();
                switch (shiftOpcao)
                {
                    case 0:
                        txbComentario.Text += controle.Teclado(50);
                        break;

                    case 1:
                        txbComentario.Text += controle.Teclado(24);
                        break;
                }
            }
        }

        private void pcbKeyY_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            if (txbComentario.Text.Length < txbComentario.MaxLength)
            {
                Controle controle = new Controle();
                switch (shiftOpcao)
                {
                    case 0:
                        txbComentario.Text += controle.Teclado(51);
                        break;

                    case 1:
                        txbComentario.Text += controle.Teclado(25);
                        break;
                }
            }
        }

        private void pcbKeyZ_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            if (txbComentario.Text.Length < txbComentario.MaxLength)
            {
                Controle controle = new Controle();
                switch (shiftOpcao)
                {
                    case 0:
                        txbComentario.Text += controle.Teclado(52);
                        break;

                    case 1:
                        txbComentario.Text += controle.Teclado(26);
                        break;
                }
            }
        }

        private void pcbKeyÇ_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            if (txbComentario.Text.Length < txbComentario.MaxLength)
            {
                Controle controle = new Controle();
                switch (shiftOpcao)
                {
                    case 0:
                        txbComentario.Text += controle.Teclado(57);
                        break;

                    case 1:
                        txbComentario.Text += controle.Teclado(56);
                        break;
                }
            }
        }

        private void pcbKeyComma_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            if (txbComentario.Text.Length < txbComentario.MaxLength)
            {
                Controle controle = new Controle();
                txbComentario.Text += controle.Teclado(53);
            }
        }

        private void pcbKeyDot_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            if (txbComentario.Text.Length < txbComentario.MaxLength)
            {
                Controle controle = new Controle();
                txbComentario.Text += controle.Teclado(54);
            }
        }

        private void pcbKeySpace_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            if (txbComentario.Text.Length < txbComentario.MaxLength)
            {
                Controle controle = new Controle();
                txbComentario.Text += controle.Teclado(55);
            }
        }

        private void pcbKeyShift_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetarTimer();
            switch (shiftOpcao)
            {
                case 0:
                    shiftOpcao = 1;
                    break;

                case 1:
                    shiftOpcao = 0;
                    break;

                default:
                    shiftOpcao = 1;
                    break;
            }
            AtualizarTeclas(shiftOpcao);
        }

        #endregion

    }
}
