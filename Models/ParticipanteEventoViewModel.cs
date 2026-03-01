using Analise.Enuns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Analise.Models
{
    public class ParticipanteEventoViewModel
    {
        public ParticipanteEventoModel ParticipanteEventoNome { get; set; }
        public List<ParticipanteEventoModel> ListaParticipanteEventos { get; set; }
    }
}
