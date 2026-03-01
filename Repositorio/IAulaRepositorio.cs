using Analise.Models;

namespace Analise.Repositorio
{
    public interface IAulaRepositorio
    {
        AulaModel ListarPorId(int id);
        List<AulaModel> BuscarTodos();
        AulaModel Adicionar(AulaModel cargo);
        AulaModel Actualizar(AulaModel cargo);
    }
}
