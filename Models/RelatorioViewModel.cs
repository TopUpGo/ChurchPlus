using Analise.Enuns;
using System.ComponentModel.DataAnnotations;

namespace Analise.Models
{
    public class RelatorioViewModel
    {
        public EntradaModel EntradaNome { get; set; }
        public List<EntradaModel> ListaEntradas { get; set; }
    }
}
