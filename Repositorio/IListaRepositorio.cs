using Analise.Models;

namespace Analise.Repositorio
{
    public interface IListaRepositorio
    {
        ListaModel ListarPorId(int id);
        List<ListaModel> BuscarTodos();
        ListaModel Adicionar(ListaModel cargo);
        ListaModel Actualizar(ListaModel cargo);
    }
}
