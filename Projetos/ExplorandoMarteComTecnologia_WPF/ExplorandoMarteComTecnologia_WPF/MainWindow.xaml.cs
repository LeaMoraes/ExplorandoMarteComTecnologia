using ExplorandoMarteComTecnologia_WPF.Controllers;
using ExplorandoMarteComTecnologia_WPF.Views;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExplorandoMarteComTecnologia_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }



        private async void btnQuestionario_Click(object sender, RoutedEventArgs e)
        {

            btnExposicoes.IsEnabled = false;
            btnMapa.IsEnabled = false;
            btnQuestionario.IsEnabled = false;

            Estatico.RELATORIOPAGINA = 1;

            Estatico.MENSAGEMCOMUNICACAO = "";
            Estatico.ERROMENSAGEM = "";

            if (String.IsNullOrEmpty(Estatico.LINKAPI))
            {
                MessageBox.Show($"API não configurada\nImpedindo Questionario para evitar erros\nPedir ajuda de um funcionario!");

                btnExposicoes.IsEnabled = true;
                btnMapa.IsEnabled = true;
                btnQuestionario.IsEnabled = true;
                return;
            }

            Questionario questionario = new Questionario();
            this.Hide();
            questionario.ShowDialog();

            imgPainel.Visibility = Visibility.Visible;
            lblMensagemQuestionario.Visibility = Visibility.Visible;

            this.Show();
            if (Estatico.MENSAGEMCOMUNICACAO == "ARMAZENAR")
            {
                Controle controle = new Controle();
                string json = controle.MontarJsonQuestionarioAvaliacao(Estatico.questionarioRespostas, Estatico.avaliacaoRespostas, Estatico.COMENTARIO);
                if(json != "")
                {
                    await controle.ArmazenarRespostas("/api/Master/QuestionarioAvaliacao", json);
                    //voltando para o MainFrame, abrir uma pagina nova de respostas
                    //Mostrar respostas certas (e a que o usuario errou)
                    if(Estatico.ERROMENSAGEM == "ERRO")
                    {
                        MessageBox.Show("Erro ao armazenar respostas\nVerificar conexão com a API\nPedir ajuda de um funcionario!");
                        imgPainel.Visibility = Visibility.Hidden;
                        lblMensagemQuestionario.Visibility = Visibility.Hidden;
                        btnExposicoes.IsEnabled = true;
                        btnMapa.IsEnabled = true;
                        btnQuestionario.IsEnabled = true;
                        return;
                    }
                    RelatorioRespostas relatorioRespostas = new RelatorioRespostas();
                    MainFrame.NavigationService.Navigate(relatorioRespostas);
                }

            }

            imgPainel.Visibility = Visibility.Hidden;
            lblMensagemQuestionario.Visibility = Visibility.Hidden;

            btnExposicoes.IsEnabled = true;
            btnMapa.IsEnabled = true;
            btnQuestionario.IsEnabled = true;

            Estatico.RELATORIOPAGINA = 1;

        }

        private void btnExposicoes_Click(object sender, RoutedEventArgs e)
        {            
            Exposicoes exposicoes = new Exposicoes();
            MainFrame.NavigationService.Navigate(exposicoes);
        }

        private void btnMapa_Click(object sender, RoutedEventArgs e)
        {
            Mapa mapa = new Mapa();
            MainFrame.NavigationService.Navigate(mapa);

        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.RightShift)
            {
                Configuracoes configuracores = new Configuracoes();
                MainFrame.NavigationService.Navigate(configuracores);
            }
        }
    }
}