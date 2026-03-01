using Analise.Enuns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Analise.Models
{
    public class LinhaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome")]
        public string Nome { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Credito { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Debito { get; set; } = 0;
        [NotMapped]
        public decimal Saldo => Credito - Debito;
        public DateTimeOffset DataCadastro { get; set; }
    }
}
