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
    /// Interação lógica para Mapa.xam
    /// </summary>
    public partial class Mapa : Page
    {

        DispatcherTimer timer = new DispatcherTimer();
        public Mapa()
        {
            InitializeComponent();

            ConfigurarTimer();
        }

        private void ResetarTimer()
        {
            timer.Stop(); // Para o timer
            timer.Start(); // Reinicia o timer, voltando a contar do zero
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


        private void OnPageUnloaded(object sender, RoutedEventArgs e)
        {
            // Para o timer quando a página é descarregada
            timer.Stop();
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
