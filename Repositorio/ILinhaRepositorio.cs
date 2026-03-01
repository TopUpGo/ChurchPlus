using Analise.Models;

namespace Analise.Repositorio
{
    public interface ILinhaRepositorio
    {
        LinhaModel ListarPorId(int id);
        List<LinhaModel> BuscarTodos();
        LinhaModel Adicionar(LinhaModel cargo);
        LinhaModel Actualizar(LinhaModel cargo);
    }
}
