using Analise.Models;

namespace Analise.Repositorio
{
    public interface ITicketRepositorio
    {
        void Adicionar(TicketModel ticket);

        TicketModel ListarPorId(int id);

        List<TicketModel> BuscarTodos();

        TicketModel Actualizar(TicketModel registo);

        // Novo método para buscar tickets com filtros
        List<TicketModel> BuscarFiltrados(string estado, DateTime? dataInicio, DateTime? dataFim);
    }
}
