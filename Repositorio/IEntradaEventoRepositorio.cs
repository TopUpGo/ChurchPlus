using Analise.Models;

namespace Analise.Repositorio
{
    public interface IEntradaEventoRepositorio
    {
        EntradaEventoModel ListarPorId(int id);
        List<EntradaEventoModel> BuscarTodos();
        EntradaEventoModel Adicionar(EntradaEventoModel cargo);
        EntradaEventoModel Actualizar(EntradaEventoModel cargo);
    }
}
