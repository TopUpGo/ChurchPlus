using Analise.Models;

namespace Analise.Repositorio
{
    public interface IEventoRepositorio
    {
        EventoModel ListarPorId(int id);
        List<EventoModel> BuscarTodos();
        EventoModel Adicionar(EventoModel cargo);
        EventoModel Actualizar(EventoModel cargo);
    }
}
