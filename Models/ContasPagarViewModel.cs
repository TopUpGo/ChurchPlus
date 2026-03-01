using Analise.Enuns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Analise.Models
{
    public class ContasPagarViewModel
    {
        public ContasPagarModel ContasPagarNome { get; set; }
        public List<ContasPagarModel> ListaContasPagars { get; set; }
    }
}
