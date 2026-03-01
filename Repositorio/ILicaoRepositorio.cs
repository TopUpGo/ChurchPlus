using Analise.Models;

namespace Analise.Repositorio
{
    public interface ILicaoRepositorio
    {
        LicaoModel ListarPorId(int id);
        List<LicaoModel> BuscarTodos();
        LicaoModel Adicionar(LicaoModel cargo);
        LicaoModel Actualizar(LicaoModel cargo);
    }
}
