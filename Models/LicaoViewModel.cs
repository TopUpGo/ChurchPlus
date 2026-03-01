using Analise.Enuns;
using System.ComponentModel.DataAnnotations;

namespace Analise.Models
{
    public class LicaoViewModel
    {
        public LicaoModel LicaoNome { get; set; }
        public List<LicaoModel> ListaLicaos { get; set; }
    }
}
