using Analise.Enuns;
using System.ComponentModel.DataAnnotations;

namespace Analise.Models
{
    public class SaidaViewModel
    {
        public SaidaModel SaidaNome { get; set; }
        public List<SaidaModel> ListaSaidas { get; set; }

        public ContaModel ContaNome { get; set; }
        public LinhaModel LinhaNome { get; set; }
        public InventarioModel InventarioNome { get; set; }
        public ContasCobrarModel ContaCobrarNome { get; set; }
        public ContasPagarModel ContaPagarNome { get; set; }
        public AgenciaModel AgenciaNome { get; set; }
    }
}
