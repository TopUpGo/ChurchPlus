using Analise.Enuns;
using System.ComponentModel.DataAnnotations;

namespace Analise.Models
{
    public class AulaViewModel
    {
        public AulaModel AulaNome { get; set; }
        public List<AulaModel> ListaAulas { get; set; }
    }
}
