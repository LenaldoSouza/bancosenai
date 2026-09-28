using BancoSENAIAPI.Models;
using Microsoft.AspNetCore.Mvc;
using BancoSENAIAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace BancoSENAIAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AgenciaController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> ListarTodas()
        {
            var agencias = await _context.Agencia.FirstOrDefaultAsync(a async => a.NumeroAgencia == codigo);

            if (agencias == null)
            {
                return NotFound(new {message = "Agencia não encontrada."});
            }

            return Ok(agencias);
        }
        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] Agencia novaAgencia)
        {
            
            if (_agencias.Any(a => a.NumeroAgencia == novaAgencia.NumeroAgencia))
                return BadRequest(new { message = "Este número de agência já existe." });

            _agencias.Add(novaAgencia);
            // Retorna Status 201 Created conforme boas práticas REST [6, 8]
            return Created("", novaAgencia);
        }

        [HttpGet("{codigo}")]
        public async Task<IActionResult> ConsultarPorCodigo(int codigo)
        {
            var agencia = _agencias.FirstOrDefault(a => a.NumeroAgencia == codigo);

            if (agencia == null)
                return NotFound(new { message = "Agência não encontrada." }); // Status 404 [6, 7]

            return Ok(agencia); // Status 200 OK [6, 7]
        }

        [HttpPut("{codigo}")]
        public async Task<IActionResult> Alterar(int codigo, [FromBody] Agencia agenciaAtualizada)
        {
            var agenciaExistente = _agencias.FirstOrDefault(a => a.NumeroAgencia == codigo);

            if (agenciaExistente == null) return NotFound();

            agenciaExistente.Cidade = agenciaAtualizada.Cidade;
            agenciaExistente.SiglaEstado = agenciaAtualizada.SiglaEstado;

            // Retorna Status 204 No Content para atualizações bem-sucedidas [6, 9]
            return NoContent();
        }

        [HttpDelete("{codigo}")]
        public async Task<IActionResult> Excluir(int codigo)
        {
            var agencia = _agencias.FirstOrDefault(a => a.NumeroAgencia == codigo);

            if (agencia == null) return NotFound();

            _agencias.Remove(agencia);
            return Ok(new { message = "Agência excluída com sucesso." }); // Status 200 [6]
        }

    }
}
