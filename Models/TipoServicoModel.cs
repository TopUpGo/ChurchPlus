using Analise.Enuns;
using System.ComponentModel.DataAnnotations;

namespace Analise.Models
{
    public class TipoServicoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o serviço")]
        public string Nome { get; set; }

        public DateTimeOffset DataCadastro { get; set; }
    }
}
