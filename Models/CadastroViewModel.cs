using Analise.Enuns;
using System.ComponentModel.DataAnnotations;

namespace Analise.Models
{
    public class CadastroViewModel
    {
        public CadastroModel CadastroNome { get; set; }
        public List<CadastroModel> ListaCadastros { get; set; }
    }
}
