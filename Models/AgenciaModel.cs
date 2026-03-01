using Analise.Enuns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Analise.Models
{
    public class AgenciaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do tabernáculo")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o nome do representante")]
        public string Representante { get; set; }

        [Required(ErrorMessage = "Digite o endereço")]
        public string Endereco { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Credito { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Debito { get; set; } = 0;
        [NotMapped]
        public decimal Saldo => Credito - Debito;
        public DateTimeOffset DataCadastro { get; set; }
    }
}
