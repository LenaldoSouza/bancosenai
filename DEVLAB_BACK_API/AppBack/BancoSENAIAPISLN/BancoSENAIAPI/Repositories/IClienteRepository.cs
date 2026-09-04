using BancoSENAIAPI.Models;

namespace BancoSENAIAPI.Repositories
{
    public interface IClienteRepository
    {
        List<Cliente> ListarTodos();
        Cliente? BuscarPorCodigo(int codigo);
        Cliente Adicionar(Cliente cliente);
        bool Atualizar(Cliente cliente);
        bool Remover(int codigo);
    }
}