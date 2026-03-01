using Analise.Enuns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Analise.Models
{
    public class ParticipanteEventoModel
    {
        public int Id { get; set; }
        public int? EventoId { get; set; }
        public EventoModel Evento { get; set; }

        public int? CadastroId { get; set; }
        public CadastroModel Cadastro { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Pago { get; set; } = 0;
        [NotMapped]
        public decimal Divida => Valor - Pago;
        public DateTime DataCadastro { get; set; }
        public int? UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }
    }
}
