using Analise.Enuns;
using System.ComponentModel.DataAnnotations;

namespace Analise.Models
{
    public class TipoDespesaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite a despesa")]
        public string Nome { get; set; }

        public DateTimeOffset DataCadastro { get; set; }
    }
}
