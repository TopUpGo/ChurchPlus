using Analise.Models;

namespace Analise.Repositorio
{
    public interface ICadastroRepositorio
    {
        CadastroModel ListarPorId(int id);
        List<CadastroModel> BuscarTodos();
        CadastroModel Adicionar(CadastroModel cargo);
        CadastroModel Actualizar(CadastroModel cargo);
    }
}
