using Analise.Enuns;
using System.ComponentModel.DataAnnotations;

namespace Analise.Models
{
    public class TurmaViewModel
    {
        public TurmaModel TurmaNome { get; set; }
        public List<TurmaModel> ListaTurmas { get; set; }
    }
}
