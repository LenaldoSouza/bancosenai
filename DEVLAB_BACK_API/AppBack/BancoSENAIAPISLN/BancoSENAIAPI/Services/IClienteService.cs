using BancoSENAIAPI.Models;

namespace BancoSENAIAPI.Services
{
    public interface IClienteService
    {
        List<Cliente> ListarTodos();
        Cliente? BuscarPorCodigo(int codigo);
        Cliente Cadastrar(Cliente cliente);
        bool Atualizar(Cliente cliente);
        bool Excluir(int codigo);
    }
}