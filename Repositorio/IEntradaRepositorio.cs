using Analise.Models;

namespace Analise.Repositorio
{
    public interface IEntradaRepositorio
    {
        EntradaModel ListarPorId(int id);
        List<EntradaModel> BuscarTodos();
        List<EntradaModel> BuscarTodosRelatorio();
        EntradaModel Adicionar(EntradaModel cargo);
        EntradaModel Actualizar(EntradaModel cargo);
    }
}
