using Analise.Models;

namespace Analise.Repositorio
{
    public interface IAlunoRepositorio
    {
        AlunoModel ListarPorId(int id);
        List<AlunoModel> BuscarTodos();
        AlunoModel Adicionar(AlunoModel cargo);
        AlunoModel Actualizar(AlunoModel cargo);
    }
}
