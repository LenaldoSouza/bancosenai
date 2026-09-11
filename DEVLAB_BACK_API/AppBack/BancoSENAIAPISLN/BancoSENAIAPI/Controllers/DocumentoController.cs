using Microsoft.AspNetCore.Mvc;

namespace BancoSENAIAPI.Controllers
{
    [ApiControler]
    [Route("api/v1/[controller]")]
    public class DocumentoController : Controllers
    {
        private readonly string _documentoRaiz = Path.Combine();
    }
}
