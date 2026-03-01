using Analise.Enuns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Analise.Models
{
    public class InventarioModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Custo { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorActual { get; set; } = 0;
       
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset? AnoAquisicao { get; set; }
        public int? TabernaculoId { get; set; }
        public AgenciaModel Agencia { get; set; }
        public DateTimeOffset DataCadastro { get; set; }
        public int? UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }
    }
}
