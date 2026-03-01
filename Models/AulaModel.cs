using Analise.Enuns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Analise.Models
{
    public class AulaModel
    {
        public int Id { get; set; }
        public int? AlunoId { get; set; }
        public AlunoModel Aluno { get; set; }
        public int? LicaoId { get; set; }
        public LicaoModel Licao { get; set; }
        public int? TurmaId { get; set; }
        public TurmaModel Turma { get; set; }
        public DateTimeOffset DataCadastro { get; set; }
        public decimal? Presenca { get; set; }
        public int? Professor { get; set; }
        public CadastroModel Cadastro { get; set; }
        public int? Adjunto1 { get; set; }
        public CadastroModel Cadastro1 { get; set; }
        public int? Adjunto2 { get; set; }
        public CadastroModel Cadastro2 { get; set; }
        public int? UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }
    }
}
