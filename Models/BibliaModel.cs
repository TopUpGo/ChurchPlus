using Analise.Enuns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Analise.Models
{
    public class BibliaModel
    {
        public int Id { get; set; }
        public string? Livro { get; set; }
        public int? TurmaId { get; set; }
        public TurmaModel Turma { get; set; }
        public string? Tipo { get; set; }
        public DateTimeOffset DataCadastro { get; set; }
        public int? UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }
    }
}
