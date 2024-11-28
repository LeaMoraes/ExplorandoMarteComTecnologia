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
    public class VendaController : ControllerBase
    {

        private readonly ExplorarMarteDBContext _dbcontext;

        public VendaController(ExplorarMarteDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VendaModel>>> PegarVendas()
        {
            var venda = await _dbcontext.Venda.ToListAsync();
            return Ok(venda);
        }

        [HttpGet("{id}")]
        public async Task<VendaModel> PegarVenda(int id)
        {
            var venda = await _dbcontext.Venda.SingleOrDefaultAsync(v => v.Id == id);

            if (venda == null)
            {
                return null;
            }

            return venda;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CriarVenda(VendaModel venda)
        {
            try
            {
                _dbcontext.Venda.Add(venda);
                await _dbcontext.SaveChangesAsync();

                return venda.Id;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao criar a venda: {ex.Message}");
            }
            
        }

    }
}
