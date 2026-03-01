using Analise.Enuns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Analise.Models
{
    public class EntradaEventoViewModel
    {
        public EntradaEventoModel EntradaEventoNome { get; set; }
        public List<EntradaEventoModel> ListaEntradaEventos { get; set; }
    }
}
