using Analise.Models;

namespace Analise.Repositorio
{
    public interface ITipoServicoRepositorio
    {
        TipoServicoModel ListarPorId(int id);
        List<TipoServicoModel> BuscarTodos();
        TipoServicoModel Adicionar(TipoServicoModel cargo);
        TipoServicoModel Actualizar(TipoServicoModel cargo);
    }
}
