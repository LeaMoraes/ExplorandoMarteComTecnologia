using ExplorandoMarteComTecnologia_WPF.Controllers;
using ExplorandoMarteComTecnologia_WPF.DTO;
using ExplorandoMarteComTecnologia_WPF.Views;
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
    /// Lógica interna para Questionario.xaml
    /// </summary>
    public partial class Questionario : Window
    {

        DispatcherTimer timer = new DispatcherTimer();
        public Questionario()
        {
            InitializeComponent();           
            Estatico.questionarioRespostas.Clear();
            Estatico.avaliacaoRespostas.Clear();
            Estatico.COMENTARIO = "";
            ProximaPergunta(1);

            ConfigurarTimer();

        }

        private void ResetarTimer()
        {
            timer.Stop(); // Para o timer
            timer.Start(); // Reinicia o timer, voltando a contar do zero
        }

        private void ConfigurarTimer()
        {
            timer.Interval = TimeSpan.FromMinutes(2); // 2 minutos
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Código para retornar ao menu

            // Parar o timer após o tick
            timer.Stop();
            this.Close();
        }

        int respostaSelecionada = 0;
        int idPergunta = 1;
        string alternativaEscolhida = "";



        private void AtualizarBotoes(string alternativa)
        {
            string caminhoBotaoA = "/Imagens/Botoes/Questionario/BotaoA.png";
            string caminhoBotaoB = "/Imagens/Botoes/Questionario/BotaoB.png";
            string caminhoBotaoC = "/Imagens/Botoes/Questionario/BotaoC.png";
            string caminhoBotaoD = "/Imagens/Botoes/Questionario/BotaoD.png";            

            pcbAlternativaA.Source = new BitmapImage(new Uri(caminhoBotaoA, UriKind.Relative));
            pcbAlternativaB.Source = new BitmapImage(new Uri(caminhoBotaoB, UriKind.Relative));
            pcbAlternativaC.Source = new BitmapImage(new Uri(caminhoBotaoC, UriKind.Relative));
            pcbAlternativaD.Source = new BitmapImage(new Uri(caminhoBotaoD, UriKind.Relative));


            switch (alternativa)
            {
                case "A":
                    string caminhoBotaoASelecionado = "/Imagens/Botoes/Questionario/BotaoASelecionado.png";
                    pcbAlternativaA.Source = new BitmapImage(new Uri(caminhoBotaoASelecionado, UriKind.Relative));
                    break;
                case "B":
                    string caminhoBotaoBSelecionado = "/Imagens/Botoes/Questionario/BotaoBSelecionado.png";
                    pcbAlternativaB.Source = new BitmapImage(new Uri(caminhoBotaoBSelecionado, UriKind.Relative));
                    break;
                case "C":
                    string caminhoBotaoCSelecionado = "/Imagens/Botoes/Questionario/BotaoCSelecionado.png";
                    pcbAlternativaC.Source = new BitmapImage(new Uri(caminhoBotaoCSelecionado, UriKind.Relative));
                    break;
                case "D":
                    string caminhoBotaoDSelecionado = "/Imagens/Botoes/Questionario/BotaoDSelecionado.png";
                    pcbAlternativaD.Source = new BitmapImage(new Uri(caminhoBotaoDSelecionado, UriKind.Relative));
                    break;
                default:
                    break;
            }
        }        

        public void ProximaPergunta(int idPergunta)
        {

            switch (idPergunta)
            {
                case 1:

                    alternativaEscolhida = "";
                    respostaSelecionada = 0;

                    AtualizarBotoes("CAIR OPCAO DEFAULT");

                    lblPergunta.Content = "1 - Qual é o maior vulcão do sistema solar?";
                    lblAlternativaA.Content = "A) Monte Everest, com 8.848 metros de altura";
                    lblAlternativaB.Content = "B) Mauna Kea, com 10.203 metros de altura";
                    lblAlternativaC.Content = "C) Monte Kilimanjaro, com 5.895 metros de altura";
                    lblAlternativaD.Content = "D) Monte Olimpo, com 27 km de altura";

                    lblMensagem.Visibility = Visibility.Hidden;
                    lblMensagem.Content = "";
                    break;

                case 2:

                    alternativaEscolhida = "";
                    respostaSelecionada = 0;

                    AtualizarBotoes("CAIR OPCAO DEFAULT");

                    lblPergunta.Content = "2 - A NASA tem planos concretos para enviar missões\ntripuladas para Marte na década de 2030\ncom a SpaceX. Quem fundou a SpaceX?";
                    lblAlternativaA.Content = "A) Michael “Lorde” Jackson";
                    lblAlternativaB.Content = "B) Neil Armstrong";
                    lblAlternativaC.Content = "C) Elon Musk";
                    lblAlternativaD.Content = "D) Sally Ride";

                    lblMensagem.Visibility = Visibility.Hidden;
                    lblMensagem.Content = "";
                    break;

                case 3:

                    alternativaEscolhida = "";
                    respostaSelecionada = 0;

                    AtualizarBotoes("CAIR OPCAO DEFAULT");

                    lblPergunta.Content = "3 - Qual é o fenômeno que deixa o planeta Marte com a sua\ncor vermelha?";
                    lblAlternativaA.Content = "A) O alto teor de carbono na atmosfera";
                    lblAlternativaB.Content = "B) A presença de óxido de ferro em sua superfície";
                    lblAlternativaC.Content = "C) A proximidade do Sol, causando oxidação intensa";
                    lblAlternativaD.Content = "D) A presença de muito planeta deixou com vergonha";

                    lblMensagem.Visibility = Visibility.Hidden;
                    lblMensagem.Content = "";
                    break;

                case 4:

                    alternativaEscolhida = "";
                    respostaSelecionada = 0;

                    AtualizarBotoes("CAIR OPCAO DEFAULT");

                    lblPergunta.Content = "4 - Qual descoberta sobre Marte pode indicar a possibilidade\nde vida no passado?";
                    lblAlternativaA.Content = "A) Evidências de água líquida em rios e lagos secos.";
                    lblAlternativaB.Content = "B) A presença de metano na atmosfera.";
                    lblAlternativaC.Content = "C) Traços de fósseis de pequenos organismos.";
                    lblAlternativaD.Content = "D) A descoberta de grandes depósitos de gelo.";

                    lblMensagem.Visibility = Visibility.Hidden;
                    lblMensagem.Content = "";
                    break;

                case 5:
                    alternativaEscolhida = "";
                    respostaSelecionada = 0;

                    AtualizarBotoes("CAIR OPCAO DEFAULT");

                    lblPergunta.Content = "5 - Qual das seguintes tecnologias será necessária para explorar\no Monte Olimpo em Marte?";
                    lblAlternativaA.Content = "A) Foguetes de propulsão solar.";
                    lblAlternativaB.Content = "B) Módulos infláveis para habitats humanos.";
                    lblAlternativaC.Content = "C) Missões robóticas para mapear a superfície.";
                    lblAlternativaD.Content = "D) Satélites com câmeras de alta resolução para\nsobrevoos contínuos.";

                    lblMensagem.Visibility = Visibility.Hidden;
                    lblMensagem.Content = "";
                    break;

                case 6:
                    //tmrTempoAusencia.Enabled = false;

                    Avaliacao avaliacao = new Avaliacao();
                    QuestionarioFrame.NavigationService.Navigate(avaliacao);
                    
                    break;

                default:
                    this.Close();
                    break;
            }

        }


        private void pcbAlternativaA_MouseDown(object sender, MouseButtonEventArgs e)
        {

            ResetarTimer();
            respostaSelecionada = 1;    //Feito para não ocorrer um clique duplo
            alternativaEscolhida = lblAlternativaA.Content.ToString();
            AtualizarBotoes("A");
        }

        private void pcbAlternativaB_MouseDown(object sender, MouseButtonEventArgs e)
        {

            ResetarTimer();
            respostaSelecionada = 1;    //Feito para não ocorrer um clique duplo
            alternativaEscolhida = lblAlternativaB.Content.ToString();
            AtualizarBotoes("B");
        }

        private void pcbAlternativaC_MouseDown(object sender, MouseButtonEventArgs e)
        {

            ResetarTimer();
            respostaSelecionada = 1;    //Feito para não ocorrer um clique duplo
            alternativaEscolhida = lblAlternativaC.Content.ToString();
            AtualizarBotoes("C");
        }

        private void pcbAlternativaD_MouseDown(object sender, MouseButtonEventArgs e)
        {

            ResetarTimer();
            respostaSelecionada = 1;    //Feito para não ocorrer um clique duplo
            alternativaEscolhida = lblAlternativaD.Content.ToString();
            AtualizarBotoes("D");
        }

        private void lblAlternativaA_MouseDown(object sender, MouseButtonEventArgs e)
        {

            ResetarTimer();
            respostaSelecionada = 1;    //Feito para não ocorrer um clique duplo
            alternativaEscolhida = lblAlternativaA.Content.ToString();
            AtualizarBotoes("A");
        }

        private void lblAlternativaB_MouseDown(object sender, MouseButtonEventArgs e)
        {

            ResetarTimer();
            respostaSelecionada = 1;    //Feito para não ocorrer um clique duplo
            alternativaEscolhida = lblAlternativaB.Content.ToString();
            AtualizarBotoes("B");
        }

        private void lblAlternativaC_MouseDown(object sender, MouseButtonEventArgs e)
        {

            ResetarTimer();
            respostaSelecionada = 1;    //Feito para não ocorrer um clique duplo
            alternativaEscolhida = lblAlternativaC.Content.ToString();
            AtualizarBotoes("C");
        }

        private void lblAlternativaD_MouseDown(object sender, MouseButtonEventArgs e)
        {

            ResetarTimer();
            respostaSelecionada = 1;    //Feito para não ocorrer um clique duplo
            alternativaEscolhida = lblAlternativaD.Content.ToString();
            AtualizarBotoes("D");
        }


        private void pcbContinuar_MouseDown(object sender, MouseButtonEventArgs e)
        {


            ResetarTimer();
            if (!respostaSelecionada.Equals(0))
            {
                Controle controle = new Controle();
                controle.ValidarResposta(idPergunta, alternativaEscolhida);
                idPergunta += 1;
                ProximaPergunta(idPergunta);
            }
            else
            {
                lblMensagem.Visibility = Visibility.Visible;
                lblMensagem.Content = "Selecione uma resposta!";
            }
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
