using Analise.Models;

namespace Analise.Repositorio
{
    public interface IFechoRepositorio
    {
        FechoModel ListarPorId(int id);
        List<FechoModel> BuscarTodosFecho();
        List<FechoModel> BuscarTodosAbertura();
    }
}
