using Analise.Enuns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Analise.Models
{
    public class ContasCobrarModel
    {
        public int Id { get; set; }

        [NotMapped]
        public string Nome { get; set; }
        public int? CadastroId { get; set; }
        public CadastroModel Cadastro { get; set; }

        public int? AgenciaId { get; set; }
        public AgenciaModel Agencia { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Pago { get; set; } = 0;
        [NotMapped]
        public decimal Vigente => Valor - Pago;
        public DateTimeOffset DataCadastro { get; set; }
        public int? UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }
    }
}
