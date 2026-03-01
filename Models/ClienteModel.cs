using Analise.Enuns;
using System.ComponentModel.DataAnnotations;

namespace Analise.Models
{
    public class ClienteModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o nome")]
        public string Nome { get; set; }
        public string Nuit { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Tipo { get; set; }
        public string Pais { get; set; }
        public string Endereco { get; set; }
        public string Pep { get; set; }
        public string DetalhePep { get; set; }
        public string Nascimento { get; set; }
        public string Segmento { get; set; }
        public string Registo { get; set; }
        public string Banco { get; set; }
        public string Conta { get; set; }
        public string NIB { get; set; }
        public string Titular { get; set; }
        
        public decimal Limite { get; set; }
        public decimal Prazo { get; set; }

        public DateTimeOffset DataCadastro { get; set; }
        public DateTimeOffset DataActualizacao { get; set; }
        public int? UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }
        public int? AgenciaId { get; set; }
        public AgenciaModel Agencia { get; set; }
    }
}
