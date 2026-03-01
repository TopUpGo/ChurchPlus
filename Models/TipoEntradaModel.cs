using Analise.Enuns;
using System.ComponentModel.DataAnnotations;

namespace Analise.Models
{
    public class TipoEntradaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite a entrada")]
        public string Nome { get; set; }

        public DateTimeOffset DataCadastro { get; set; }
    }
}
