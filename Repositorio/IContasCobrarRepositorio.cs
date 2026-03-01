using Analise.Models;

namespace Analise.Repositorio
{
    public interface IContasCobrarRepositorio
    {
        ContasCobrarModel ListarPorId(int id);
        List<ContasCobrarModel> BuscarTodos();
        ContasCobrarModel Adicionar(ContasCobrarModel cargo);
        ContasCobrarModel Actualizar(ContasCobrarModel cargo);
    }
}
