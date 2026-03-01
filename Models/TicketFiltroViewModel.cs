using Analise.Enuns;
using System.ComponentModel.DataAnnotations;

namespace Analise.Models
{
    public class TicketFiltroViewModel
    {
        public string Estado { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

        public List<TicketModel> Tickets { get; set; }
    }
}
