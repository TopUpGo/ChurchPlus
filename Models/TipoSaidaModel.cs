using Analise.Enuns;
using System.ComponentModel.DataAnnotations;

namespace Analise.Models
{
    public class TipoSaidaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite a saída")]
        public string Nome { get; set; }

        public DateTimeOffset DataCadastro { get; set; }
    }
}
