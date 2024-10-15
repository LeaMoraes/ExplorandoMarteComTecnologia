﻿using ExplorandoMarteComTecnologia_API.Data;
using ExplorandoMarteComTecnologia_API.DTO;
using ExplorandoMarteComTecnologia_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;

namespace ExplorandoMarteComTecnologia_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {

        private readonly ExplorarMarteDBContext _dbcontext;

        public MasterController(ExplorarMarteDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpPost("novavenda")]
        public async Task<ActionResult> NovaVenda([FromBody] NovaVendaDTO novavendaDTO)
        {
            //Verificar se os dados foram enviados corretamente
            Validacao validacao = new Validacao();
            if (!validacao.VerificarCampos(novavendaDTO))
            {
                return BadRequest("Os dados enviados estão incorretos.");
            }            

            //Separa os modelos do DTO em variaveis separadas
            List<IngressoDTO> ingressoDTO = novavendaDTO.ingressoDTO;
            VendaModel venda = novavendaDTO.venda;

            //Deixa o email com caracteres minusculos
            venda.Email = validacao.ConverterParaMinusculas(venda.Email);

            //Atribui chaves para todos os ingressos
            KeyGenerator keyGenerator = new KeyGenerator();
            List<IngressoDTO> ingressoComKey;

            try
            {
                ingressoComKey = keyGenerator.AtribuirKey(ingressoDTO, venda.Email);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atribuir chave aos ingressos: {ex.Message}");
            }
            

           //Instancia dos Controllers para criar a venda e os ingressos
            
            VendaController vendaController = new VendaController(_dbcontext);
            IngressosVendasController ingressoVendasController = new IngressosVendasController(_dbcontext);
            IngressoController ingressoController = new IngressoController(_dbcontext);
            RelatorioIngressosController relatorioIngressosController = new RelatorioIngressosController(_dbcontext);
            RelatorioVendasController relatorioVendasController = new RelatorioVendasController(_dbcontext);

            //Calcula o preço de cada ingresso e cria a venda
            try 
            {
                var precoIngressos = validacao.CalcularPreco(ingressoComKey);
                venda.Preco = precoIngressos.preco;
                var vendaId = await vendaController.CriarVenda(venda);

                //Verificação do metodo de pagamento para relatorioVendas
                int quantidademetodoCredito = 0;
                int quantidademedotoDebito = 0;
                int quantidademetodoPix = 0;
                switch (venda.MetodoPagamento)
                {
                    case "Credito":
                        quantidademetodoCredito = 1;
                        break;

                    case "Debito":
                        quantidademedotoDebito = 1;
                        break;

                    case "Pix":
                        quantidademetodoPix = 1;
                        break;
                }

                //Criação do relatorioVendas
                var relatorioVendas = new RelatorioVendasDTO
                {
                    RelatorioData = DateOnly.FromDateTime(DateTime.Now).ToString(),
                    DinheiroTotal = precoIngressos.preco,
                    QuantidadePagamentoCredito = quantidademetodoCredito,
                    QuantidadePagamentoDebito = quantidademedotoDebito,
                    QuantidadePagamentoPix = quantidademetodoPix
                };

                await relatorioVendasController.AtualizarRelatorio(relatorioVendas);

                //Criação do relatorioIngressos
                var relatorioIngressos = new RelatorioIngressosDTO
                {
                    RelatorioData = DateOnly.FromDateTime(DateTime.Now).ToString(),
                    TotalIngressosVendidos = precoIngressos.TotalIngressosInteiro + precoIngressos.TotalIngressosMeia + precoIngressos.TotalIngressosIsento,
                    TotalIngressosInteiro = precoIngressos.TotalIngressosInteiro,
                    TotalIngressosMeia = precoIngressos.TotalIngressosMeia,
                    TotalIngressosIsentos = precoIngressos.TotalIngressosIsento
                };
                await relatorioIngressosController.AtualizarRelatorio(relatorioIngressos);

                //Associa os ingressos com o Id da venda
                foreach (var ingressoAssociar in ingressoComKey)
                {
                    if (!string.IsNullOrEmpty(ingressoAssociar.Key) && ingressoAssociar.Key.Length == 9)
                    {
                        await ingressoVendasController.CriarAssociacaoIdKey(vendaId.Value, ingressoAssociar.Key);
                    }
                    else
                    {
                        return BadRequest("Um ou mais ingressos têm a Key inválida.");
                    }
                }

                //Cria os ingressos e salva no banco de dados
                await ingressoController.CriarIngresso(ingressoComKey);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao processar a venda: {ex.Message}");
            }

            try
            {

                await _dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao criar a venda: {ex.Message}");
            }

            return Ok();
        }

        [HttpGet("login/{emailKey}")]
        public async Task<ActionResult<LoginRespostaDTO>> RealizarLogin(string emailKey)
        {

            //Chamadas das instancias
            IngressosVendasController ingressosVendasController = new IngressosVendasController(_dbcontext);
            VendaController vendaController = new VendaController(_dbcontext);
            Validacao validacao = new Validacao();

            if (string.IsNullOrEmpty(emailKey) || emailKey.Length <= 9)
            {
                return CriarRespostaErro();
            }

            EmailKeyDTO emailKeyDTO = new EmailKeyDTO
            {
                Email = validacao.SepararEmailKey(emailKey).email,
                Key = validacao.SepararEmailKey(emailKey).key
            };

            //Valida o Email e a Key que o usuario enviou
            if (!validacao.ValidarEmailKey(emailKeyDTO))
            {
                return CriarRespostaErro();
            }
            string email = validacao.ConverterParaMinusculas(emailKeyDTO.Email);
            string key = validacao.ConverterParaMaiuscula(emailKeyDTO.Key);

            try
            {
                //Procura o Id da venda usando a Key que o usuario colocou e verifica se esta nula
                var idVenda = await ingressosVendasController.PegarIdEmail(key);
                if (string.IsNullOrWhiteSpace(idVenda.ToString()))
                {
                    return CriarRespostaErro();
                }

                //Procura a venda usando o Id da venda e armazena em uma variavel com o modelo VendaModel
                //E valida se existe o Email
                VendaModel venda = await vendaController.PegarVenda(idVenda.Value);

                if (venda == null || venda.Email == null)
                {
                    return CriarRespostaErro();
                }


                if (string.IsNullOrWhiteSpace(venda.Email))
                {
                    return CriarRespostaErro();
                }
                

                //Compara se o email que o usuario escreveu é igual ao da venda
                if (venda.Email != email)
                {                    
                    return CriarRespostaErro();
                }

                //Cria um LoginResposta com o Success true
                LoginRespostaDTO loginResposta = new LoginRespostaDTO
                {
                    Success = true,
                    Email = venda.Email
                };

                return Ok(loginResposta);
            }
            catch (Exception ex)
            {
                return CriarRespostaErro($"Erro ao realizar login: {ex.Message}");
            }


        }

        [HttpGet("vendas")]
        public async Task<ActionResult<IEnumerable<VendaModel>>> PegarVendas()
        {
            VendaController vendaController = new VendaController(_dbcontext);
            return await vendaController.PegarVendas();
        }

        [HttpGet("vendas/{id}")]
        public async Task<ActionResult<VendaModel>> PegarVenda(int id)
        {
            VendaController vendaController = new VendaController(_dbcontext);
            return await vendaController.PegarVenda(id);
        }

        [HttpGet("ingressos")]
        public async Task<ActionResult<IEnumerable<IngressoModel>>> PegarIngressos()
        {
            IngressoController ingressoController = new IngressoController(_dbcontext);
            return await ingressoController.PegarIngressos();
        }

        [HttpGet("ingressos/{key}")]
        public async Task<ActionResult<IngressoModel>> PegarIngresso(string key)
        {
            if (key.Length != 9)
            {
                return BadRequest("A Key do ingresso deve ter exatamente 9 caracteres e não pode ser nula!");
            }

            IngressoController ingressoController = new IngressoController(_dbcontext);
            return await ingressoController.PegarIngresso(key);
        }

        private LoginRespostaDTO CriarRespostaErro()
        {
            return new LoginRespostaDTO
            {
                Success = false,
                ErrorMessage = "Email ou Key inválidos!"
            };
        }

        private LoginRespostaDTO CriarRespostaErro(string erro)
        {
            return new LoginRespostaDTO
            {
                Success = false,
                ErrorMessage = erro
            };
        }

    }
}