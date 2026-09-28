using BancoSENAIAPI.Data;
using BancoSENAIAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BancoSENAIAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DocumentoController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly string _caminhoRaiz = Path.Combine(
            Directory.GetCurrentDirectory(),
            "ClienteArquivos"
        );

        public DocumentoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("upload/{codigoCliente}")]
        public async Task<ActionResult> AnexarArquivo(
            int codigoCliente,
            IFormFile arquivo)
        {
            if (arquivo == null || arquivo.Length == 0)
                return BadRequest("Nenhum arquivo foi enviado.");

            // R06F - Limite de tamanho: 2 MB
            const long limiteTamanho = 2 * 1024 * 1024;

            if (arquivo.Length > limiteTamanho)
                return BadRequest("O arquivo excede o limite máximo de 2 MB.");

            // R06G - Extensões permitidas
            string extensao = Path.GetExtension(arquivo.FileName).ToLowerInvariant();
            string[] extensoesPermitidas = { ".pdf", ".jpg", ".png" };

            if (!extensoesPermitidas.Contains(extensao))
                return BadRequest(
                    "Extensão de arquivo não permitida. " +
                    "Use apenas arquivos .pdf, .jpg ou .png.");

            // O cliente precisa existir no banco
            if (!await _context.Cliente.AnyAsync(c => c.CodigoCliente == codigoCliente))
                return NotFound(new { message = "Cliente não encontrado." });

            string pastaCliente = Path.Combine(_caminhoRaiz, codigoCliente.ToString());
            Directory.CreateDirectory(pastaCliente);

            string nomeOriginal = Path.GetFileNameWithoutExtension(arquivo.FileName);
            string novoNome = $"{codigoCliente}_{nomeOriginal}_{Guid.NewGuid()}{extensao}";
            string caminhoFinal = Path.Combine(pastaCliente, novoNome);

            using (var stream = new FileStream(caminhoFinal, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            // Id gerado pelo banco (sem _nextId)
            var documentoMetadado = new DocumentoMetadado
            {
                Nome = nomeOriginal,
                Extensao = extensao,
                Caminho = caminhoFinal,
                CodigoCliente = codigoCliente
            };

            await _context.DocumentoMetadado.AddAsync(documentoMetadado);
            await _context.SaveChangesAsync();

            return Created("", new
            {
                mensagem = "Documento anexado com sucesso",
                id = documentoMetadado.Id,
                arquivoSalvo = novoNome
            });
        }
    }
}