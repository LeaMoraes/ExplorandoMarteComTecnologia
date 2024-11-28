using ExplorandoMarteComTecnologia_WPF.Controllers;
using ExplorandoMarteComTecnologia_WPF.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ExplorandoMarteComTecnologia_WPF.Service
{
    internal class ControllService
    {

        private readonly HttpClient _httpClient;

        public ControllService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<QuestionarioRespostasDTO>> GetDataAsync(string endpoint)
        {

            if (String.IsNullOrEmpty(Estatico.LINKAPI))
            {
                MessageBox.Show($"API não configurada\nPedir ajuda de um funcionario!");
                return new List<QuestionarioRespostasDTO>();
            }

            try
            {
                var response = await _httpClient.GetAsync($"{Estatico.LINKAPI}{endpoint}");
                if (response.IsSuccessStatusCode)
                {
                    // Lê e desserializa o conteúdo JSON para uma lista de QuestionarioRespostasDTO
                    var content = await response.Content.ReadFromJsonAsync<List<QuestionarioRespostasDTO>>();

                    // Retorna a lista de QuestionarioRespostasDTO
                    return content ?? new List<QuestionarioRespostasDTO>(); // Retorna uma lista vazia se o conteúdo for nulo
                }
                else
                {
                    MessageBox.Show($"Erro: {response.StatusCode} - {response.ReasonPhrase}");
                    return new List<QuestionarioRespostasDTO>(); // Retorna uma lista vazia em caso de erro
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Erro ao obter dados: {e.Message}\nChame por um funcionario!");
                Estatico.ERROMENSAGEM = "ERRO";
                return new List<QuestionarioRespostasDTO>(); // Retorna uma lista vazia em caso de exceção
            }
        }

        public async Task<string> PutDataAsync(string endpoint, string jsonData)
        {
            // O jsonData já está em formato JSON, então usamos diretamente
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var mensagem = "";

            try
            {
                var response = await _httpClient.PostAsync($"{Estatico.LINKAPI}{endpoint}", content);
                if (response.IsSuccessStatusCode)
                {
                    mensagem = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    mensagem = $"Erro: {response.StatusCode} - {response.ReasonPhrase}";
                }
            }
            catch (Exception e)
            {
                Estatico.ERROMENSAGEM = "ERRO";
                return $"Erro ao atualizar dados: {e.Message}\nChame por um funcionario!";
            }

            return mensagem;
        }


    }
}
