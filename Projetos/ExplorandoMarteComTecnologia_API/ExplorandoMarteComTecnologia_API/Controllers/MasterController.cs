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
        public async Task<ActionResult<NomeKeyDTO>> NovaVenda([FromBody] NovaVendaDTO novavendaDTO)
        {
            using( var transaction = _dbcontext.Database.BeginTransaction())
            {
                //Verificar se os dados foram enviados corretamente
                Validacao validacao = new Validacao();
                if (!validacao.VerificarCampos(novavendaDTO))
                {
                    await transaction.RollbackAsync();
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
                List<NomeKeyDTO> nomeKeyVisual = new List<NomeKeyDTO>();

                try
                {
                    ingressoComKey = keyGenerator.AtribuirKey(ingressoDTO, venda.Email);
                    foreach(var ingresso in ingressoComKey)
                    {
                        nomeKeyVisual.Add(new NomeKeyDTO { Nome = ingresso.Nome, Key = ingresso.Key });
                    }
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
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
                    await transaction.RollbackAsync();
                    return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao processar a venda: {ex.Message}");
                }

                try
                {

                    await _dbcontext.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return Ok(nomeKeyVisual);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao criar a venda: {ex.Message}");
                }
            }
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
                return BadRequest(CriarRespostaErro());
            }

            EmailKeyDTO emailKeyDTO = new EmailKeyDTO
            {
                Email = validacao.SepararEmailKey(emailKey).email,
                Key = validacao.SepararEmailKey(emailKey).key
            };

            //Valida o Email e a Key que o usuario enviou
            if (!validacao.ValidarEmailKey(emailKeyDTO))
            {
                return BadRequest(CriarRespostaErro());
            }
            string email = validacao.ConverterParaMinusculas(emailKeyDTO.Email);
            string key = validacao.ConverterParaMaiuscula(emailKeyDTO.Key);

            try
            {
                //Procura o Id da venda usando a Key que o usuario colocou e verifica se esta nula
                var idVenda = await ingressosVendasController.PegarIdEmail(key);
                if (string.IsNullOrWhiteSpace(idVenda.ToString()))
                {
                    return BadRequest(CriarRespostaErro());
                }

                //Procura a venda usando o Id da venda e armazena em uma variavel com o modelo VendaModel
                //E valida se existe o Email
                VendaModel venda = await vendaController.PegarVenda(idVenda.Value);

                if (venda == null || venda.Email == null)
                {
                    return BadRequest(CriarRespostaErro());
                }


                if (string.IsNullOrWhiteSpace(venda.Email))
                {
                    return BadRequest(CriarRespostaErro());
                }
                

                //Compara se o email que o usuario escreveu é igual ao da venda
                if (venda.Email != email)
                {
                    return BadRequest(CriarRespostaErro());
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
                return BadRequest(CriarRespostaErro($"Erro ao realizar login: {ex.Message}"));
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

        [HttpGet("relatorioIngressos")]
        public async Task<ActionResult<IEnumerable<RelatorioIngressosModel>>> PegarRelatoriosIngressos()
        {
            RelatorioIngressosController relatorioIngressosController = new RelatorioIngressosController(_dbcontext);
            return await relatorioIngressosController.PegarRelatorios();
        }

        [HttpGet("relatorioVendas")]
        public async Task<ActionResult<IEnumerable<RelatorioVendasModel>>> PegarRelatoriosVenda()
        {
            RelatorioVendasController relatorioVendaController = new RelatorioVendasController(_dbcontext);
            return await relatorioVendaController.PegarRelatorios();
        }

        [HttpGet("relatorioIngressos/{anoMesDia}")]
        public async Task<ActionResult<RelatorioIngressosModel>> PegarRelatorioIngressos(string anoMesDia)
        {
            RelatorioIngressosController relatorioIngressosController = new RelatorioIngressosController(_dbcontext);
            return await relatorioIngressosController.PegarRelatorio(anoMesDia);
        }

        [HttpGet("relatorioVendas/{anoMesDia}")]
        public async Task<ActionResult<RelatorioVendasModel>> PegarRelatorioVenda(string anoMesDia)
        {
            RelatorioVendasController relatorioVendaController = new RelatorioVendasController(_dbcontext);
            return await relatorioVendaController.PegarRelatorio(anoMesDia);
        }

        [HttpPost("QuestionarioAvaliacao")]
        public async Task<ActionResult> ArmazenarRespostas(QuestionarioAvaliacaoRespostasDTO questionarioAvaliacaoRespostasDto)
        {
            List<QuestionarioRespostasModel> questionarioRespostas = questionarioAvaliacaoRespostasDto.questionarioRespostas;
            List<AvaliacaoRespostasModel> avaliacaoRespostas = questionarioAvaliacaoRespostasDto.avaliacaoRespostas;

            QuestionarioRespostasController questionarioRespostasController = new QuestionarioRespostasController(_dbcontext);
            AvaliacaoRespostasController avaliacaoRespostasController = new AvaliacaoRespostasController(_dbcontext);
            AvaliacaoSugestaoController avaliacaoSugestaoController = new AvaliacaoSugestaoController(_dbcontext);
            using(var transaction = _dbcontext.Database.BeginTransaction())
            {

                try
                {

                    if (questionarioAvaliacaoRespostasDto.avaliacaoSugestao != null)
                    {
                        await avaliacaoSugestaoController.ArmazenarSugestao(questionarioAvaliacaoRespostasDto.avaliacaoSugestao);
                    }

                    foreach (var respostas in questionarioRespostas)
                    {
                        await questionarioRespostasController.AtualizarRespostas(respostas);
                    }

                    foreach (var respostas in avaliacaoRespostas)
                    {
                        await avaliacaoRespostasController.AtualizarRespostas(respostas);
                    }

                    await _dbcontext.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return Ok(new { message = "Respostas armazenadas com sucesso!" });
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao salvar respostas: {ex.Message}");
                }
            }             
        }

        [HttpGet("QuestionarioAvaliacao/q")]
        public async Task<ActionResult<IEnumerable<QuestionarioRespostasModel>>> PegarRespostasPerguntasQuestionario()
        {
            QuestionarioRespostasController questionarioRespostasController = new QuestionarioRespostasController(_dbcontext);
            return await questionarioRespostasController.PegarTodasPerguntas();
        }

        [HttpGet("QuestionarioAvaliacao/q/{id}")]
        public async Task<ActionResult<QuestionarioRespostasModel>> PegarRespostasPerguntaQuestionario(int id)
        {
            QuestionarioRespostasController questionarioRespostasController = new QuestionarioRespostasController(_dbcontext);
            return await questionarioRespostasController.PegarPergunta(id);
        }

        [HttpGet("QuestionarioAvaliacao/a")]
        public async Task<ActionResult<IEnumerable<AvaliacaoRespostasModel>>> PegarRespostasPerguntasAvaliacao()
        {
            AvaliacaoRespostasController avaliacaoRespostasController = new AvaliacaoRespostasController(_dbcontext);
            return await avaliacaoRespostasController.PegarTodasPerguntas();
        }

        [HttpGet("QuestionarioAvaliacao/a/{id}")]
        public async Task<ActionResult<AvaliacaoRespostasModel>> PegarRespostasPerguntaAvaliacao(int id)
        {
            AvaliacaoRespostasController avaliacaoRespostasController = new AvaliacaoRespostasController(_dbcontext);
            return await avaliacaoRespostasController.PegarPergunta(id);
        }

        [HttpGet("QuestionarioAvaliacao/s")]
        public async Task<ActionResult<IEnumerable<AvaliacaoSugestaoModel>>> PegarSugestoes()
        {
            AvaliacaoSugestaoController avaliacaoSugestaoController = new AvaliacaoSugestaoController(_dbcontext);
            return await avaliacaoSugestaoController.PegarTodasSugestoes();
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
