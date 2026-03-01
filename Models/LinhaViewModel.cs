using Analise.Enuns;
using System.ComponentModel.DataAnnotations;

namespace Analise.Models
{
    public class LinhaViewModel
    {
        public LinhaModel LinhaNome { get; set; }
        public List<LinhaModel> ListaLinhas { get; set; }
    }
}
