using Analise.Enuns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Analise.Models
{
    public class RecebimentoModel
    {
        public int Id { get; set; }
        public int? ClienteId { get; set; }
        [ForeignKey("ClienteId")] // 🔹 Define explicitamente a chave estrangeira
        public ClienteModel Cliente { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataActualizacao { get; set; }
        
        public int? UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }
        public int? AgenciaId { get; set; }
        public AgenciaModel Agencia { get; set; }
    }
}
