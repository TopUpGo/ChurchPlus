using Analise.Enuns;
using System.ComponentModel.DataAnnotations;

namespace Analise.Models
{
    public class CanalModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome")]
        public string Nome { get; set; }
        public string Conta { get; set; }
        public string Nib { get; set; }
        public string Titular { get; set; }
        public string Banco { get; set; }
        public DateTimeOffset DataCadastro { get; set; }
        public DateTimeOffset DataActualizacao { get; set; }
        public int? UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }
    }
}
