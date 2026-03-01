using Analise.Enuns;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Analise.Models
{
    public class CadastroModel
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Digite o nome")]
        public string Nome { get; set; }

        [DataType(DataType.Date)]
        public DateTimeOffset? Nascimento { get; set; }
        public string? Documento { get; set; }
        public string? Contacto { get; set; }

        public string? Contribuinte { get; set; }

        public string? Obreiro { get; set; }
        public string? Cliente { get; set; }
        public string? Fornecedor { get; set; }
        public string? Professor { get; set; }

        public DateTimeOffset DataCadastro { get; set; } = DateTime.Now;
    }
}
