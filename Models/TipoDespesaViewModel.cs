using Analise.Enuns;
using System.ComponentModel.DataAnnotations;

namespace Analise.Models
{
    public class TipoDespesaViewModel
    {
        public TipoDespesaModel TipoDespesaNome { get; set; }
        public List<TipoDespesaModel> ListaTipoDespesas { get; set; }
    }
}
