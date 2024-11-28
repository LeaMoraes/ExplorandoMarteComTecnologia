using ExplorandoMarteComTecnologia_WPF.Controllers;
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

namespace ExplorandoMarteComTecnologia_WPF.Views
{
    /// <summary>
    /// Interação lógica para Configuracoes.xam
    /// </summary>
    public partial class Configuracoes : Page
    {
        public Configuracoes()
        {
            InitializeComponent();
            txbApiLink.Text = Estatico.LINKAPI;
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;

            // Limpar o conteúdo do Frame, removendo a Page
            mainWindow.MainFrame.Content = null;
        }

        private void btnFecharSistema_Click(object sender, RoutedEventArgs e)
        {
            Window janela = Window.GetWindow(this);
            if (janela != null)
            {
                janela.Close();
            }
        }

        private void btnSalvarApi_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(txbApiLink.Text))
            {
                Estatico.LINKAPI = txbApiLink.Text;
                MessageBox.Show("API Salva");
                return;
            }
        }
    }
}
