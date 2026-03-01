using Analise.Models;

namespace Analise.Repositorio
{
    public interface IContasPagarRepositorio
    {
        ContasPagarModel ListarPorId(int id);
        List<ContasPagarModel> BuscarTodos();
        ContasPagarModel Adicionar(ContasPagarModel cargo);
        ContasPagarModel Actualizar(ContasPagarModel cargo);
    }
}
