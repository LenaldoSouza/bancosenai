namespace BancoSENAIAPI.Models
{
    public class Cliente
    {
        public int CodigoCliente { get; set; }
        public string NomeCliente { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public int NumeroAgencia { get; set; } = 10;
        public decimal SaldoTotal { get; set; } = 0m;
        public Sexo SexoMF { get; set; }
        public string Endereco { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;

        public enum Sexo
        {
            Masculino,
            Feminino
        }
    }
}