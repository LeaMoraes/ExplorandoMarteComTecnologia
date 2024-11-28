using ExplorandoMarteComTecnologia_API.Data;
using ExplorandoMarteComTecnologia_API.DTO;
using ExplorandoMarteComTecnologia_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Cryptography;

namespace ExplorandoMarteComTecnologia_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatorioIngressosController : ControllerBase
    {

        private readonly ExplorarMarteDBContext _dbcontext;

        public RelatorioIngressosController(ExplorarMarteDBContext dbContext)
        {
            _dbcontext = dbContext;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<RelatorioIngressosModel>>> PegarRelatorios()
        {
            var relatorio = await _dbcontext.RelatorioIngressos.ToListAsync();
            return Ok(relatorio);
        }

        [HttpGet("{anoMesDia}")]
        public async Task<ActionResult<RelatorioIngressosModel>> PegarRelatorio(string anoMesDia)
        {

            //É necessario que anoMesDia seja em string, pois se o metodo for receber um DateOnly, o controller vai dar erro
            //Por conta da formatação do DateOnly

            //Converte a string anoMesDia para Int
            Validacao validacao = new Validacao();
            int ano = validacao.SepararConverterAnoMesDia(anoMesDia).ano;
            int mes = validacao.SepararConverterAnoMesDia(anoMesDia).mes;
            int dia = validacao.SepararConverterAnoMesDia(anoMesDia).dia;
            //Cria um DateOnly com as variaveis convertidas
            var data = new DateOnly(ano, mes, dia);

            var relatorio = await _dbcontext.RelatorioIngressos.FirstOrDefaultAsync(ri => ri.RelatorioData == data);

            if (relatorio == null)
            {
                return NotFound("Relatorio não existe!");
            }
            return Ok(relatorio);
        }

        [HttpPost]
        public async Task<ActionResult<RelatorioIngressosModel>> CriarRelatorioIngressos()
        {
            //Pega a data atual e verifica se o relatorio ja existe
            var data = DateOnly.FromDateTime(DateTime.Now);
            if (RelatorioExists(data.Year, data.Month, data.Day))
            {
                return Conflict("Relatorio ja existe!");
            }

            //Faz um novo relatorio com valores zerados
            var relatorio = new RelatorioIngressosModel()
            {
                RelatorioData = data,
                TotalIngressosVendidos = 0,            
                TotalIngressosInteiro = 0,
                TotalIngressosMeia = 0,
                TotalIngressosIsentos = 0
            };

            _dbcontext.RelatorioIngressos.Add(relatorio);
            await _dbcontext.SaveChangesAsync();

            return Ok("Relatorio novo criado\nPronto para receber informações\n\n" + relatorio);
        }

        [HttpPut("{anoMesDia}")]
        public async Task<ActionResult> AtualizarRelatorio(string anoMesDia, RelatorioIngressosDTO relatorioDTO)
        {

            //É necessario que anoMesDia seja em string, pois se o metodo for receber um DateOnly, o controller vai dar erro
            //Por conta da formatação do DateOnly

            //Converte a string anoMesDia para Int
            Validacao validacao = new Validacao();
            int ano = validacao.SepararConverterAnoMesDia(anoMesDia).ano;
            int mes = validacao.SepararConverterAnoMesDia(anoMesDia).mes;
            int dia = validacao.SepararConverterAnoMesDia(anoMesDia).dia;            

            //Verifica se a data do relatorioDTO esta nos parametros do DateOnly
            if (!DateOnly.TryParse(relatorioDTO.RelatorioData, out DateOnly data))
            {
                return BadRequest("Data inválida.");
            }            

            //Verifica se a data do relatorioDTO corresponde a data fornecida
            if (data.Year != ano || data.Month != mes || data.Day != dia)
            {
                return BadRequest("Data do relatórioDTO não corresponde à data fornecida.");
            }

            //Verifica se relatorio existe, caso não, ele cria um
            if (!RelatorioExists(ano, mes, dia))
            {
                await CriarRelatorioIngressos();
            }

            var relatorio = await _dbcontext.RelatorioIngressos.FirstOrDefaultAsync(ri => ri.RelatorioData == data);

            //Faz uma verificação se a variavel esta nula
            if (relatorio == null)
            {
                return NotFound("Relatório não encontrado.");
            }

            //Adiciona os valores do DTO para o relatorio
            relatorio.TotalIngressosVendidos += relatorioDTO.TotalIngressosVendidos;
            relatorio.TotalIngressosInteiro += relatorioDTO.TotalIngressosInteiro;
            relatorio.TotalIngressosMeia += relatorioDTO.TotalIngressosMeia;
            relatorio.TotalIngressosIsentos += relatorioDTO.TotalIngressosIsentos;

            _dbcontext.Entry(relatorio).State = EntityState.Modified;

            try
            {
                await _dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelatorioExists(ano, mes, dia))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> AtualizarRelatorio(RelatorioIngressosDTO relatorioDTO)
        {            

            //Verifica se a data do relatorioDTO esta nos parametros do DateOnly
            if (!DateOnly.TryParse(relatorioDTO.RelatorioData, out DateOnly data))
            {
                return BadRequest("Data inválida.");
            }

            if (!RelatorioExists(relatorioDTO.RelatorioData))
            {
                await CriarRelatorioIngressos();
            }

            var relatorio = await _dbcontext.RelatorioIngressos.FirstOrDefaultAsync(ri => ri.RelatorioData == data);

            //Faz uma verificação se a variavel esta nula
            if (relatorio == null)
            {
                return NotFound("Relatório não encontrado.");
            }

            //Adiciona os valores do DTO para o relatorio
            relatorio.TotalIngressosVendidos += relatorioDTO.TotalIngressosVendidos;
            relatorio.TotalIngressosInteiro += relatorioDTO.TotalIngressosInteiro;
            relatorio.TotalIngressosMeia += relatorioDTO.TotalIngressosMeia;
            relatorio.TotalIngressosIsentos += relatorioDTO.TotalIngressosIsentos;

            _dbcontext.Entry(relatorio).State = EntityState.Modified;

            try
            {
                await _dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return Ok();
        }

        private bool RelatorioExists(int Ano, int Mes, int Dia)
        {
            var data = new DateOnly(Ano, Mes, Dia);
            var relatorio = _dbcontext.RelatorioIngressos.SingleOrDefault(ri => ri.RelatorioData == data);
            return relatorio != null;
        }

        private bool RelatorioExists(string relatorioData)
        {
            DateOnly.TryParse(relatorioData, out DateOnly data);
            var relatorio = _dbcontext.RelatorioIngressos.SingleOrDefault(ri => ri.RelatorioData == data);
            return relatorio != null;
        }

    }
}
