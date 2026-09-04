namespace BancoSENAIAPI.Models
{
    public class Carteira
    {
        public int NumeroCarteira { get; set; }
        public string NomeCarteira { get; set; } = string.Empty;
        public decimal ApetiteCarteira { get; set; }

        public Carteira(int numeroCarteira, string nomeCarteira, decimal apetiteCarteira)
        {
            NumeroCarteira = numeroCarteira;
            NomeCarteira = nomeCarteira;
            ApetiteCarteira = apetiteCarteira;

            if (ApetiteCarteira < NumeroCarteira)
            {
                throw new ArgumentException("O valor do apetite deve ser maior ao valor da carteira");
            }
        }
    }
}