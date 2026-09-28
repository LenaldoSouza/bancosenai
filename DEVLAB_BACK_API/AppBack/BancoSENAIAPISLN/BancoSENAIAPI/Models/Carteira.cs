namespace BancoSENAIAPI.Models
{
    public class Carteira
    {
        public int idCarteira { get; set; }
        public string tituloCarteira { get; set; } = string.Empty;
        public decimal nivelApetite { get; set; }

        public Carteira(int numeroCarteira, string nomeCarteira, decimal apetiteCarteira = 1000000)
        {
            idCarteira = numeroCarteira;
            tituloCarteira = nomeCarteira;
            nivelApetite = apetiteCarteira;

            if (apetiteCarteira < 0)
            {
                throw new ArgumentException("Apetite carteira não pode ser negativo.");
            }
        }
    }
}