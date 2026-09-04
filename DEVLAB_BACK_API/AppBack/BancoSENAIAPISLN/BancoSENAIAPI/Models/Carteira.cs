namespace BancoSENAIAPI.Models
{
    public class Carteira
    {
        public int NumeroCarteira { get; set; }
        public string NomeCarteira { get; set; } = string.Empty;
        public decimal ApetiteCarteira { get; set; }

        public Carteira(int numeroCarteira, string nomeCarteira, decimal apetiteCarteira = 1000000)
        {
            NumeroCarteira = numeroCarteira;
            NomeCarteira = nomeCarteira;
            ApetiteCarteira = apetiteCarteira;

            if (ApetiteCarteira < 0)
            {
                throw new ArgumentException("Apetite carteira não pode ser negativo.");
            }
        }
    }
}