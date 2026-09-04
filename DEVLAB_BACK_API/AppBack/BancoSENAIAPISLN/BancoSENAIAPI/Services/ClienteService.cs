using BancoSENAIAPI.Models;
using BancoSENAIAPI.Repositories;

namespace BancoSENAIAPI.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;

        public ClienteService(IClienteRepository repository)
        {
            _repository = repository;
        }

        public List<Cliente> ListarTodos()
        {
            return _repository.ListarTodos();
        }

        public Cliente? BuscarPorCodigo(int codigo)
        {
            return _repository.BuscarPorCodigo(codigo);
        }

        public Cliente Cadastrar(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.NomeCliente))
                throw new ArgumentException("O nome do cliente é obrigatório.");

            if (string.IsNullOrWhiteSpace(cliente.CPF))
                throw new ArgumentException("O CPF do cliente é obrigatório.");

            return _repository.Adicionar(cliente);
        }

        public bool Atualizar(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.NomeCliente))
                throw new ArgumentException("O nome do cliente é obrigatório.");

            if (string.IsNullOrWhiteSpace(cliente.CPF))
                throw new ArgumentException("O CPF do cliente é obrigatório.");

            return _repository.Atualizar(cliente);
        }

        public bool Excluir(int codigo)
        {
            return _repository.Remover(codigo);
        }
    }
}