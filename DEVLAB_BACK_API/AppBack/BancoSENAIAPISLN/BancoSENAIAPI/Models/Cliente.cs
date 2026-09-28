namespace BancoSENAIAPI.Models
{
    public class Cliente
    {
        public int idCliente { get; set; }
        public string nomeCompleto { get; set; } = string.Empty;
        public string numCpf { get; set; } = string.Empty;
        public int idAgencia { get; set; } = 10;
        public decimal saldoAtual { get; set; } = 0m;
        public Sexo SexoMF { get; set; }
        public enum Sexo
        {
            Masculino,
            Feminino
        }
    }
}