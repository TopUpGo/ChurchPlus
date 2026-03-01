using Analise.Enuns;
using System.ComponentModel.DataAnnotations;

namespace Analise.Models
{
    public class EventoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o evento")]
        public string Nome { get; set; }

        public DateTimeOffset DataCadastro { get; set; }
    }
}
