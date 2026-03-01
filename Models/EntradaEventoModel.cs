using Analise.Enuns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Analise.Models
{
    public class EntradaEventoModel
    {
        public int Id { get; set; }
        public int? EventoId { get; set; }
        public EventoModel Evento { get; set; }

        public int? CadastroId { get; set; }
        public CadastroModel Cadastro { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; } = 0;
        public DateTime DataCadastro { get; set; }
        public int? UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }
    }
}
