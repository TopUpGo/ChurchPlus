using Analise.Enuns;
using System.ComponentModel.DataAnnotations;

namespace Analise.Models
{
    public class ContaViewModel
    {
        public ContaModel ContaNome { get; set; }
        public List<ContaModel> ListaContas { get; set; }
    }
}
