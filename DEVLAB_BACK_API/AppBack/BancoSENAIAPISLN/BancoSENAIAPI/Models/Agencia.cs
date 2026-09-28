using System.ComponentModel.DataAnnotations;

namespace BancoSENAIAPI.Models
{
    public class Agencia
    {
        [Required]
        [Key]
        public int NumeroAgencia { get; set; } 
        public string Cidade { get; set; } = string.Empty;
        public string SiglaEstado { get; set; } = string.Empty;
        public Agencia(int numeroAgencia, string cidade, string sigla)
        {
            NumeroAgencia = numeroAgencia;
            Cidade = cidade;
            SiglaEstado = sigla;
        }
    }
}
