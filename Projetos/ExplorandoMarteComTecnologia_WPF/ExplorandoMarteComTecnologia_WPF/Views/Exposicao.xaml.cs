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
    /// Interação lógica para Exposicao.xam
    /// </summary>
    public partial class Exposicao : Page
    {

        DispatcherTimer timer = new DispatcherTimer();
        public Exposicao(int obraIndex)
        {
            InitializeComponent();
            MudarExposicao(obraIndex);

            ConfigurarTimer();
        }

        private void ConfigurarTimer()
        {
            timer.Interval = TimeSpan.FromMinutes(1.30); // 1 minuto
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

        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            // Acessar a janela principal
            var mainWindow = (MainWindow)Application.Current.MainWindow;

            // Carregar a página Exposicoes no MainFrame
            mainWindow.MainFrame.Content = new Exposicoes();
        }

        private void MudarExposicao(int obraIndex)
        {
            switch (obraIndex)
            {
                case 0:
                    lblObraTitulo.Content = "O Planeta Vermelho";
                    lblDescricao.FontSize = 24;
                    lblDescricao.Content = "Marte é conhecido por sua característica\ncor vermelha, um traço distintivo que fascina\ncientistas e astrônomos.\nEssa tonalidade é resultado da presença de\nóxido de ferro em sua superfície, um produto\nda oxidação intensa do ferro presente na\nterra marciana.\nEm comparação com a Terra, a oxidação\nem Marte é muito mais acentuada, conferindo\nao planeta sua aparência única\ne deslumbrante.";
                    imgObra.Source = new BitmapImage(new Uri("/Imagens/Exposicoes/O_Planeta_Vermelho.jpg", UriKind.Relative));
                    break;

                case 1:
                    lblObraTitulo.Content = "Exploração e Potencial para Vida";
                    lblDescricao.FontSize = 24;
                    lblDescricao.Content = "A exploração de Marte é fundamental\npara desvendar os segredos do sistema\nsolar.\nEste planeta vermelho, com sua superfície\nárida e paisagens deslumbrantes,\né um alvo fascinante para cientistas\ne engenheiros.\nAtualmente, robôs pioneiros como o\nCuriosity Rover, Perseverance Rover,\nInSight Lander e ExoMars Rover\nestão explorando Marte, coletando dados\nsobre sua geologia, composição do solo,\nclima, atmosfera e potencial\npara água líquida.";
                    imgObra.Source = new BitmapImage(new Uri("/Imagens/Exposicoes/Exploracao_e_Potencial_para_Vida.png", UriKind.Relative));
                    break;

                case 2:
                    lblObraTitulo.Content = "Terreno Marciano";
                    lblDescricao.FontSize = 24;
                    lblDescricao.Content = "Marte apresenta uma paisagem\ndiversificada, marcada por vales profundos,\ncrateras gigantescas e vulcões imponentes.\nO mais notável deles é o Olympus Mons,\no maior vulcão do sistema solar, com uma\naltura impressionante de 27 km.\nA superfície de Marte é dividida em\nduas regiões principais:\na planície setentrional, caracterizada por\nsuas amplas extensões planas,\ne a região montanhosa meridional,\nrepleta de montanhas e vales.";
                    imgObra.Source = new BitmapImage(new Uri("/Imagens/Exposicoes/Terreno_Marciano.jpg", UriKind.Relative));
                    break;

                case 3:
                    lblObraTitulo.Content = "Agua em Marte";
                    lblDescricao.FontSize = 24;
                    lblDescricao.Content = "A possibilidade de vida em Marte é um tema\nintrigante e complexo.\nA água é essencial para a vida como\nconhecemos, e sua presença é um fator\ncrítico para o desenvolvimento de organismos\nvivos.\nA NASA e outras agências espaciais já\nencontraram evidências de que Marte teve\nágua líquida no passado, como rios e lagos\nsecos, deltas de rios e minerais hidratados.\nEssas descobertas sugerem que o planeta\npode ter tido condições favoráveis\nà vida no passado.";
                    imgObra.Source = new BitmapImage(new Uri("/Imagens/Exposicoes/Agua_em_Marte.jpg", UriKind.Relative));
                    break;

                case 4:
                    lblObraTitulo.Content = "Valles Marineris";
                    lblDescricao.FontSize = 24;
                    lblDescricao.Content = "Valles Marineris, o maior canyon do\nsistema solar, é um verdadeiro marco natural\nem Marte, estendendo-se por uma\nimpressionante distância de 4.000 km.\nPara colocar essa magnitude em perspectiva,\né quatro vezes mais longo do que o\nGrand Canyon, um dos mais icônicos\nmonumentos naturais da Terra.\nSua profundidade e vastidão são um\ntestemunho da força erosiva do vento e\nda água que, no passado, fluíram em Marte.";
                    imgObra.Source = new BitmapImage(new Uri("/Imagens/Exposicoes/Valles_Marineris.jpg", UriKind.Relative));
                    break;

                case 5:
                    lblObraTitulo.Content = "O Monte Olimpo";
                    lblDescricao.FontSize = 24;
                    lblDescricao.Content = "O Monte Olimpo, localizado em Marte,\né um verdadeiro gigante, com seus\nimpressionantes 27 km de altura, superando o\nMonte Everest em cerca de três vezes.\nExplorar esse vulcão seria um desafio\nemocionante e complexo.\nPara explorar o Monte Olimpo, seria\nnecessário desenvolver tecnologias\navançadas e estratégias inovadoras.\nEm primeiro lugar, missões robóticas seriam\nessenciais para mapear a superfície e coletar\ndados sobre a geologia e composição\ndo vulcão.\nIsso ajudaria a identificar áreas de\ninteresse e riscos potenciais.";
                    imgObra.Source = new BitmapImage(new Uri("/Imagens/Exposicoes/O_Monte_Olimpo.jpg", UriKind.Relative));
                    break;

                case 6:
                    lblObraTitulo.Content = "Impacto de Asteroides";
                    lblDescricao.FontSize = 24;
                    lblDescricao.Content = "Marte, o Planeta Vermelho, é um testemunho\nvivo da violência cósmica que\nmarca a história do sistema solar.\nSua superfície é pontuada por inúmeras\ncrateras de impacto, resultado da colisão\ncom asteroides e cometas ao longo de\nmilhões de anos, variando em tamanho desde\npequenas depressões até gigantescas\ncavidades como a Cratera Hellas, com mais\nde 2.200 km de diâmetro.\nEsses impactos fornecem informações\nvaliosas sobre a composição e estrutura\ngeológica de Marte, além de sugerir que a\nágua pode ter sido trazida por cometas\ne asteroides.";
                    imgObra.Source = new BitmapImage(new Uri("/Imagens/Exposicoes/Impacto_de_Asteroides.jpg", UriKind.Relative));
                    break;

                case 7:
                    lblObraTitulo.Content = "Colonização de Marte";
                    lblDescricao.FontSize = 24;
                    lblDescricao.Content = "A colonização de Marte é um objetivo\nambicioso que tem capturado a imaginação\nde científicos, engenheiros e visionários\npor décadas.\nA NASA e outras agências espaciais já têm\nplanos concretos para enviar missões\ntripuladas a Marte na década de 2030, com a\nSpaceX, fundada por Elon Musk,\ntrabalhando arduamente para desenvolver\ntecnologias necessárias para uma\ncolonização sustentável.\nCom a tecnologia avançando rapidamente,\na possibilidade de estabelecer uma\ncomunidade humana no Planeta Vermelho\nestá se tornando cada vez mais realista.\nNo entanto, a colonização de Marte\nnão será fácil.";
                    imgObra.Source = new BitmapImage(new Uri("/Imagens/Exposicoes/Colonizacao_de_Marte.jpg", UriKind.Relative));
                    break;

                default:
                    //Se uma obra não existente for selecionada, voltar para o menu
                    var mainWindow = (MainWindow)Application.Current.MainWindow;
                    mainWindow.MainFrame.Content = new Exposicoes();
                    break;

            }
        }
    }
}
