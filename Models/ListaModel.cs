using Analise.Enuns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Analise.Models
{
    public class ListaModel
    {
        public int Id { get; set; }
        public int? AlunoId { get; set; }
        public AlunoModel Aluno { get; set; }
        public int? TurmaId { get; set; }
        public TurmaModel Turma { get; set; }
        public DateTimeOffset DataCadastro { get; set; }
        public decimal? Presenca { get; set; }
        public decimal? Falta { get; set; }
        public string? Classificacao { get; set; }
        public int? EstadoId { get; set; }
        public EstadoModel Estado { get; set; }
        public int? UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }
    }
}
