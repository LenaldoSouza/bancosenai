using BancoSENAIAPI.Models;
using BancoSENAIAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BancoSENAIAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _service;

        public ClienteController(IClienteService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult ListarTodos()
        {
            return Ok(_service.ListarTodos());
        }

        [HttpGet("{codigo}")]
        public IActionResult ConsultarPorCodigo(int codigo)
        {
            var cliente = _service.BuscarPorCodigo(codigo);

            if (cliente == null)
                return NotFound(new { message = "Cliente não encontrado." });

            return Ok(cliente);
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] Cliente novoCliente)
        {
            try
            {
                var cliente = _service.Cadastrar(novoCliente);

                return Created("", cliente);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{codigo}")]
        public IActionResult Alterar(int codigo, [FromBody] Cliente clienteAtualizado)
        {
            clienteAtualizado.CodigoCliente = codigo;

            try
            {
                var atualizado = _service.Atualizar(clienteAtualizado);

                if (!atualizado)
                    return NotFound(new { message = "Cliente não encontrado." });

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{codigo}")]
        public IActionResult Excluir(int codigo)
        {
            var removido = _service.Excluir(codigo);

            if (!removido)
                return NotFound(new { message = "Cliente não encontrado." });

            return Ok(new { message = "Cliente excluído com sucesso." });
        }
    }
}