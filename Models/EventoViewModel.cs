using Analise.Enuns;
using System.ComponentModel.DataAnnotations;

namespace Analise.Models
{
    public class EventoViewModel
    {
        public EventoModel EventoNome { get; set; }
        public List<EventoModel> ListaEventos { get; set; }
    }
}
