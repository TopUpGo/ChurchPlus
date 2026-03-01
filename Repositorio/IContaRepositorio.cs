using Analise.Models;

namespace Analise.Repositorio
{
    public interface IContaRepositorio
    {
        ContaModel ListarPorId(int id);
        List<ContaModel> BuscarTodos();
        ContaModel Adicionar(ContaModel cargo);
        ContaModel Actualizar(ContaModel cargo);
    }
}
