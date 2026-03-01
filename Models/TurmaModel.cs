using Analise.Enuns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Analise.Models
{
    public class TurmaModel
    {
        public int Id { get; set; }

        public string? Nome { get; set; }
        public int? Professor1 { get; set; }
        public CadastroModel Cadastro1 { get; set; }
        public int? Professor2 { get; set; }
        public CadastroModel Cadastro2 { get; set; }
        public int? Professor3 { get; set; }
        public CadastroModel Cadastro3 { get; set; }
        public DateTimeOffset DataCadastro { get; set; }
        public int? AgenciaId { get; set; }
        public AgenciaModel Agencia { get; set; }
        public int? UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }
    }
}
