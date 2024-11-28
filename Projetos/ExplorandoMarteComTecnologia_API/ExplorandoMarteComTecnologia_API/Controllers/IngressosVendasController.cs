using ExplorandoMarteComTecnologia_API.Data;
using ExplorandoMarteComTecnologia_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExplorandoMarteComTecnologia_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngressosVendasController : ControllerBase
    {

        private readonly ExplorarMarteDBContext _dbcontext;

        public IngressosVendasController(ExplorarMarteDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet("{key_ingresso}")]
        public async Task<ActionResult<int>> PegarIdEmail(string key_ingresso)
        {
            var ingressoVenda = await _dbcontext.IngressoVenda.SingleOrDefaultAsync(iv => iv.Key_Ingresso == key_ingresso);

            if (ingressoVenda == null)
            {
                return NotFound();
            }

            return ingressoVenda.Id_Venda;
        }

        [HttpPost]
        public async Task<ActionResult<IngressoVendaModel>> CriarAssociacaoIdKey(int id_venda, string key_ingresso)
        {
            var IngressoVenda = new IngressoVendaModel {
                Id_Venda = id_venda,
                Key_Ingresso = key_ingresso
            };

            try
            {
                _dbcontext.IngressoVenda.Add(IngressoVenda);
                await _dbcontext.SaveChangesAsync();
                return Ok(IngressoVenda);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao criar a associação: {ex.Message}");
            }
            
        }


    }
}
