using BancoSENAIAPI.Data;
using BancoSENAIAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BancoSENAIAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClienteController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ListarTodos()
        {
            var clientes = await _context.Cliente.ToListAsync();
            return Ok(clientes);
        }

        [HttpGet("{codigo}")]
        public async Task<IActionResult> ConsultarPorCodigo(int codigo)
        {
            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(c => c.CodigoCliente == codigo);

            if (cliente == null)
                return NotFound(new { message = "Cliente não encontrado." });

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] Cliente novoCliente)
        {
            // TODO: mover para cá as validações que existiam no ClienteService
            // (as que lançavam ArgumentException), retornando BadRequest.
            if (await _context.Cliente.AnyAsync(c => c.CodigoCliente == novoCliente.CodigoCliente))
                return BadRequest(new { message = "Este código de cliente já existe." });

            await _context.Cliente.AddAsync(novoCliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ConsultarPorCodigo),
                new { codigo = novoCliente.CodigoCliente }, novoCliente);
        }

        [HttpPut("{codigo}")]
        public async Task<IActionResult> Alterar(int codigo, [FromBody] Cliente clienteAtualizado)
        {
            var clienteExistente = await _context.Cliente
                .FirstOrDefaultAsync(c => c.CodigoCliente == codigo);

            if (clienteExistente == null)
                return NotFound(new { message = "Cliente não encontrado." });

            // TODO: validações do ClienteService (BadRequest) antes de gravar.

            clienteAtualizado.CodigoCliente = codigo;
            _context.Entry(clienteExistente).CurrentValues.SetValues(clienteAtualizado);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{codigo}")]
        public async Task<IActionResult> Excluir(int codigo)
        {
            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(c => c.CodigoCliente == codigo);

            if (cliente == null)
                return NotFound(new { message = "Cliente não encontrado." });

            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Cliente excluído com sucesso." });
        }
    }
}