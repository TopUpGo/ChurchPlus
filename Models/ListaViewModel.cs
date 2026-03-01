using Analise.Enuns;
using System.ComponentModel.DataAnnotations;

namespace Analise.Models
{
    public class ListaViewModel
    {
        public ListaModel ListaNome { get; set; }
        public List<ListaModel> ListaListas { get; set; }
    }
}
