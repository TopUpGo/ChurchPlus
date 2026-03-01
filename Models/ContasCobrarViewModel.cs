using Analise.Enuns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Analise.Models
{
    public class ContasCobrarViewModel
    {
        public ContasCobrarModel ContasCobrarNome { get; set; }
        public List<ContasCobrarModel> ListaContasCobrars { get; set; }
    }
}
