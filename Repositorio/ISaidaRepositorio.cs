using Analise.Models;

namespace Analise.Repositorio
{
    public interface ISaidaRepositorio
    {
        SaidaModel ListarPorId(int id);
        List<SaidaModel> BuscarTodos();
        SaidaModel Adicionar(SaidaModel cargo);
        SaidaModel Actualizar(SaidaModel cargo);
        List<SaidaModel> BuscarTodosRelatorio();
    }
}
