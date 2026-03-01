using Analise.Models;

namespace Analise.Repositorio
{
    public interface ITurmaRepositorio
    {
        TurmaModel ListarPorId(int id);
        List<TurmaModel> BuscarTodos();
        TurmaModel Adicionar(TurmaModel cargo);
        TurmaModel Actualizar(TurmaModel cargo);
    }
}
