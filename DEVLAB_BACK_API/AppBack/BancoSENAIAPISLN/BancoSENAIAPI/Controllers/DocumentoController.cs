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

        private static List<Models.DocumentoMetadado> _documentosMetadados =
            new List<Models.DocumentoMetadado>();

        private static int _nextId = 1;

        [HttpPost("upload/{codigoCliente}")]
        public async Task<ActionResult> AnexarArquivo(
            int codigoCliente,
            IFormFile arquivo)
        {
            if (arquivo == null || arquivo.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado.");
            }

            // R06F - Limite de tamanho: 2 MB
            const long limiteTamanho = 2 * 1024 * 1024;

            if (arquivo.Length > limiteTamanho)
            {
                return BadRequest(
                    "O arquivo excede o limite máximo de 2 MB."
                );
            }

            // R06G - Extensões permitidas
            string extensao = Path.GetExtension(arquivo.FileName)
                .ToLowerInvariant();

            string[] extensoesPermitidas =
            {
                ".pdf",
                ".jpg",
                ".png"
            };

            if (!extensoesPermitidas.Contains(extensao))
            {
                return BadRequest(
                    "Extensão de arquivo não permitida. " +
                    "Use apenas arquivos .pdf, .jpg ou .png."
                );
            }

            string pastaCliente = Path.Combine(
                _caminhoRaiz,
                codigoCliente.ToString()
            );

            if (!Directory.Exists(pastaCliente))
            {
                Directory.CreateDirectory(pastaCliente);
            }

            string nomeOriginal =
                Path.GetFileNameWithoutExtension(arquivo.FileName);

            string novoNome =
                $"{codigoCliente}_{nomeOriginal}_{Guid.NewGuid()}{extensao}";

            string caminhoFinal =
                Path.Combine(pastaCliente, novoNome);

            using (var stream = new FileStream(
                caminhoFinal,
                FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
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

            return Ok(new
            {
                mensagem = "Documento anexado com sucesso",
                arquivoSalvo = novoNome
            });
        }
    }
}