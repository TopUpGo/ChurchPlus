using Analise.Enuns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Analise.Models
{
    public class InventarioViewModel
    {
        public InventarioModel InventarioNome { get; set; }
        public List<InventarioModel> ListaInventarios { get; set; }
    }
}
