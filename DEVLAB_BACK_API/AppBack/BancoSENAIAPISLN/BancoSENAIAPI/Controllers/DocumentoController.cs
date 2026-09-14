using Microsoft.AspNetCore.Mvc;

namespace BancoSENAIAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class DocumentoController : Controller
    {
        private readonly string _caminhoRaiz = Path.Combine(
            Directory.GetCurrentDirectory(),
            "ClienteArquivos"
            );
        private static List<Models.DocumentoMetadado> _documentosMetadados = new List<Models.DocumentoMetadado>();

        private static int _nextId = 1;
        [HttpPost("upload/{codigoCliente}")]
        public async Task<ActionResult> AnexarArquivo(int codigoCliente, IFormFile arquivo)
        {
            if (arquivo == null || arquivo.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado.");
            }

            string pastaCliente = Path.Combine(_caminhoRaiz, codigoCliente.ToString());

            if (!Directory.Exists(pastaCliente))
            {
                Directory.CreateDirectory(pastaCliente);
            }

            string extensao = Path.GetExtension(arquivo.FileName);
            string nomeOriginal = Path.GetFileNameWithoutExtension(arquivo.FileName);
            string novoNome = $"{codigoCliente}_{nomeOriginal}_{Guid.NewGuid()}{extensao}";
            string caminhoFinal = Path.Combine(pastaCliente, novoNome);

            using (var strean = new FileStrean(caminhoFinal, FileMode.Create))
            {
                await arquivo.CopyToAsync(strean);
            }

            var documentoMetadados = new Models.DocumentoMetadado
            {
                Id = _nextId++,
                Nome = nomeOriginal,
                Extensao = extensao,
                Caminho = caminhoFinal,
                CodigoCliente = codigoCliente,
            };

            _documentosMetadados.Add(documentoMetadados);

            return Ok(new { mensagem = "Documento anexado com sucesso", arquivoSalvo = novoNome });

        }
        [HttpGet("listar/{codigoCliente}")]
        public IActionResult Listar(int codigoCliente)
        {
            var documentos = _documentosMetadados
                .Where(d => d.CodigoCliente == codigoCliente)
                .ToList();

            if (!documentos.Any())
            {
                return NotFound("Nenhum documento encontrado para este cliente.");
            }

            return Ok(documentos);
        }


        [HttpGet("download/{id}")]
        public IActionResult Download(int id)
        {
            var documento = _documentosMetadados
                .FirstOrDefault(d => d.Id == id);

            if (documento == null)
            {
                return NotFound("Documento não encontrado.");
            }

            if (!System.IO.File.Exists(documento.Caminho))
            {
                return NotFound("Arquivo físico não encontrado.");
            }

            byte[] fileBytes = System.IO.File.ReadAllBytes(documento.Caminho);

            string nomeArquivo = documento.Name + documento.Extensao;

            return File(fileBytes, "application/octet-stream", nomeArquivo);
        }


        [HttpDelete("excluir/{id}")]
        public IActionResult Excluir(int id)
        {
            var documento = _documentosMetadados
                .FirstOrDefault(d => d.Id == id);

            if (documento == null)
            {
                return NotFound("Documento não encontrado.");
            }

            System.IO.File.Delete(documento.Caminho);
            _documentosMetadados.Remove(documento);

            return Ok("Documento excluído com sucesso.");
        }

    }
}