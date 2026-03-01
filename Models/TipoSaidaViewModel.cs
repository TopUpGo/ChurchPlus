using Analise.Enuns;
using System.ComponentModel.DataAnnotations;

namespace Analise.Models
{
    public class TipoSaidaViewModel
    {
        public TipoSaidaModel TipoSaidaNome { get; set; }
        public List<TipoSaidaModel> ListaTipoSaidas { get; set; }
    }
}
