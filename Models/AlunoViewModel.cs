using Analise.Enuns;
using System.ComponentModel.DataAnnotations;

namespace Analise.Models
{
    public class AlunoViewModel
    {
        public AlunoModel AlunoNome { get; set; }
        public List<AlunoModel> ListaAlunos { get; set; }
    }
}
