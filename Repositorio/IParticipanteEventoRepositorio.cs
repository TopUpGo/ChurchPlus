using Analise.Models;

namespace Analise.Repositorio
{
    public interface IParticipanteEventoRepositorio
    {
        ParticipanteEventoModel ListarPorId(int id);
        List<ParticipanteEventoModel> BuscarTodos();
        ParticipanteEventoModel Adicionar(ParticipanteEventoModel cargo);
        ParticipanteEventoModel Actualizar(ParticipanteEventoModel cargo);
    }
}
