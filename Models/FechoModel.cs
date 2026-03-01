using Analise.Enuns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Analise.Models
{
    public class FechoModel
    {
        public int Id { get; set; }
        public DateTimeOffset DataCadastro { get; set; }
        public int? UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }
        public int? AgenciaId { get; set; }
        public AgenciaModel Agencia { get; set; }
        public int? EstadoId { get; set; }
        public EstadoModel Estado { get; set; }

        //Saldo contas

        [Column(TypeName = "decimal(18,2)")]

        public decimal Caixa { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal BCI1 { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal BCI2 { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Mpesa { get; set; } = 0;

        //Saldo linhas

        [Column(TypeName = "decimal(18,2)")]
        public decimal Construcao { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Corrente { get; set; } = 0;

        //Saldo balanço
        [Column(TypeName = "decimal(18,2)")]
        public decimal Inventario { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Cobranca { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Passivo { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Maputo { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Manhica { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Romao { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Mahubo { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Maguaza { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Fevereiro { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Maluana { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Malhampsene { get; set; } = 0;
    }
}
