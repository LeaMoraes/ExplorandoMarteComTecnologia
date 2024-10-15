using ExplorandoMarteComTecnologia_API.Data;
using ExplorandoMarteComTecnologia_API.DTO;
using ExplorandoMarteComTecnologia_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;

namespace ExplorandoMarteComTecnologia_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngressoController : ControllerBase
    {

        private readonly ExplorarMarteDBContext _dbcontext;

        public IngressoController(ExplorarMarteDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngressoModel>>> PegarIngressos()
        {
            var ingressos = await _dbcontext.Ingresso.ToListAsync();
            return Ok(ingressos);

        }

        [HttpGet("{key}")]
        public async Task<ActionResult<IngressoModel>> PegarIngresso(string key)
        {
            var ingresso = await _dbcontext.Ingresso.SingleOrDefaultAsync(i => i.Key == key);

            if (ingresso == null)
            {
                return NotFound();
            }

            return Ok(ingresso);
        }

        [HttpPost]
        public async Task<ActionResult<IngressoModel>> CriarIngresso(List<IngressoDTO> ingressoDTO)
        {
            try
            {
                foreach (var ingressoObjectDTO in ingressoDTO)
                {
                    //Verifica se a Key do ingresso esta nula ou não é de 9 caracteres
                    if (string.IsNullOrEmpty(ingressoObjectDTO.Key) || ingressoObjectDTO.Key.Length != 9)
                    {
                        return BadRequest("A Key do ingresso deve ter exatamente 9 caracteres e não pode ser nula.");
                    }

                    //Cria o ingresso
                    var ingresso = new IngressoModel
                    {
                        Key = ingressoObjectDTO.Key,
                        Nome = ingressoObjectDTO.Nome,
                        Tipo = ingressoObjectDTO.Tipo
                    };

                    _dbcontext.Ingresso.Add(ingresso);
                }

                await _dbcontext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao criar ingresso: {ex.Message}");
            }

        }
    }
}
