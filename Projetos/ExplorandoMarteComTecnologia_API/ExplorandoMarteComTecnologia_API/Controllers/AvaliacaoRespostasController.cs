using ExplorandoMarteComTecnologia_API.Data;
using ExplorandoMarteComTecnologia_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExplorandoMarteComTecnologia_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvaliacaoRespostasController : ControllerBase
    {
        private readonly ExplorarMarteDBContext _dbcontext;

        public AvaliacaoRespostasController(ExplorarMarteDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AvaliacaoRespostasModel>>> PegarTodasPerguntas()
        {
            var perguntas = await _dbcontext.AvaliacaoRespostas.ToListAsync();
            return Ok(perguntas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AvaliacaoRespostasModel>> PegarPergunta(int id)
        {
            var pergunta = await _dbcontext.AvaliacaoRespostas.SingleOrDefaultAsync(p => p.Id == id);

            if (pergunta == null)
            {
                return NotFound();
            }

            return Ok(pergunta);
        }

        [HttpPost]
        public async Task<ActionResult<AvaliacaoRespostasModel>> CriarPergunta(AvaliacaoRespostasModel avaliacaoRespostas)
        {

            Validacao validacao = new Validacao();

            //Garantia que na criação de pergunta, os valores iniciam com 0
            avaliacaoRespostas.Excelente = 0;
            avaliacaoRespostas.Bom = 0;
            avaliacaoRespostas.Regular = 0;
            avaliacaoRespostas.Ruim = 0;
            avaliacaoRespostas.Pessimo = 0;

            try
            {
                List<AvaliacaoRespostasModel> perguntas = await _dbcontext.AvaliacaoRespostas.ToListAsync();
                bool existe = validacao.PerguntaExiste(perguntas, avaliacaoRespostas.Pergunta);
                if (existe == true)
                {
                    return BadRequest("A pergunta já existe no banco de dados.");
                }

                _dbcontext.AvaliacaoRespostas.Add(avaliacaoRespostas);
                await _dbcontext.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro no banco de dados: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao criar pergunta: {ex.Message}");
            }

        }

        [HttpPut]
        public async Task<ActionResult> AtualizarRespostas(AvaliacaoRespostasModel avaliacaoRespostas)
        {
            await VerificarEAdicionarPerguntas();

            Validacao validacao = new Validacao();

            var pergunta = await _dbcontext.AvaliacaoRespostas.FirstOrDefaultAsync(p => p.Id == avaliacaoRespostas.Id);

            if (pergunta == null)
            {
                return NotFound("Pergunta não encontrada!");
            }

            List<AvaliacaoRespostasModel> perguntas = await _dbcontext.AvaliacaoRespostas.ToListAsync();
            bool perguntaIdIgual = validacao.PerguntaIdIgual(perguntas, avaliacaoRespostas.Id, avaliacaoRespostas.Pergunta);

            if (perguntaIdIgual == false)
            {
                return NotFound("O ID não bate com a Pergunta no banco de dados.");
            }

            pergunta.Excelente += avaliacaoRespostas.Excelente;
            pergunta.Bom += avaliacaoRespostas.Bom;
            pergunta.Regular += avaliacaoRespostas.Regular;
            pergunta.Ruim += avaliacaoRespostas.Ruim;
            pergunta.Pessimo += avaliacaoRespostas.Pessimo;

            _dbcontext.Entry(pergunta).State = EntityState.Modified;

            try
            {
                await _dbcontext.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

        }


        private async Task VerificarEAdicionarPerguntas()
        {
            var perguntasNecessarias = new List<string>
            {
                "QualidadeExposicoes",
                "InteracaoTotemSite",
                "InformacoesClaras"
            };

            var perguntasExistentes = await _dbcontext.AvaliacaoRespostas.ToListAsync();

            // Verificar se as perguntas existem e estão na ordem correta
            for (int i = 0; i < perguntasNecessarias.Count; i++)
            {
                var pergunta = perguntasExistentes.FirstOrDefault(p => p.Pergunta == perguntasNecessarias[i]);

                // Se a pergunta não existir, cria ela
                if (pergunta == null)
                {
                    AvaliacaoRespostasModel novaPergunta = new AvaliacaoRespostasModel
                    {
                        Pergunta = perguntasNecessarias[i],
                        Excelente = 0,
                        Bom = 0,
                        Regular = 0,
                        Ruim = 0,
                        Pessimo = 0
                    };
                    _dbcontext.AvaliacaoRespostas.Add(novaPergunta);
                }
                // Caso a pergunta exista mas a ordem esteja errada, remova e insira novamente
                else
                {
                    if (pergunta.Pergunta != perguntasNecessarias[i])
                    {
                        _dbcontext.AvaliacaoRespostas.Remove(pergunta);
                        AvaliacaoRespostasModel novaPergunta = new AvaliacaoRespostasModel
                        {
                            Pergunta = perguntasNecessarias[i],
                            Excelente = 0,
                            Bom = 0,
                            Regular = 0,
                            Ruim = 0,
                            Pessimo = 0
                        };
                        _dbcontext.AvaliacaoRespostas.Add(novaPergunta);
                    }
                }
            }

            await _dbcontext.SaveChangesAsync();
        }      



    }
}
