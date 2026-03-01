using Analise.Enuns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Analise.Models
{
    public class ExtratoModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Credito { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Debito { get; set; } = 0;
        public int? CanalId { get; set; }
        public ContaModel Canal { get; set; }
        public int? LinhaId { get; set; }
        public LinhaModel Linha { get; set; }

        public DateTimeOffset DataCadastro { get; set; }
        
        public int? UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }
        public int? AgenciaId { get; set; }
        public AgenciaModel Agencia { get; set; }
    }
}
