using Analise.Models;

namespace Analise.Repositorio
{
    public interface ITipoSaidaRepositorio
    {
        TipoSaidaModel ListarPorId(int id);
        List<TipoSaidaModel> BuscarTodos();
        TipoSaidaModel Adicionar(TipoSaidaModel cargo);
        TipoSaidaModel Actualizar(TipoSaidaModel cargo);
    }
}
