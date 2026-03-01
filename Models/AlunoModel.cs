using Analise.Enuns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Analise.Models
{
    public class AlunoModel
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        [DataType(DataType.Date)]
        public DateTimeOffset? Nascimento { get; set; }

        [StringLength(50)]
        public string? Documento { get; set; }

        [StringLength(50)]
        public string? Contacto { get; set; }

        [StringLength(100)]
        public string? Pai { get; set; }

        [StringLength(100)]
        public string? Mae { get; set; }
        [NotMapped]
        public int Idade { get; set; }
        public DateTimeOffset DataCadastro { get; set; }
        public int? EstadoId { get; set; }
        public EstadoModel Estado { get; set; }
        public int? UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }
    }
}
