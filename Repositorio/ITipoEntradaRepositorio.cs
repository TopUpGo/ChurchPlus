using Analise.Models;

namespace Analise.Repositorio
{
    public interface ITipoEntradaRepositorio
    {
        TipoEntradaModel ListarPorId(int id);
        List<TipoEntradaModel> BuscarTodos();
        TipoEntradaModel Adicionar(TipoEntradaModel cargo);
        TipoEntradaModel Actualizar(TipoEntradaModel cargo);
    }
}
