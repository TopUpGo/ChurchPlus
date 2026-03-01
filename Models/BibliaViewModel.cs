using Analise.Enuns;
using System.ComponentModel.DataAnnotations;

namespace Analise.Models
{
    public class BibliaViewModel
    {
        public BibliaModel BibliaNome { get; set; }
        public List<BibliaModel> ListaBiblias { get; set; }
    }
}
