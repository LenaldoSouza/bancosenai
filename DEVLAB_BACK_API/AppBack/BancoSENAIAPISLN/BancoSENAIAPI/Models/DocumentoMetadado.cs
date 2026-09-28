namespace BancoSENAIAPI.Models
{
    public class DocumentoMetadado
    {
        public int idDoc { get; set; } 
        public string nomeArquivo { get; set; }
        public string extArquivo { get; set;}
        public string caminhoArquivo { get; set; }
        public int idCliente { get; set; }
    }
}
