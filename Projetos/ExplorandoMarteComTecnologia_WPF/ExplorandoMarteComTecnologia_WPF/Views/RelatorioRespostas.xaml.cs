﻿using ExplorandoMarteComTecnologia_WPF.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interação lógica para RelatorioRespostas.xam
    /// </summary>
    public partial class RelatorioRespostas : Page
    {
        private List<Label> lblPerguntaNumeracao;
        DispatcherTimer timer = new DispatcherTimer();
        

        public RelatorioRespostas()
        {
            InitializeComponent();

            lblRespostaSelecionada1.Content = Estatico.TEMPRESPOSTAQUEST1;
            lblRespostaSelecionada2.Content = Estatico.TEMPRESPOSTAQUEST2;
            lblRespostaSelecionada3.Content = Estatico.TEMPRESPOSTAQUEST3;
            lblRespostaSelecionada4.Content = Estatico.TEMPRESPOSTAQUEST4;
            lblRespostaSelecionada5.Content = Estatico.TEMPRESPOSTAQUEST5;

            Estatico.RELATORIOPAGINA = 1;

            Window janela = Window.GetWindow(this);
            Controle controle = new Controle();
            controle.AtualizarValoresAcertosErrosPerguntas("/api/Master/QuestionarioAvaliacao/q");
            ProximaPagina(Estatico.RELATORIOPAGINA);
            ConfigurarTimer();


        }

        string questao1 = "Acertou";
        string questao2 = "Acertou";
        string questao3 = "Acertou";
        string questao4 = "Acertou";
        string questao5 = "Acertou";

        private bool isTicking = false;


        private void ResetarTimer()
        {
            timer.Stop(); // Para o timer
            timer.Start(); // Reinicia o timer, voltando a contar do zero
        }

        private void ConfigurarTimer()
        {
            Estatico.RELATORIOPAGINA = 1;
            timer.Interval = TimeSpan.FromMinutes(1); // 1 minuto
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Se o timer já estiver "rodando", não faça nada
            if (isTicking)
                return;

            // Marca o início do tick
            isTicking = true;

            // Exibe o pop-up de debug

            // A lógica para incrementar a página e chamar a próxima
            Estatico.RELATORIOPAGINA += 1;

            // Chama a função para exibir a próxima página
            ProximaPagina(Estatico.RELATORIOPAGINA);

            // Se a página atingir 3, interrompe o timer
            if (Estatico.RELATORIOPAGINA >= 4)
            {
                timer.Stop();
            }

            // Marca o fim do tick
            isTicking = false;
        }

        private void OnPageUnloaded(object sender, RoutedEventArgs e)
        {
            // Para o timer quando a página é descarregada
            timer.Stop();
        }

        private void ChecarRespostas()
        {
            // Comparar o conteúdo como string
            if (lblRespostaSelecionada1.Content.ToString() != lblRespostaCerta1.Content.ToString())
            {
                questao1 = "Errou";
                lblRespostaSelecionada1.Foreground = new SolidColorBrush(Colors.Red);
                lblRespostaCerta1.Visibility = Visibility.Visible;
                imgRC1.Visibility = Visibility.Visible;
                imgStatusPergunta1.Source = new BitmapImage(new Uri("/Imagens/Relatorio/Errado.png", UriKind.Relative));
            }

            if (lblRespostaSelecionada2.Content.ToString() != lblRespostaCerta2.Content.ToString())
            {
                questao2 = "Errou";
                lblRespostaSelecionada2.Foreground = new SolidColorBrush(Colors.Red);
                lblRespostaCerta2.Visibility = Visibility.Visible;
                imgRC2.Visibility = Visibility.Visible;
                imgStatusPergunta2.Source = new BitmapImage(new Uri("/Imagens/Relatorio/Errado.png", UriKind.Relative));
            }

            if (lblRespostaSelecionada3.Content.ToString() != lblRespostaCerta3.Content.ToString())
            {
                questao3 = "Errou";
                lblRespostaSelecionada3.Foreground = new SolidColorBrush(Colors.Red);
                lblRespostaCerta3.Visibility = Visibility.Visible;
                imgRC3.Visibility = Visibility.Visible;
                imgStatusPergunta3.Source = new BitmapImage(new Uri("/Imagens/Relatorio/Errado.png", UriKind.Relative));  
            }

            if (lblRespostaSelecionada4.Content.ToString() != lblRespostaCerta4.Content.ToString())
            {
                questao4 = "Errou";
                lblRespostaSelecionada4.Foreground = new SolidColorBrush(Colors.Red);
                lblRespostaCerta4.Visibility = Visibility.Visible;
                imgRC4.Visibility = Visibility.Visible;
                imgStatusPergunta4.Source = new BitmapImage(new Uri("/Imagens/Relatorio/Errado.png", UriKind.Relative));
            }

            if (lblRespostaSelecionada5.Content.ToString() != lblRespostaCerta5.Content.ToString())
            {
                questao5 = "Errou";
                lblRespostaSelecionada5.Foreground = new SolidColorBrush(Colors.Red);
                lblRespostaCerta5.Visibility = Visibility.Visible;
                imgRC5.Visibility = Visibility.Visible;
                imgStatusPergunta5.Source = new BitmapImage(new Uri("/Imagens/Relatorio/Errado.png", UriKind.Relative));
            }
        }

        public void AtualizarEstatisticaGlobal()
        {
            Random random = new Random();

            // Lista de perguntas com acertos e erros
            string[] perguntas = { questao1, questao2, questao3, questao4, questao5};
            int[] acertos = { Estatico.QUANTIDADEACERTOSQUEST1, Estatico.QUANTIDADEACERTOSQUEST2, Estatico.QUANTIDADEACERTOSQUEST3, Estatico.QUANTIDADEACERTOSQUEST4, Estatico.QUANTIDADEACERTOSQUEST5 };
            int[] erros = { Estatico.QUANTIDADEERROSQUEST1, Estatico.QUANTIDADEERROSQUEST2, Estatico.QUANTIDADEERROSQUEST3, Estatico.QUANTIDADEERROSQUEST4, Estatico.QUANTIDADEERROSQUEST5 };
            int[] totais = { Estatico.QUANTIDADEACERTOSQUEST1 + Estatico.QUANTIDADEERROSQUEST1, Estatico.QUANTIDADEACERTOSQUEST2 + Estatico.QUANTIDADEERROSQUEST2, Estatico.QUANTIDADEACERTOSQUEST3 + Estatico.QUANTIDADEERROSQUEST3, Estatico.QUANTIDADEACERTOSQUEST4 + Estatico.QUANTIDADEERROSQUEST4, Estatico.QUANTIDADEACERTOSQUEST5 + Estatico.QUANTIDADEERROSQUEST5 };

            
            for (int i = 0; i < perguntas.Length; i++)
            {
                if (perguntas[i] == "Acertou")
                {
                    int fraseIndex = random.Next(1, 4);
                    double porcentagemAcerto = Math.Round((double)acertos[i] / totais[i] * 100, 1);

                    switch (fraseIndex)
                    {
                        case 1:
                            string fraseA1 = $"{acertos[i]} Pessoa(s) acertaram esta questão, e você é uma delas!";
                            InserirFrase(fraseA1, i);
                            break;
                        case 2:
                            string fraseA2 = $"Você está entre {acertos[i]} das pessoas que acertaram esta pergunta!";
                            InserirFrase(fraseA2, i);
                            break;
                        case 3:
                            string fraseA3 = $"Você está entre os {porcentagemAcerto}% que acertaram esta pergunta!";
                            InserirFrase(fraseA3, i);
                            break;
                        default:
                            //MessageBox.Show("Sem frase");
                            break;
                    }
                }
                else if (perguntas[i] == "Errou")
                {
                    int fraseIndex = random.Next(1, 4);
                    double porcentagemErro = Math.Round((double)erros[i] / totais[i] * 100, 1);

                    switch (fraseIndex)
                    {
                        case 1:
                            string fraseE1 = $"{erros[i]} Pessoa(s) erraram esta questão!";
                            InserirFrase(fraseE1, i);
                            break;
                        case 2:
                            string fraseE2 = $"Não se preocupe, {erros[i]} pessoa(s) também erraram!";
                            InserirFrase(fraseE2, i);
                            break;
                        case 3:
                            string fraseE3 = $"{porcentagemErro}% das pessoas erraram esta pergunta!";
                            InserirFrase(fraseE3, i);
                            break;
                        default:
                            //MessageBox.Show("Sem frase");
                            break;
                    }
                }
            }
        }

        private void InserirFrase(string frase, int lblIndex)
        {
            switch (lblIndex)
            {
                case 0:
                    lblPessoasTotalQuestao1.Content = frase;
                    break;
                case 1:
                    lblPessoasTotalQuestao2.Content = frase;
                    break;
                case 2:
                    lblPessoasTotalQuestao3.Content = frase;
                    break;
                case 3:
                    lblPessoasTotalQuestao4.Content = frase;
                    break;
                case 4:
                    lblPessoasTotalQuestao5.Content = frase;
                    break;
                
                default:
                    break;
            }
        }

        public void ProximaPagina(int pagina)
        {
            switch (pagina)
            {
                case 1:

                    ConfigurarTimer();
                    ResetarTimer();
                    ChecarRespostas();
                    lblSuasRespostas.Visibility = Visibility.Visible;

                    lblPerguntaNumeracao1.Visibility = Visibility.Visible;
                    lblPerguntaNumeracao2.Visibility = Visibility.Visible;
                    lblPerguntaNumeracao3.Visibility = Visibility.Visible;
                    lblPerguntaNumeracao4.Visibility = Visibility.Visible;
                    lblPerguntaNumeracao5.Visibility = Visibility.Visible;

                    // Exibindo as respostas selecionadas
                    lblRespostaSelecionada1.Visibility = Visibility.Visible;
                    lblRespostaSelecionada2.Visibility = Visibility.Visible;
                    lblRespostaSelecionada3.Visibility = Visibility.Visible;
                    lblRespostaSelecionada4.Visibility = Visibility.Visible;
                    lblRespostaSelecionada5.Visibility = Visibility.Visible;

                    lblEstatisticas.Visibility = Visibility.Hidden;

                    // Ocultando o total de pessoas por questão
                    lblPessoasTotalQuestao1.Visibility = Visibility.Hidden;
                    lblPessoasTotalQuestao2.Visibility = Visibility.Hidden;
                    lblPessoasTotalQuestao3.Visibility = Visibility.Hidden;
                    lblPessoasTotalQuestao4.Visibility = Visibility.Hidden;
                    lblPessoasTotalQuestao5.Visibility = Visibility.Hidden;

                    imgAgradecimento.Visibility = Visibility.Hidden;

                    break;

                case 2:

                    AtualizarEstatisticaGlobal();

                    lblSuasRespostas.Visibility = Visibility.Hidden;

                    lblPerguntaNumeracao1.Visibility = Visibility.Visible;
                    lblPerguntaNumeracao2.Visibility = Visibility.Visible;
                    lblPerguntaNumeracao3.Visibility = Visibility.Visible;
                    lblPerguntaNumeracao4.Visibility = Visibility.Visible;
                    lblPerguntaNumeracao5.Visibility = Visibility.Visible;

                    // Reposicionando a numeração das perguntas
                    lblPerguntaNumeracao1.Margin = new Thickness(12, 84, 0, 0);
                    lblPerguntaNumeracao2.Margin = new Thickness(12, 156, 0, 0);
                    lblPerguntaNumeracao3.Margin = new Thickness(12, 229, 0, 0);
                    lblPerguntaNumeracao4.Margin = new Thickness(12, 301, 0, 0);
                    lblPerguntaNumeracao5.Margin = new Thickness(12, 377, 0, 0);

                    // Reposicionando o total de pessoas por questão
                    lblPessoasTotalQuestao1.Margin = new Thickness(42, 84, 0, 0);
                    lblPessoasTotalQuestao2.Margin = new Thickness(42, 156, 0, 0);
                    lblPessoasTotalQuestao3.Margin = new Thickness(42, 229, 0, 0);
                    lblPessoasTotalQuestao4.Margin = new Thickness(42, 301, 0, 0);
                    lblPessoasTotalQuestao5.Margin = new Thickness(42, 377, 0, 0);

                    // Ocultando as respostas selecionadas
                    lblRespostaSelecionada1.Visibility = Visibility.Hidden;
                    lblRespostaSelecionada2.Visibility = Visibility.Hidden;
                    lblRespostaSelecionada3.Visibility = Visibility.Hidden;
                    lblRespostaSelecionada4.Visibility = Visibility.Hidden;
                    lblRespostaSelecionada5.Visibility = Visibility.Hidden;

                    // Ocultando as respostas corretas
                    lblRespostaCerta1.Visibility = Visibility.Hidden;
                    lblRespostaCerta2.Visibility = Visibility.Hidden;
                    lblRespostaCerta3.Visibility = Visibility.Hidden;
                    lblRespostaCerta4.Visibility = Visibility.Hidden;
                    lblRespostaCerta5.Visibility = Visibility.Hidden;

                    imgRC1.Visibility = Visibility.Hidden;
                    imgRC2.Visibility = Visibility.Hidden;
                    imgRC3.Visibility = Visibility.Hidden;
                    imgRC4.Visibility = Visibility.Hidden;
                    imgRC5.Visibility = Visibility.Hidden;

                    imgStatusPergunta1.Visibility = Visibility.Hidden;
                    imgStatusPergunta2.Visibility = Visibility.Hidden;
                    imgStatusPergunta3.Visibility = Visibility.Hidden;
                    imgStatusPergunta4.Visibility = Visibility.Hidden;
                    imgStatusPergunta5.Visibility = Visibility.Hidden;

                    lblEstatisticas.Visibility = Visibility.Visible;
                    lblEstatisticas.Margin = new Thickness(12, 9, 0, 0);

                    // Exibindo o total de pessoas por questão
                    lblPessoasTotalQuestao1.Visibility = Visibility.Visible;
                    lblPessoasTotalQuestao2.Visibility = Visibility.Visible;
                    lblPessoasTotalQuestao3.Visibility = Visibility.Visible;
                    lblPessoasTotalQuestao4.Visibility = Visibility.Visible;
                    lblPessoasTotalQuestao5.Visibility = Visibility.Visible;

                    imgAgradecimento.Visibility = Visibility.Hidden;



                    break;

                case 3:
                    imgAgradecimento.Visibility = Visibility.Visible;
                    break;

                default:
                    timer.Stop();

                    // Reiniciar a variável de controle
                    Estatico.RELATORIOPAGINA = 1;

                    // Acessar a janela principal e limpar o conteúdo do Frame, removendo a página
                    var mainWindow = (MainWindow)Application.Current.MainWindow;
                    mainWindow.MainFrame.Content = null;

                    // Forçar a coleta de lixo para liberar recursos da página
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    break;
            }
        }

        private void pcbContinuar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Estatico.RELATORIOPAGINA++;
            ProximaPagina(Estatico.RELATORIOPAGINA);
        }

        private void imgAgradecimento_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Estatico.RELATORIOPAGINA++;
            ProximaPagina(Estatico.RELATORIOPAGINA);
        }
    }
}
