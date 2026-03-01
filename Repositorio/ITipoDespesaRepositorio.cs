using Analise.Models;

namespace Analise.Repositorio
{
    public interface ITipoDespesaRepositorio
    {
        TipoDespesaModel ListarPorId(int id);
        List<TipoDespesaModel> BuscarTodos();
        TipoDespesaModel Adicionar(TipoDespesaModel cargo);
        TipoDespesaModel Actualizar(TipoDespesaModel cargo);
    }
}
