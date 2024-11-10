using ExplorandoMarteComTecnologia_API.Data;
using ExplorandoMarteComTecnologia_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExplorandoMarteComTecnologia_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvaliacaoSugestaoController : ControllerBase
    {
        private readonly ExplorarMarteDBContext _dbcontext;

        public AvaliacaoSugestaoController(ExplorarMarteDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpPost]
        public async Task<ActionResult> ArmazenarSugestao(AvaliacaoSugestaoModel avaliacaoSugestaoModel)
        {
            _dbcontext.AvaliacaoSugestao.Add(avaliacaoSugestaoModel);
            await _dbcontext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AvaliacaoSugestaoModel>>> PegarTodasSugestoes()
        {
            var sugestoes = await _dbcontext.AvaliacaoSugestao.ToListAsync();
            return Ok(sugestoes);
        }

    }
}
