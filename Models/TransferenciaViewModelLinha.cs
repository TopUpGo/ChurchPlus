using Analise.Enuns;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Analise.Models
{
    public class TransferenciaViewModelLinha
    {
        public int ContaOrigemId { get; set; }
        public int ContaDestinoId { get; set; }
        public decimal Valor { get; set; }
        public string? Observacao { get; set; }

        public List<SelectListItem>? ListaLinhas { get; set; }
    }
}
