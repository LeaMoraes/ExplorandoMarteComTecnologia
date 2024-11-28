using ExplorandoMarteComTecnologia_API.Data;
using ExplorandoMarteComTecnologia_API.DTO;
using ExplorandoMarteComTecnologia_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExplorandoMarteComTecnologia_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionarioRespostasController : ControllerBase
    {

        private readonly ExplorarMarteDBContext _dbcontext;

        public QuestionarioRespostasController(ExplorarMarteDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionarioRespostasModel>>> PegarTodasPerguntas()
        {
            var perguntas = await _dbcontext.QuestionarioRespostas.ToListAsync();
            return Ok(perguntas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionarioRespostasModel>> PegarPergunta(int id)
        {
            var pergunta = await _dbcontext.QuestionarioRespostas.SingleOrDefaultAsync(p => p.Id == id);

            if (pergunta == null)
            {
                return NotFound();
            }

            return Ok(pergunta);
        }

        [HttpPost]
        public async Task<ActionResult<QuestionarioRespostasModel>> CriarPergunta(QuestionarioRespostasModel questionarioRespostas)
        {
            Validacao validacao = new Validacao();

            //Garantia que na criação de pergunta, os valores iniciam com 0
            questionarioRespostas.Acertos = 0;
            questionarioRespostas.Erros = 0;

            try
            {
                List<QuestionarioRespostasModel> perguntas = await _dbcontext.QuestionarioRespostas.ToListAsync();
                bool existe = validacao.PerguntaExiste(perguntas, questionarioRespostas.Pergunta);
                if (existe == true)
                {
                    return BadRequest("A pergunta já existe no banco de dados.");
                }

                _dbcontext.QuestionarioRespostas.Add(questionarioRespostas);              
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
        public async Task<ActionResult> AtualizarRespostas(QuestionarioRespostasModel questionarioRespostas)
        {

            await VerificarEAdicionarPerguntas();

            Validacao validacao = new Validacao();

            var pergunta = await _dbcontext.QuestionarioRespostas.FirstOrDefaultAsync(p => p.Id == questionarioRespostas.Id);

            if (pergunta == null)
            {
                return NotFound("Pergunta não encontrada!");
            }

            List<QuestionarioRespostasModel> perguntas = await _dbcontext.QuestionarioRespostas.ToListAsync();
            bool perguntaIdIgual = validacao.PerguntaIdIgual(perguntas, questionarioRespostas.Id, questionarioRespostas.Pergunta);

            if (perguntaIdIgual == false)
            {
                return NotFound("O ID não bate com a Pergunta no banco de dados.");
            }

            pergunta.Acertos += questionarioRespostas.Acertos;
            pergunta.Erros += questionarioRespostas.Erros;

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
                "VulcaoSistemaSolar",
                "FundadorSpaceX",
                "FenomenoVermelho",
                "VidaPassada",
                "MonteOlimpo"
            };

            var perguntasExistentes = await _dbcontext.QuestionarioRespostas.ToListAsync();

            // Verificar se as perguntas existem e estão na ordem correta
            for (int i = 0; i < perguntasNecessarias.Count; i++)
            {
                var pergunta = perguntasExistentes.FirstOrDefault(p => p.Pergunta == perguntasNecessarias[i]);

                // Se a pergunta não existir, cria ela
                if (pergunta == null)
                {
                    QuestionarioRespostasModel novaPergunta = new QuestionarioRespostasModel
                    {
                        Pergunta = perguntasNecessarias[i],
                        Acertos = 0,
                        Erros = 0
                    };
                    _dbcontext.QuestionarioRespostas.Add(novaPergunta);
                }
                // Caso a pergunta exista mas a ordem esteja errada, remova e insira novamente
                else
                {
                    if (pergunta.Pergunta != perguntasNecessarias[i])
                    {
                        _dbcontext.QuestionarioRespostas.Remove(pergunta);
                        QuestionarioRespostasModel novaPergunta = new QuestionarioRespostasModel
                        {
                            Pergunta = perguntasNecessarias[i],
                            Acertos = 0,
                            Erros = 0
                        };
                        _dbcontext.QuestionarioRespostas.Add(novaPergunta);
                    }
                }
            }

            await _dbcontext.SaveChangesAsync();
        }


    }
}
