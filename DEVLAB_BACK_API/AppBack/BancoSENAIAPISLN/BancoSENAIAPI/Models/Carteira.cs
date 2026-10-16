using System.ComponentModel.DataAnnotations;

namespace BancoSENAIAPI.Models
{
    public class Carteira
    {
        [Key]
        public int NumeroCarteira { get; set; }
        public string NomeCarteira { get; set; } = string.Empty;
        public decimal ApetiteCarteira { get; set; }

    }
}