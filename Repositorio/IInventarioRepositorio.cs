using Analise.Models;

namespace Analise.Repositorio
{
    public interface IInventarioRepositorio
    {
        InventarioModel ListarPorId(int id);
        List<InventarioModel> BuscarTodos();
        InventarioModel Adicionar(InventarioModel cargo);
        InventarioModel Actualizar(InventarioModel cargo);
    }
}
