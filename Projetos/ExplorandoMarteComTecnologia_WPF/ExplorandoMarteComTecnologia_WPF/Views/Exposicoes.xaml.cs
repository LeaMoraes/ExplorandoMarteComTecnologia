using ExplorandoMarteComTecnologia_WPF.Controllers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

using System.Windows.Threading;

namespace ExplorandoMarteComTecnologia_WPF.Views
{
    /// <summary>
    /// Interação lógica para Exposicoes.xam
    /// </summary>
    public partial class Exposicoes : Page
    {

        DispatcherTimer timer = new DispatcherTimer();
        public Exposicoes()
        {
            InitializeComponent();

            ConfigurarTimer();
        }

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
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainFrame.Content = null;
        }

        private void ResetarTimer()
        {
            timer.Stop(); // Para o timer
            timer.Start(); // Reinicia o timer, voltando a contar do zero
        }

        private void OnPageUnloaded(object sender, RoutedEventArgs e)
        {
            // Para o timer quando a página é descarregada
            timer.Stop();
        }


        private void Obra_Click(object sender, RoutedEventArgs e)
        {

            ResetarTimer();

            // Obter o indice da obra selecionada
            string obraIndexString = (string)((Button)sender).Tag;

            Controle controle = new Controle();
            int obraIndex = controle.ConverterStringParaInt(obraIndexString);

            // Navegar para a página de detalhes da obra
            Exposicao exposicao = new Exposicao(obraIndex);
            NavigationService.Navigate(exposicao);
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            // Acessar a janela principal
            var mainWindow = (MainWindow)Application.Current.MainWindow;

            // Limpar o conteúdo do Frame, removendo a Page
            mainWindow.MainFrame.Content = null;
        }
    }
}
