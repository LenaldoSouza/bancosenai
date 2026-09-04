using BancoSENAIAPI.Models;

namespace BancoSENAIAPI.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private static readonly List<Cliente> _clientes = new List<Cliente>();
        private static int _proximoCodigo = 1;

        public List<Cliente> ListarTodos()
        {
            return _clientes;
        }

        public Cliente? BuscarPorCodigo(int codigo)
        {
            return _clientes.FirstOrDefault(c => c.CodigoCliente == codigo);
        }

        public Cliente Adicionar(Cliente cliente)
        {
            cliente.CodigoCliente = _proximoCodigo++;
            _clientes.Add(cliente);

            return cliente;
        }

        public bool Atualizar(Cliente cliente)
        {
            var clienteExistente = BuscarPorCodigo(cliente.CodigoCliente);

            if (clienteExistente == null)
                return false;

            clienteExistente.NomeCliente = cliente.NomeCliente;
            clienteExistente.CPF = cliente.CPF;
            clienteExistente.NumeroAgencia = cliente.NumeroAgencia;
            clienteExistente.SaldoTotal = cliente.SaldoTotal;
            clienteExistente.Sexo = cliente.Sexo;
            clienteExistente.Endereco = cliente.Endereco;
            clienteExistente.Cidade = cliente.Cidade;
            clienteExistente.Estado = cliente.Estado;

            return true;
        }

        public bool Remover(int codigo)
        {
            var cliente = BuscarPorCodigo(codigo);

            if (cliente == null)
                return false;

            _clientes.Remove(cliente);

            return true;
        }
    }
}