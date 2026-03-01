using Analise.Data;
using Analise.Filters;
using Analise.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Analise.Controllers
{
    public class HomeController : Controller
    {
        private readonly BancoContext _context;
        public HomeController(BancoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public IActionResult Index()
        {
            // 🔹 Recupera os valores armazenados na sessão
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            var agenciaId = HttpContext.Session.GetInt32("AgenciaId");
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == usuarioId);

            

            // 🔹 Passa os valores para a ViewBag
            ViewBag.UsuarioId = usuarioId;
            ViewBag.AgenciaId = agenciaId;
            ViewBag.Perfil = usuario?.Perfil;

            var hoje = DateTime.Today;
            var agora = DateTime.Now;
            int mes = agora.Month;
            int ano = agora.Year;

            var fechoHoje = _context.Fechos
              .Where(f => f.AgenciaId == agenciaId && f.DataCadastro.Date == hoje)
              .OrderByDescending(f => f.DataCadastro)
              .FirstOrDefault();

            ViewBag.EstadoDia = fechoHoje?.EstadoId;
            // null = nunca abriu
            // 1 = aberto
            // 2 = fechado


            // Definir início e fim do mês corrente
            var inicioMes = new DateTime(ano, mes, 1);
            var fimMes = inicioMes.AddMonths(1).AddTicks(-1);

            // Total de Linhas
            var linhaConstrucao = _context.Linhas.FirstOrDefault(c => c.Id == 2);
            decimal valorLinhaConstrucao = linhaConstrucao?.Saldo ?? 0;
            ViewBag.TotalLinhaConstrucao = valorLinhaConstrucao.ToString("#,0.00").Replace(",", ".");

            var linhaCorrente = _context.Linhas.FirstOrDefault(c => c.Id == 1);
            decimal valorLinhaCorrente = linhaCorrente?.Saldo ?? 0;
            ViewBag.TotalLinhaCorrente = valorLinhaCorrente.ToString("#,0.00").Replace(",", ".");

            // Total em contas
            var contaCaixa = _context.Contas.FirstOrDefault(c => c.Id == 1);
            decimal valorContaCaixa = contaCaixa?.Saldo ?? 0;
            ViewBag.TotalContaCaixa= valorContaCaixa.ToString("#,0.00").Replace(",", ".");

            var contaBCI1 = _context.Contas.FirstOrDefault(c => c.Id == 2);
            decimal valorContaBCI1 = contaBCI1?.Saldo ?? 0;
            ViewBag.TotalContaBci1 = valorContaBCI1.ToString("#,0.00").Replace(",", ".");

            var contaBCI2 = _context.Contas.FirstOrDefault(c => c.Id == 3);
            decimal valorContaBCI2 = contaBCI2?.Saldo ?? 0;
            ViewBag.TotalContaBci2 = valorContaBCI2.ToString("#,0.00").Replace(",", ".");

            var contaMpesa = _context.Contas.FirstOrDefault(c => c.Id == 4);
            decimal valorContaMpesa = contaMpesa?.Saldo ?? 0;
            ViewBag.TotalContaMpesa = valorContaMpesa.ToString("#,0.00").Replace(",", ".");

            decimal totalContaGeral = valorContaCaixa + valorContaBCI1 + valorContaBCI2;
            ViewBag.TotalContaGeral = totalContaGeral.ToString("#,0.00").Replace(",", ".");

            // Total em tabernaculos
            var tabernaculoMaputo = _context.Agencias.FirstOrDefault(c => c.Id == 1);
            decimal valortabernaculoMaputo = tabernaculoMaputo?.Saldo ?? 0;
            ViewBag.TotaltabernaculoMaputo = valortabernaculoMaputo.ToString("#,0.00").Replace(",", ".");

            var tabernaculoManhica = _context.Agencias.FirstOrDefault(c => c.Id == 2);
            decimal valortabernaculoManhica = tabernaculoManhica?.Saldo ?? 0;
            ViewBag.TotaltabernaculoManhica = valortabernaculoManhica.ToString("#,0.00").Replace(",", ".");

            var tabernaculoRomao = _context.Agencias.FirstOrDefault(c => c.Id == 3);
            decimal valortabernaculoRomao = tabernaculoRomao?.Saldo ?? 0;
            ViewBag.TotaltabernaculoRomao = valortabernaculoRomao.ToString("#,0.00").Replace(",", ".");

            var tabernaculoMahubo = _context.Agencias.FirstOrDefault(c => c.Id == 4);
            decimal valortabernaculoMahubo = tabernaculoMahubo?.Saldo ?? 0;
            ViewBag.TotaltabernaculoMahubo = valortabernaculoMahubo.ToString("#,0.00").Replace(",", ".");

            var tabernaculoMaguaza = _context.Agencias.FirstOrDefault(c => c.Id == 5);
            decimal valortabernaculoMaguaza = tabernaculoMaguaza?.Saldo ?? 0;
            ViewBag.TotaltabernaculoMaguaza = valortabernaculoMaguaza.ToString("#,0.00").Replace(",", ".");

            var tabernaculoFevereiro = _context.Agencias.FirstOrDefault(c => c.Id == 6);
            decimal valortabernaculoFevereiro = tabernaculoFevereiro?.Saldo ?? 0;
            ViewBag.TotaltabernaculoFevereiro = valortabernaculoFevereiro.ToString("#,0.00").Replace(",", ".");

            var tabernaculoMaluana = _context.Agencias.FirstOrDefault(c => c.Id == 7);
            decimal valortabernaculoMaluana = tabernaculoMaluana?.Saldo ?? 0;
            ViewBag.TotaltabernaculoMaluana = valortabernaculoMaluana.ToString("#,0.00").Replace(",", ".");

            var tabernaculoMalhampsene = _context.Agencias.FirstOrDefault(c => c.Id == 8);
            decimal valortabernaculoMalhampsene = tabernaculoMalhampsene?.Saldo ?? 0;
            ViewBag.TotaltabernaculoMalhampsene = valortabernaculoMalhampsene.ToString("#,0.00").Replace(",", ".");

            // Total de Entradas
            // HOJE
            decimal valorOfertaHoje = _context.Entradas.Where(c => c.TipoEntradaId == 1 && c.DataCadastro.Date == DateTime.Today).Sum(c => (decimal?)c.Valor) ?? 0;
            ViewBag.TotalEntradaOfertaHoje = valorOfertaHoje.ToString("#,0.00").Replace(",", ".");

            // MÊS ATUAL
            decimal valorOfertaMes = _context.Entradas.Where(c => c.TipoEntradaId == 1 && c.DataCadastro.Month == DateTime.Today.Month && c.DataCadastro.Year == DateTime.Today.Year).Sum(c => (decimal?)c.Valor) ?? 0;
            ViewBag.TotalEntradaOfertaMes = valorOfertaMes.ToString("#,0.00").Replace(",", ".");

            // ANO ATUAL
            decimal valorOfertaAno = _context.Entradas.Where(c => c.TipoEntradaId == 1 && c.DataCadastro.Year == DateTime.Today.Year).Sum(c => (decimal?)c.Valor) ?? 0;
            ViewBag.TotalEntradaOfertaAno = valorOfertaAno.ToString("#,0.00").Replace(",", ".");

            // GLOBAL (TODOS)
            decimal valorOfertaGlobal = _context.Entradas.Where(c => c.TipoEntradaId == 1).Sum(c => (decimal?)c.Valor) ?? 0;
            ViewBag.TotalEntradaOfertaGlobal = valorOfertaGlobal.ToString("#,0.00").Replace(",", ".");

            // Total de Dizimo
            // HOJE
            decimal valorDizimoHoje = _context.Entradas.Where(c => c.TipoEntradaId == 2 && c.DataCadastro.Date == DateTime.Today).Sum(c => (decimal?)c.Valor) ?? 0;
            ViewBag.TotalEntradaDizimoHoje = valorDizimoHoje.ToString("#,0.00").Replace(",", ".");

            // MÊS ATUAL
            decimal valorDizimoMes = _context.Entradas.Where(c => c.TipoEntradaId == 2 && c.DataCadastro.Month == DateTime.Today.Month && c.DataCadastro.Year == DateTime.Today.Year).Sum(c => (decimal?)c.Valor) ?? 0;
            ViewBag.TotalEntradaDizimoMes = valorDizimoMes.ToString("#,0.00").Replace(",", ".");

            // ANO ATUAL
            decimal valorDizimoAno = _context.Entradas.Where(c => c.TipoEntradaId == 2 && c.DataCadastro.Year == DateTime.Today.Year).Sum(c => (decimal?)c.Valor) ?? 0;
            ViewBag.TotalEntradaDizimoAno = valorDizimoAno.ToString("#,0.00").Replace(",", ".");

            // GLOBAL (TODOS)
            decimal valorDizimoGlobal = _context.Entradas.Where(c => c.TipoEntradaId == 2).Sum(c => (decimal?)c.Valor) ?? 0;
            ViewBag.TotalEntradaDizimoGlobal = valorDizimoGlobal.ToString("#,0.00").Replace(",", ".");

            // Total de Oferta especial
            // HOJE
            decimal valorEspecialHoje = _context.Entradas.Where(c => c.TipoEntradaId == 3 && c.DataCadastro.Date == DateTime.Today).Sum(c => (decimal?)c.Valor) ?? 0;
            ViewBag.TotalEntradaEspecialHoje = valorEspecialHoje.ToString("#,0.00").Replace(",", ".");

            // MÊS ATUAL
            decimal valorEspecialMes = _context.Entradas.Where(c => c.TipoEntradaId == 3 && c.DataCadastro.Month == DateTime.Today.Month && c.DataCadastro.Year == DateTime.Today.Year).Sum(c => (decimal?)c.Valor) ?? 0;
            ViewBag.TotalEntradaEspecialMes = valorEspecialMes.ToString("#,0.00").Replace(",", ".");

            // ANO ATUAL
            decimal valorEspecialAno = _context.Entradas.Where(c => c.TipoEntradaId == 3 && c.DataCadastro.Year == DateTime.Today.Year).Sum(c => (decimal?)c.Valor) ?? 0;
            ViewBag.TotalEntradaEspecialAno = valorEspecialAno.ToString("#,0.00").Replace(",", ".");

            // GLOBAL (TODOS)
            decimal valorEspecialGlobal = _context.Entradas.Where(c => c.TipoEntradaId == 3).Sum(c => (decimal?)c.Valor) ?? 0;
            ViewBag.TotalEntradaEspecialGlobal = valorEspecialGlobal.ToString("#,0.00").Replace(",", ".");

            // Total de Escola dominical
            // HOJE
            decimal valorDominicalHoje = _context.Entradas.Where(c => c.TipoEntradaId == 4 && c.DataCadastro.Date == DateTime.Today).Sum(c => (decimal?)c.Valor) ?? 0;
            ViewBag.TotalEntradaDominicalHoje = valorDominicalHoje.ToString("#,0.00").Replace(",", ".");

            // MÊS ATUAL
            decimal valorDominicalMes = _context.Entradas.Where(c => c.TipoEntradaId == 4 && c.DataCadastro.Month == DateTime.Today.Month && c.DataCadastro.Year == DateTime.Today.Year).Sum(c => (decimal?)c.Valor) ?? 0;
            ViewBag.TotalEntradaDominicalMes = valorDominicalMes.ToString("#,0.00").Replace(",", ".");

            // ANO ATUAL
            decimal valorDominicalAno = _context.Entradas.Where(c => c.TipoEntradaId == 4 && c.DataCadastro.Year == DateTime.Today.Year).Sum(c => (decimal?)c.Valor) ?? 0;
            ViewBag.TotalEntradaDominicalAno = valorDominicalAno.ToString("#,0.00").Replace(",", ".");

            // GLOBAL (TODOS)
            decimal valorDominicalGlobal = _context.Entradas.Where(c => c.TipoEntradaId == 4).Sum(c => (decimal?)c.Valor) ?? 0;
            ViewBag.TotalEntradaDominicalGlobal = valorDominicalGlobal.ToString("#,0.00").Replace(",", ".");

            // Total de Reembolso
            // HOJE
            decimal valorReembolsoHoje = _context.Entradas.Where(c => c.TipoEntradaId == 5 && c.DataCadastro.Date == DateTime.Today).Sum(c => (decimal?)c.Valor) ?? 0;
            ViewBag.TotalEntradaReembolsoHoje = valorReembolsoHoje.ToString("#,0.00").Replace(",", ".");

            // MÊS ATUAL
            decimal valorReembolsoMes = _context.Entradas.Where(c => c.TipoEntradaId == 5 && c.DataCadastro.Month == DateTime.Today.Month && c.DataCadastro.Year == DateTime.Today.Year).Sum(c => (decimal?)c.Valor) ?? 0;
            ViewBag.TotalEntradaReembolsoMes = valorReembolsoMes.ToString("#,0.00").Replace(",", ".");

            // ANO ATUAL
            decimal valorReembolsoAno = _context.Entradas.Where(c => c.TipoEntradaId == 5 && c.DataCadastro.Year == DateTime.Today.Year).Sum(c => (decimal?)c.Valor) ?? 0;
            ViewBag.TotalEntradaReembolsoAno = valorReembolsoAno.ToString("#,0.00").Replace(",", ".");

            // GLOBAL (TODOS)
            decimal valorReembolsoGlobal = _context.Entradas.Where(c => c.TipoEntradaId == 5).Sum(c => (decimal?)c.Valor) ?? 0;
            ViewBag.TotalEntradaReembolsoGlobal = valorReembolsoGlobal.ToString("#,0.00").Replace(",", ".");

            // Total de Construcao
            // HOJE
            decimal valorConstrucaoHoje = _context.Entradas.Where(c => c.LinhaId == 2 && c.DataCadastro.Date == DateTime.Today).Sum(c => (decimal?)c.Valor) ?? 0;
            ViewBag.TotalEntradaConstrucaoHoje = valorConstrucaoHoje.ToString("#,0.00").Replace(",", ".");

            // SEMANA
            DateTime fimSemana = DateTime.Today;          // hoje (exclusivo)
            DateTime inicioSemana = fimSemana.AddDays(-7);      // 7 dias antes
            decimal valorConstrucaoSemana = _context.Entradas.Where(c => c.LinhaId == 2 && c.DataCadastro >= inicioSemana && c.DataCadastro < fimSemana).Sum(c => (decimal?)c.Valor) ?? 0;
            ViewBag.TotalEntradaConstrucaoSemana = valorConstrucaoSemana.ToString("#,0.00").Replace(",", ".");

            // MÊS ATUAL
            decimal valorConstrucaoMes = _context.Entradas.Where(c => c.LinhaId == 2 && c.DataCadastro.Month == DateTime.Today.Month && c.DataCadastro.Year == DateTime.Today.Year).Sum(c => (decimal?)c.Valor) ?? 0;
            ViewBag.TotalEntradaConstrucaoMes = valorConstrucaoMes.ToString("#,0.00").Replace(",", ".");

            // ANO ATUAL
            decimal valorConstrucaoAno = _context.Entradas.Where(c => c.LinhaId == 2 && c.DataCadastro.Year == DateTime.Today.Year).Sum(c => (decimal?)c.Valor) ?? 0;
            ViewBag.TotalEntradaConstrucaoAno = valorConstrucaoAno.ToString("#,0.00").Replace(",", ".");

            // GLOBAL (TODOS)
            decimal valorConstrucaoGlobal = _context.Entradas.Where(c => c.LinhaId == 2).Sum(c => (decimal?)c.Valor) ?? 0;
            ViewBag.TotalEntradaConstrucaoGlobal = valorConstrucaoGlobal.ToString("#,0.00").Replace(",", ".");

            // Entradas Globais
            // HOJE
            decimal valorEntradaHoje = _context.Entradas.Where(c => c.DataCadastro.Date == DateTime.Today).Sum(c => (decimal?)c.Valor) ?? 0;
            ViewBag.TotalEntradaHoje = valorEntradaHoje.ToString("#,0.00").Replace(",", ".");

            // MÊS ATUAL
            decimal valorEntradaMes = _context.Entradas.Where(c => c.DataCadastro.Month == DateTime.Today.Month && c.DataCadastro.Year == DateTime.Today.Year).Sum(c => (decimal?)c.Valor) ?? 0;
            ViewBag.TotalEntradaMes = valorEntradaMes.ToString("#,0.00").Replace(",", ".");

            // ANO ATUAL
            decimal valorEntradaAno = _context.Entradas.Where(c => c.DataCadastro.Year == DateTime.Today.Year).Sum(c => (decimal?)c.Valor) ?? 0;
            ViewBag.TotalEntradaAno = valorEntradaAno.ToString("#,0.00").Replace(",", ".");

            // GLOBAL (TODOS)
            decimal valorEntradaGlobal = _context.Entradas.Sum(c => (decimal?)c.Valor) ?? 0;
            ViewBag.TotalEntradaGlobal = valorEntradaGlobal.ToString("#,0.00").Replace(",", ".");

            
            // Total de Saidas
            // Total de Corrente
            // HOJE
            decimal valorSaidaCorrenteHoje = _context.Saidas.Where(c => c.LinhaId == 1 && c.TipoDespesaId !=28 && c.DataCadastro.Date == DateTime.Today).Sum(c => (decimal?)c.Quantidade * c.Valor) ?? 0;
            ViewBag.TotalSaidaCorrenteHoje = valorSaidaCorrenteHoje.ToString("#,0.00").Replace(",", ".");

            // MÊS ATUAL
            decimal valorSaidaCorrenteMes = _context.Saidas.Where(c => c.LinhaId == 1 && c.TipoDespesaId != 28 && c.DataCadastro.Month == DateTime.Today.Month && c.DataCadastro.Year == DateTime.Today.Year).Sum(c => (decimal?)c.Quantidade * c.Valor) ?? 0;
            ViewBag.TotalSaidaCorrenteMes = valorSaidaCorrenteMes.ToString("#,0.00").Replace(",", ".");

            // ANO ATUAL
            decimal valorSaidaCorrenteAno = _context.Saidas.Where(c => c.LinhaId == 1 && c.TipoDespesaId != 28 && c.DataCadastro.Year == DateTime.Today.Year).Sum(c => (decimal?)c.Quantidade * c.Valor) ?? 0;
            ViewBag.TotalSaidaCorrenteAno = valorSaidaCorrenteAno.ToString("#,0.00").Replace(",", ".");

            // GLOBAL (TODOS)
            decimal valorSaidaCorrenteGlobal = _context.Saidas.Where(c => c.LinhaId == 1 && c.TipoDespesaId != 28).Sum(c => (decimal?)c.Quantidade * c.Valor) ?? 0;
            ViewBag.TotalSaidaCorrenteGlobal = valorSaidaCorrenteGlobal.ToString("#,0.00").Replace(",", ".");

            // Total de Emprestimo
            // HOJE
            decimal valorSaidaEmprestimoHoje = _context.Saidas.Where(c => c.LinhaId == 1 && c.TipoDespesaId == 28 && c.DataCadastro.Date == DateTime.Today).Sum(c => (decimal?)c.Quantidade * c.Valor) ?? 0;
            ViewBag.TotalSaidaEmprestimoHoje = valorSaidaEmprestimoHoje.ToString("#,0.00").Replace(",", ".");

            // MÊS ATUAL
            decimal valorSaidaEmprestimoMes = _context.Saidas.Where(c => c.LinhaId == 1 && c.TipoDespesaId == 28 && c.DataCadastro.Month == DateTime.Today.Month && c.DataCadastro.Year == DateTime.Today.Year).Sum(c => (decimal?)c.Quantidade * c.Valor) ?? 0;
            ViewBag.TotalSaidaEmprestimoMes = valorSaidaEmprestimoMes.ToString("#,0.00").Replace(",", ".");

            // ANO ATUAL
            decimal valorSaidaEmprestimoAno = _context.Saidas.Where(c => c.LinhaId == 1 && c.TipoDespesaId == 28 && c.DataCadastro.Year == DateTime.Today.Year).Sum(c => (decimal?)c.Quantidade * c.Valor) ?? 0;
            ViewBag.TotalSaidaEmprestimoAno = valorSaidaEmprestimoAno.ToString("#,0.00").Replace(",", ".");

            // GLOBAL (TODOS)
            decimal valorSaidaEmprestimoGlobal = _context.Saidas.Where(c => c.LinhaId == 1 && c.TipoDespesaId == 28).Sum(c => (decimal?)c.Quantidade * c.Valor) ?? 0;
            ViewBag.TotalSaidaEmprestimoGlobal = valorSaidaEmprestimoGlobal.ToString("#,0.00").Replace(",", ".");

            // Total de Construcao
            // HOJE
            decimal valorSaidaConstrucaoHoje = _context.Saidas.Where(c => c.LinhaId == 2 && c.DataCadastro.Date == DateTime.Today).Sum(c => (decimal?)c.Quantidade * c.Valor) ?? 0;
            ViewBag.TotalSaidaConstrucaoHoje = valorSaidaConstrucaoHoje.ToString("#,0.00").Replace(",", ".");

            // MÊS ATUAL
            decimal valorSaidaConstrucaoMes = _context.Saidas.Where(c => c.LinhaId == 2 && c.DataCadastro.Month == DateTime.Today.Month && c.DataCadastro.Year == DateTime.Today.Year).Sum(c => (decimal?)c.Quantidade * c.Valor) ?? 0;
            ViewBag.TotalSaidaConstrucaoMes = valorSaidaConstrucaoMes.ToString("#,0.00").Replace(",", ".");

            // ANO ATUAL
            decimal valorSaidaConstrucaoAno = _context.Saidas.Where(c => c.LinhaId == 2 && c.DataCadastro.Year == DateTime.Today.Year).Sum(c => (decimal?)c.Quantidade * c.Valor) ?? 0;
            ViewBag.TotalSaidaConstrucaoAno = valorSaidaConstrucaoAno.ToString("#,0.00").Replace(",", ".");

            // GLOBAL (TODOS)
            decimal valorSaidaConstrucaoGlobal = _context.Saidas.Where(c => c.LinhaId == 2).Sum(c => (decimal?)c.Quantidade * c.Valor) ?? 0;
            ViewBag.TotalSaidaConstrucaoGlobal = valorSaidaConstrucaoGlobal.ToString("#,0.00").Replace(",", ".");

            // Saidas Globais
            // HOJE
            decimal valorSaidaHoje = _context.Saidas.Where(c => c.DataCadastro.Date == DateTime.Today).Sum(c => (decimal?)c.Quantidade * c.Valor) ?? 0;
            ViewBag.TotalSaidaHoje = valorSaidaHoje.ToString("#,0.00").Replace(",", ".");

            // MÊS ATUAL
            decimal valorSaidaMes = _context.Saidas.Where(c => c.DataCadastro.Month == DateTime.Today.Month && c.DataCadastro.Year == DateTime.Today.Year).Sum(c => (decimal?)c.Quantidade * c.Valor) ?? 0;
            ViewBag.TotalSaidaMes = valorSaidaMes.ToString("#,0.00").Replace(",", ".");

            // ANO ATUAL
            decimal valorSaidaAno = _context.Saidas.Where(c => c.DataCadastro.Year == DateTime.Today.Year).Sum(c => (decimal?)c.Quantidade * c.Valor) ?? 0;
            ViewBag.TotalSaidaAno = valorSaidaAno.ToString("#,0.00").Replace(",", ".");

            // GLOBAL (TODOS)
            decimal valorSaidaGlobal = _context.Saidas.Sum(c => (decimal?)c.Quantidade * c.Valor) ?? 0;
            ViewBag.TotalSaidaGlobal = valorSaidaGlobal.ToString("#,0.00").Replace(",", ".");


            // Contas a receber
            decimal valorReceber = _context.CobrarContas.Sum(c => (decimal?)(c.Valor - c.Pago)) ?? 0;
            ViewBag.TotalReceber = valorReceber.ToString("#,0.00").Replace(",", ".");

            // Contas a pagar
            decimal valorPagar = _context.PagarContas.Sum(c => (decimal?)(c.Valor - c.Pago)) ?? 0;
            ViewBag.TotalPagar = valorPagar.ToString("#,0.00").Replace(",", ".");

            // Contas a pagar
            decimal valorInventario = _context.Inventarios.Sum(c => (decimal?)c.ValorActual) ?? 0;
            ViewBag.TotalInventario = valorInventario.ToString("#,0.00").Replace(",", ".");

            //Grafico de entradas
            
            var entradasPorMes = _context.Entradas
                .Where(c => c.DataCadastro.Year == ano)
                .GroupBy(c => c.DataCadastro.Month)
                .Select(g => new
                {
                    Mes = g.Key,
                    Total = g.Sum(x => x.Valor)
                })
                .ToList();

            // Lista final apenas com valores (jan-dez)
            var valoresMes = Enumerable.Range(1, 12)
                .Select(m => entradasPorMes.FirstOrDefault(x => x.Mes == m)?.Total ?? 0)
                .ToList();
            ViewBag.EntradasMesValores = valoresMes;


            //Grafico de saidas
            var saidasPorMes = _context.Saidas
                .Where(c => c.DataCadastro.Year == ano)
                .GroupBy(c => c.DataCadastro.Month)
                .Select(g => new
                {
                    Mes = g.Key,
                    Total = g.Sum(x => x.Quantidade * x.Valor)
                })
                .ToList();

            // Preenche meses que não têm dados
            var valoresSaidasMes = Enumerable.Range(1, 12)
                .Select(m => saidasPorMes.FirstOrDefault(x => x.Mes == m)?.Total ?? 0)
                .ToList();
            ViewBag.SaidasMesValores = valoresSaidasMes;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AbrirDia()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
            var agenciaId = HttpContext.Session.GetInt32("AgenciaId") ?? 0;

            var hoje = DateTime.Today;

            // 🔹 Verificar se já abriu hoje
            bool jaAberto = _context.Fechos
                .Any(f => f.DataCadastro.Date == hoje && f.AgenciaId == agenciaId && f.EstadoId == 1);

            if (jaAberto)
            {
                TempData["Erro"] = "O dia já foi aberto.";
                return RedirectToAction("Index");
            }

            // 🔹 Recalcular valores novamente (NUNCA confiar na View)
            decimal valorContaBCI1 = _context.Contas.FirstOrDefault(c => c.Id == 2)?.Saldo ?? 0;
            decimal valorContaBCI2 = _context.Contas.FirstOrDefault(c => c.Id == 3)?.Saldo ?? 0;
            decimal valorContaCaixa = _context.Contas.FirstOrDefault(c => c.Id == 1)?.Saldo ?? 0;
            decimal valorContaMpesa = _context.Contas.FirstOrDefault(c => c.Id == 4)?.Saldo ?? 0;

            decimal valorReceber = _context.CobrarContas.Sum(c => (decimal?)(c.Valor - c.Pago)) ?? 0;
            decimal valorPagar = _context.PagarContas.Sum(c => (decimal?)(c.Valor - c.Pago)) ?? 0;
            decimal valorInventario = _context.Inventarios.Sum(c => (decimal?)c.ValorActual) ?? 0;

            decimal valorLinhaConstrucao = _context.Linhas.FirstOrDefault(c => c.Id == 2)?.Saldo ?? 0;
            decimal valorLinhaCorrente = _context.Linhas.FirstOrDefault(c => c.Id == 1)?.Saldo ?? 0;

            decimal valortabernaculoMaputo = _context.Agencias.FirstOrDefault(c => c.Id == 1)?.Saldo ?? 0;
            decimal valortabernaculoManhica = _context.Agencias.FirstOrDefault(c => c.Id == 2)?.Saldo ?? 0;
            decimal valortabernaculoRomao = _context.Agencias.FirstOrDefault(c => c.Id == 3)?.Saldo ?? 0;
            decimal valortabernaculoMahubo = _context.Agencias.FirstOrDefault(c => c.Id == 4)?.Saldo ?? 0;
            decimal valortabernaculoMaguaza = _context.Agencias.FirstOrDefault(c => c.Id == 5)?.Saldo ?? 0;
            decimal valortabernaculoFevereiro = _context.Agencias.FirstOrDefault(c => c.Id == 6)?.Saldo ?? 0;
            decimal valortabernaculoMaluana = _context.Agencias.FirstOrDefault(c => c.Id == 7)?.Saldo ?? 0;
            decimal valortabernaculoMalhampsene = _context.Agencias.FirstOrDefault(c => c.Id == 8)?.Saldo ?? 0;

            FechoModel fechoAbrir = new FechoModel()
            {
                BCI1 = valorContaBCI1,
                BCI2 = valorContaBCI2,
                Caixa = valorContaCaixa,
                Mpesa = valorContaMpesa,
                Cobranca = valorReceber,
                Passivo = valorPagar,
                Inventario = valorInventario,
                Construcao = valorLinhaConstrucao,
                Corrente = valorLinhaCorrente,
                Fevereiro = valortabernaculoFevereiro,
                Maguaza = valortabernaculoMaguaza,
                Mahubo = valortabernaculoMahubo,
                Malhampsene = valortabernaculoMalhampsene,
                Maluana = valortabernaculoMaluana,
                Manhica = valortabernaculoManhica,
                Maputo = valortabernaculoMaputo,
                Romao = valortabernaculoRomao,
                EstadoId = 1,
                UsuarioId = usuarioId,
                AgenciaId = agenciaId,
                DataCadastro = DateTime.Now
            };

            _context.Fechos.Add(fechoAbrir);
            _context.SaveChanges();

            TempData["Sucesso"] = "Dia aberto com sucesso!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FecharDia()
        {
            int usuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
            int agenciaId = HttpContext.Session.GetInt32("AgenciaId") ?? 0;
            var hoje = DateTime.Today;

            // 🔒 Verificar se existe dia aberto hoje
            bool diaAbertoHoje = _context.Fechos.Any(f =>
                f.AgenciaId == agenciaId &&
                f.EstadoId == 1 &&
                f.DataCadastro.Date == hoje
            );

            if (!diaAbertoHoje)
            {
                TempData["Erro"] = "Não existe dia aberto para fechar.";
                return RedirectToAction("Index");
            }

            // ⛔ Impedir fecho duplicado
            bool diaJaFechado = _context.Fechos.Any(f =>
                f.AgenciaId == agenciaId &&
                f.EstadoId == 2 &&
                f.DataCadastro.Date == hoje
            );

            if (diaJaFechado)
            {
                TempData["Erro"] = "O dia já foi fechado.";
                return RedirectToAction("Index");
            }

            // 🔄 RECALCULAR TODOS OS VALORES (IGUAL AO ABRIR)
            decimal valorContaBCI1 = _context.Contas.FirstOrDefault(c => c.Id == 2)?.Saldo ?? 0;
            decimal valorContaBCI2 = _context.Contas.FirstOrDefault(c => c.Id == 3)?.Saldo ?? 0;
            decimal valorContaCaixa = _context.Contas.FirstOrDefault(c => c.Id == 1)?.Saldo ?? 0;
            decimal valorContaMpesa = _context.Contas.FirstOrDefault(c => c.Id == 4)?.Saldo ?? 0;

            decimal valorReceber = _context.CobrarContas.Sum(c => (decimal?)(c.Valor - c.Pago)) ?? 0;
            decimal valorPagar = _context.PagarContas.Sum(c => (decimal?)(c.Valor - c.Pago)) ?? 0;
            decimal valorInventario = _context.Inventarios.Sum(c => (decimal?)c.ValorActual) ?? 0;

            decimal valorLinhaConstrucao = _context.Linhas.FirstOrDefault(c => c.Id == 2)?.Saldo ?? 0;
            decimal valorLinhaCorrente = _context.Linhas.FirstOrDefault(c => c.Id == 1)?.Saldo ?? 0;

            decimal valortabernaculoMaputo = _context.Agencias.FirstOrDefault(c => c.Id == 1)?.Saldo ?? 0;
            decimal valortabernaculoManhica = _context.Agencias.FirstOrDefault(c => c.Id == 2)?.Saldo ?? 0;
            decimal valortabernaculoRomao = _context.Agencias.FirstOrDefault(c => c.Id == 3)?.Saldo ?? 0;
            decimal valortabernaculoMahubo = _context.Agencias.FirstOrDefault(c => c.Id == 4)?.Saldo ?? 0;
            decimal valortabernaculoMaguaza = _context.Agencias.FirstOrDefault(c => c.Id == 5)?.Saldo ?? 0;
            decimal valortabernaculoFevereiro = _context.Agencias.FirstOrDefault(c => c.Id == 6)?.Saldo ?? 0;
            decimal valortabernaculoMaluana = _context.Agencias.FirstOrDefault(c => c.Id == 7)?.Saldo ?? 0;
            decimal valortabernaculoMalhampsene = _context.Agencias.FirstOrDefault(c => c.Id == 8)?.Saldo ?? 0;

            // 🧾 CRIAR REGISTO DE FECHO (NOVO)
            FechoModel fechoFechar = new FechoModel()
            {
                BCI1 = valorContaBCI1,
                BCI2 = valorContaBCI2,
                Caixa = valorContaCaixa,
                Mpesa = valorContaMpesa,
                Cobranca = valorReceber,
                Passivo = valorPagar,
                Inventario = valorInventario,
                Construcao = valorLinhaConstrucao,
                Corrente = valorLinhaCorrente,
                Fevereiro = valortabernaculoFevereiro,
                Maguaza = valortabernaculoMaguaza,
                Mahubo = valortabernaculoMahubo,
                Malhampsene = valortabernaculoMalhampsene,
                Maluana = valortabernaculoMaluana,
                Manhica = valortabernaculoManhica,
                Maputo = valortabernaculoMaputo,
                Romao = valortabernaculoRomao,
                EstadoId = 2, // 🔒 FECHADO
                UsuarioId = usuarioId,
                AgenciaId = agenciaId,
                DataCadastro = DateTime.Now
            };

            _context.Fechos.Add(fechoFechar);
            _context.SaveChanges();

            TempData["Sucesso"] = "Dia fechado com sucesso!";
            return RedirectToAction("Index");
        }
    }
}
