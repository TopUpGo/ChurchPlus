using Analise.Enuns;
using System.ComponentModel.DataAnnotations;

namespace Analise.Models
{
    public class TipoEntradaViewModel
    {
        public TipoEntradaModel TipoEntradaNome { get; set; }
        public List<TipoEntradaModel> ListaTipoEntradas { get; set; }
    }
}
