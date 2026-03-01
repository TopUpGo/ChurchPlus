using Analise.Enuns;
using System.ComponentModel.DataAnnotations;

namespace Analise.Models
{
    public class FechoViewModel
    {
        public FechoModel FechoNome { get; set; }
        public FechoModel FechoFinal { get; set; }
        public List<FechoModel> ListaFechos { get; set; }

        public EntradaModel EntradaNome { get; set; }
        public List<EntradaModel> ListaEntradas { get; set; }

        public SaidaModel SaidaNome { get; set; }
        public List<SaidaModel> ListaSaidas { get; set; }

        public decimal TotalEntradas { get; set; }
        public decimal TotalSaidas { get; set; }
    }
}
