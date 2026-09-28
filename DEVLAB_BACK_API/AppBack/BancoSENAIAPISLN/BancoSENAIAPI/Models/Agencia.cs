namespace BancoSENAIAPI.Models
{
    public class agencia
    {
        public int idAgencia { get; set; } 
        public string nomeCidade { get; set; } = string.Empty;
        public string ufEstado { get; set; } = string.Empty;
        public agencia(int numeroAgencia, string cidade, string sigla)
        {
            idAgencia = numeroAgencia;
            nomeCidade = cidade;
            ufEstado = sigla;
        }
    }
}
