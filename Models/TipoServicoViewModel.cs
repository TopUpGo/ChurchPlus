using Analise.Enuns;
using System.ComponentModel.DataAnnotations;

namespace Analise.Models
{
    public class TipoServicoViewModel
    {
        public TipoServicoModel TipoServicoNome { get; set; }
        public List<TipoServicoModel> ListaTipoServicos { get; set; }
    }
}
