using BancoSENAIAPI.Data;
using BancoSENAIAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BancoSENAIAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class AgenciaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AgenciaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ListarTodas()
        {
            var agencias = await _context.Agencia.ToListAsync();
            return Ok(agencias);
        }

        [HttpGet("{codigo}")]
        public async Task<IActionResult> ConsultarPorCodigo(int codigo)
        {
            var agencia = await _context.Agencia
                .FirstOrDefaultAsync(a => a.NumeroAgencia == codigo);

            if (agencia == null)
                return NotFound(new { message = "Agência não encontrada." });

            return Ok(agencia);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] Agencia novaAgencia)
        {
            if (await _context.Agencia.AnyAsync(a => a.NumeroAgencia == novaAgencia.NumeroAgencia))
                return BadRequest(new { message = "Este número de agência já existe." });

            await _context.Agencia.AddAsync(novaAgencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ConsultarPorCodigo),
                new { codigo = novaAgencia.NumeroAgencia }, novaAgencia);
        }

        [HttpPut("{codigo}")]
        public async Task<IActionResult> Alterar(int codigo, [FromBody] Agencia agenciaAtualizada)
        {
            var agenciaExistente = await _context.Agencia
                .FirstOrDefaultAsync(a => a.NumeroAgencia == codigo);

            if (agenciaExistente == null)
                return NotFound(new { message = "Agência não encontrada." });

            agenciaExistente.Cidade = agenciaAtualizada.Cidade;
            agenciaExistente.SiglaEstado = agenciaAtualizada.SiglaEstado;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{codigo}")]
        public async Task<IActionResult> Excluir(int codigo)
        {
            var agencia = await _context.Agencia
                .FirstOrDefaultAsync(a => a.NumeroAgencia == codigo);

            if (agencia == null)
                return NotFound(new { message = "Agência não encontrada." });

            _context.Agencia.Remove(agencia);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Agência excluída com sucesso." });
        }
    }
}