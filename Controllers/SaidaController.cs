using Analise.Data;
using Analise.Filters;
using Analise.Helper;
using Analise.Models;
using Analise.Repositorio;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.Globalization;

namespace Analise.Controllers
{
    public class SaidaController : Controller
    {
        private readonly BancoContext _context;
        private readonly ISaidaRepositorio _saidaRepositorio;
        private readonly IContasCobrarRepositorio _contasCobrarRepositorio;
        private readonly IContasPagarRepositorio _contasPagarRepositorio;
        private readonly ICadastroRepositorio _cadastroRepositorio;
        private readonly IAgenciaRepositorio _agenciaRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ITipoDespesaRepositorio _tipoDespesaRepositorio;
        private readonly ILinhaRepositorio _linhaRepositorio;
        private readonly IContaRepositorio _contaRepositorio;
        private readonly IWebHostEnvironment _env;
        public SaidaController(ISaidaRepositorio cargoRepositorio,
                                    ICadastroRepositorio cadastroRepositorio,
                                    IContasCobrarRepositorio contasCobrarRepositorio,
                                    IContasPagarRepositorio contasPagarRepositorio,
                                    ITipoDespesaRepositorio tipoDespesaRepositorio,
                                    IUsuarioRepositorio usuarioRepositorio,
                                    ILinhaRepositorio linhaRepositorio,
                                    IAgenciaRepositorio agenciaRepositorio,
                                    IContaRepositorio contaRepositorio,
                                    BancoContext bancoContext,
                                    IWebHostEnvironment env)
        {
            _saidaRepositorio = cargoRepositorio;
            _contasCobrarRepositorio = contasCobrarRepositorio;
            _contasPagarRepositorio = contasPagarRepositorio;
            _cadastroRepositorio = cadastroRepositorio;
            _tipoDespesaRepositorio = tipoDespesaRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _linhaRepositorio = linhaRepositorio;
            _agenciaRepositorio = agenciaRepositorio;
            _contaRepositorio = contaRepositorio;
            this._context = bancoContext;
            _env = env;
        }

        // GET: Criar
        public IActionResult Criar()
        {
            int usuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
            int agenciaId = HttpContext.Session.GetInt32("AgenciaId") ?? 0;

            var tabernaculos = _agenciaRepositorio.BuscarTodos()
                .Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Nome
                }).ToList();
            var linhas = _linhaRepositorio.BuscarTodos()
                .Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Nome
                }).ToList();
            var contas = _contaRepositorio.BuscarTodos()
                .Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Nome
                }).ToList();
            var cadastros = _cadastroRepositorio.BuscarTodos()
               .Select(m => new SelectListItem
               {
                   Value = m.Id.ToString(),
                   Text = m.Nome
               }).ToList();
            var tipoDespesas = _tipoDespesaRepositorio.BuscarTodos()
              .Select(m => new SelectListItem
              {
                  Value = m.Id.ToString(),
                  Text = m.Nome
              }).ToList();

            var pedidoModel = new SaidaModel
            {
                ListaAgencias = tabernaculos,
                ListaDespesas = tipoDespesas,
                ListaLinhas = linhas,
                ListaContas = contas,
                ListaCadastros = cadastros
            };


            var viewModel = new SaidaViewModel
            {
                SaidaNome = pedidoModel, 
                ListaSaidas = _saidaRepositorio.BuscarTodos()
            };

            return View(viewModel);
        }

        // GET: Editar
        public IActionResult Editar(int id)
        {
            int usuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
            int agenciaId = HttpContext.Session.GetInt32("AgenciaId") ?? 0;

            var cadastroSelecionado = _saidaRepositorio.ListarPorId(id);

            if (cadastroSelecionado == null)
            {
                TempData["MensagemErro"] = "Não encontrado.";
                return RedirectToAction("Criar");
            }

            var viewModel = new SaidaViewModel
            {
                SaidaNome = cadastroSelecionado,
                ListaSaidas = _saidaRepositorio.BuscarTodos()
            };

            return View(viewModel);
        }

        private bool DiaEstaAberto(int agenciaId)
        {
            var hoje = DateTime.Today;

            return _context.Fechos.Any(f =>
                f.AgenciaId == agenciaId &&
                f.EstadoId == 1 &&
                f.DataCadastro.Date == hoje
            );
        }

        [HttpPost]
        public IActionResult Criar(SaidaViewModel saida)
        {
            int usuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
            int agenciaId = HttpContext.Session.GetInt32("AgenciaId") ?? 0;
            try
            {
                // 🔒 BLOQUEIO DO DIA
                if (!DiaEstaAberto(agenciaId))
                {
                    TempData["MensagemErro"] = "O dia está fechado. Não é possível lançar saídas.";
                    return RedirectToAction("Criar");
                }

                if (ModelState.IsValid)
                {
                    saida.ListaSaidas = _saidaRepositorio.BuscarTodos();

                    TempData["MensagemErro"] = "Dados inválidos! Corrija os erros e tente novamente.";
                    return View(saida);
                }

                //Actualizar saldo Agencia
                var agencia = _context.Agencias
                              .FirstOrDefault(a => a.Id == saida.SaidaNome.AgenciaId);

                if (agencia == null)
                {
                    TempData["MensagemErro"] = "Tabernáculo não encontrado!";
                    return RedirectToAction("Criar");
                }

                agencia.Debito += saida.SaidaNome.Total;

                //Actualizar saldo Linha
                var linha = _context.Linhas
                              .FirstOrDefault(a => a.Id == saida.SaidaNome.LinhaId);

                if (linha == null)
                {
                    TempData["MensagemErro"] = "Linha não encontrada!";
                    return RedirectToAction("Criar");
                }

                linha.Debito += saida.SaidaNome.Total;

                //Actualizar saldo Conta
                var conta = _context.Contas
                              .FirstOrDefault(a => a.Id == saida.SaidaNome.ContaId);

                if (conta == null)
                {
                    TempData["MensagemErro"] = "Conta não encontrada!";
                    return RedirectToAction("Criar");
                }

                conta.Debito += saida.SaidaNome.Total;

                //Extrato
                ExtratoModel extrato = new ExtratoModel()
                {
                    AgenciaId = saida.SaidaNome.AgenciaId,
                    Credito = 0,
                    CanalId = saida.SaidaNome.ContaId,
                    LinhaId = saida.SaidaNome.LinhaId,
                    Debito = saida.SaidaNome.Total,
                    UsuarioId = usuarioId,
                    DataCadastro = DateTime.Now,
                    Descricao = saida.SaidaNome.Descricao
                };

                _context.Extratos.Add(extrato);

                if (saida.SaidaNome.TipoDespesaId == 44)
                {
                    ContasCobrarModel contasCobrar = new ContasCobrarModel()
                    {
                        AgenciaId = saida.SaidaNome.AgenciaId,
                        CadastroId = saida.SaidaNome.CadastroId,
                        Valor = saida.SaidaNome.Total,
                        Pago = 0,
                        UsuarioId = usuarioId,
                        DataCadastro = DateTime.Now
                    };
                    _context.CobrarContas.Add(contasCobrar);
                }

                if (saida.SaidaNome.TipoDespesaId == 43)
                {
                    //Actualizar saldo Conta
                    var pagar = _context.PagarContas
                                  .FirstOrDefault(a => a.Id == saida.SaidaNome.CadastroId);

                    if (pagar == null)
                    {
                        TempData["MensagemErro"] = "Dados não encontrados!";
                        return RedirectToAction("Criar");
                    }

                    pagar.Pago += saida.SaidaNome.Total;
                }

                if (saida.SaidaNome.TipoDespesaId == 47)
                {
                    saida.SaidaNome.LinhaId = 2;
                }
                else
                {
                    saida.SaidaNome.LinhaId = 1;
                }
                _context.SaveChanges();

                // ✅ Se chegou aqui → ModelState válido
                saida.SaidaNome.UsuarioId = usuarioId;
                _saidaRepositorio.Adicionar(saida.SaidaNome);

                TempData["MensagemSucesso"] = "Registado com sucesso!";
                return RedirectToAction("Criar");
            }
            catch (Exception erro)
            {
                Console.WriteLine("Erro: " + erro.InnerException?.Message);

                TempData["MensagemErro"] = $"Ops, erro ao registar! Detalhes: {erro.InnerException?.Message ?? erro.Message}";

                saida.ListaSaidas = _saidaRepositorio.BuscarTodos();

                return View(saida);
            }
        }


        // POST: Editar
        [HttpPost]
        public IActionResult Editar(SaidaViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    //viewModel.CadastroNome.UsuarioId = usuarioId;
                    _saidaRepositorio.Actualizar(viewModel.SaidaNome);
                    TempData["MensagemSucesso"] = "Actualizado com sucesso!";
                    return RedirectToAction("Criar");
                }
                viewModel.ListaSaidas = _saidaRepositorio.BuscarTodos();
                return View(viewModel);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, erro ao actualizar. Detalhes: {erro.Message}";
                return RedirectToAction("Criar");
            }
        }

        public IActionResult RelatorioSaidas(
             DateTime? dataInicio,
             DateTime? dataFim,
             int? agenciaId,
             int? tipoId,
             int? cadastroId,
             int? linhaId,
             int? contaId)
        {
            int usuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
            int agenciaSessaoId = HttpContext.Session.GetInt32("AgenciaId") ?? 0;

            var tipoSaidas = _tipoDespesaRepositorio.BuscarTodos()
                 .Select(m => new SelectListItem
                 {
                     Value = m.Id.ToString(),
                     Text = m.Nome,
                     Selected = tipoId.HasValue && m.Id == tipoId
                 }).ToList();

            var tabernaculos = _agenciaRepositorio.BuscarTodos()
                .Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Nome,
                    Selected = agenciaId.HasValue && m.Id == agenciaId
                }).ToList();

            var linhas = _linhaRepositorio.BuscarTodos()
                .Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Nome,
                    Selected = linhaId.HasValue && m.Id == linhaId
                }).ToList();

            var contas = _contaRepositorio.BuscarTodos()
                .Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Nome,
                    Selected = contaId.HasValue && m.Id == contaId
                }).ToList();

            var cadastros = _cadastroRepositorio.BuscarTodos()
                 .Select(m => new SelectListItem
                 {
                     Value = m.Id.ToString(),
                     Text = m.Nome,
                     Selected = cadastroId.HasValue && m.Id == cadastroId
                 }).ToList();

            var saidas = _saidaRepositorio.BuscarTodosRelatorio().AsQueryable();

            // 🔹 FILTRO POR INTERVALO DE DATAS
            if (dataInicio.HasValue)
                saidas = saidas.Where(e => e.DataCadastro >= dataInicio.Value.Date);

            if (dataFim.HasValue)
                saidas = saidas.Where(e => e.DataCadastro < dataFim.Value.Date.AddDays(1));

            if (agenciaId.HasValue)
                saidas = saidas.Where(e => e.AgenciaId == agenciaId);

            if (tipoId.HasValue)
                saidas = saidas.Where(e => e.TipoDespesaId == tipoId);

            if (cadastroId.HasValue)
                saidas = saidas.Where(e => e.CadastroId == cadastroId);

            if (linhaId.HasValue)
                saidas = saidas.Where(e => e.LinhaId == linhaId);

            if (contaId.HasValue)
                saidas = saidas.Where(e => e.ContaId == contaId);

            var pedidoModel = new SaidaModel
            {
                ListaAgencias = tabernaculos,
                ListaDespesas = tipoSaidas,
                ListaLinhas = linhas,
                ListaContas = contas,
                ListaCadastros = cadastros
            };

            var viewModel = new SaidaViewModel
            {
                SaidaNome = pedidoModel,
                ListaSaidas = saidas.ToList()
            };

            return View(viewModel);
        }

        public IActionResult ImprimirSaidas(DateTime? dataInicio,
            DateTime? dataFim,
            int? agenciaId,
            int? tipoId,
            int? cadastroId,
            int? linhaId,
            int? contaId)
        {
            var saidas = _context.Saidas
                .Include(e => e.Agencia)
                .Include(e => e.TipoDespesa)
                .Include(e => e.Cadastro)
                .ToList();
            int usuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;

            var lista = _saidaRepositorio.BuscarTodosRelatorio().AsQueryable();

            if (dataInicio.HasValue)
                lista = lista.Where(e => e.DataCadastro >= dataInicio.Value.Date);

            if (dataFim.HasValue)
                lista = lista.Where(e => e.DataCadastro < dataFim.Value.Date.AddDays(1));

            if (agenciaId.HasValue)
                lista = lista.Where(e => e.AgenciaId == agenciaId);

            if (tipoId.HasValue)
                lista = lista.Where(e => e.TipoDespesaId == tipoId);

            if (cadastroId.HasValue)
                lista = lista.Where(e => e.CadastroId == cadastroId);

            if (linhaId.HasValue)
                lista = lista.Where(e => e.LinhaId == linhaId);

            if (contaId.HasValue)
                lista = lista.Where(e => e.ContaId == contaId);

            var dadosFiltrados = lista.ToList();
            decimal totalGeral = dadosFiltrados.Sum(x => (decimal?)x.Valor) ?? 0;

            using MemoryStream stream = new MemoryStream();

            float pxPorMm = 72 / 25.5f;
            iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.A4,
                15 * pxPorMm, 15 * pxPorMm, 60 * pxPorMm, 25 * pxPorMm); // Margem topo maior para cabeçalho, rodapé

            PdfWriter writer = PdfWriter.GetInstance(doc, stream);

            // Adicionar evento de cabeçalho/rodapé
            string logoPath = Path.Combine(_env.WebRootPath, "images", "logo.png");
            string usuario = "Desconhecido";
            if (usuarioId > 0)
            {
                usuario = _context.Usuarios
                    .Where(u => u.Id == usuarioId)
                    .Select(u => u.Login)
                    .FirstOrDefault() ?? "Desconhecido";
            }
            string empresa = "IGREJA CRENTES DA BÍBLIA EM MOÇAMBIQUE\nTABERNÁCULO DE MAPUTO\nInterdenominacional";
            string escritura = "_____________________________________________________________________________________________________________________________________________________" +
                               "\n\nMALAQUIAS 4:5-6                                                                          LUCAS 17:30                                                                          APOCALIPSE 10:7";
            string titulo = "\nSaídas";

            writer.PageEvent = new PdfHeaderFooter(logoPath, empresa, escritura, titulo, usuario);

            doc.Open();

            // 🔹 RESUMO DOS FILTROS
            iTextSharp.text.Font fonteFiltro =
                FontFactory.GetFont(FontFactory.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            Paragraph filtros = new Paragraph();
            filtros.SpacingAfter = 10;

            string periodo = (dataInicio.HasValue || dataFim.HasValue)
                ? $"Período: {dataInicio:dd/MM/yyyy} a {dataFim:dd/MM/yyyy}"
                : "Período: Todo período";

            string tabernaculo = agenciaId.HasValue
                ? "Tabernáculo: " + _context.Agencias
                    .Where(a => a.Id == agenciaId)
                    .Select(a => a.Nome)
                    .FirstOrDefault()
                : "Tabernáculo: Todos";

            string tipo = tipoId.HasValue
                ? "Tipo: " + _context.TipoEntradas
                    .Where(t => t.Id == tipoId)
                    .Select(t => t.Nome)
                    .FirstOrDefault()
                : "Tipo: Todos";

            string linha = linhaId.HasValue
                ? "Linha: " + _context.Linhas
                    .Where(t => t.Id == linhaId)
                    .Select(t => t.Nome)
                    .FirstOrDefault()
                : "Linha: Todas";

            string conta = contaId.HasValue
                ? "Conta: " + _context.Contas
                    .Where(t => t.Id == contaId)
                    .Select(t => t.Nome)
                    .FirstOrDefault()
                : "Conta: Todas";

            string beneficiario = cadastroId.HasValue
                ? "Beneficiário: " + _context.Cadastros
                    .Where(c => c.Id == cadastroId)
                    .Select(c => c.Nome)
                    .FirstOrDefault()
                : "Beneficiário: Todos";

            filtros.Add(new Phrase(periodo + "\n", fonteFiltro));
            filtros.Add(new Phrase(tabernaculo + " | ", fonteFiltro));
            filtros.Add(new Phrase(tipo + " | ", fonteFiltro));
            filtros.Add(new Phrase(linha + " | ", fonteFiltro));
            filtros.Add(new Phrase(conta + " | ", fonteFiltro));
            filtros.Add(new Phrase(beneficiario + "\n\n", fonteFiltro));

            doc.Add(filtros);

            // Fontes e cores
            BaseColor corCabecalho = new BaseColor(240, 240, 240);
            iTextSharp.text.Font fonteCabecalho = FontFactory.GetFont(FontFactory.HELVETICA, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font fonteDados = FontFactory.GetFont(FontFactory.COURIER, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            BaseColor corLinha = new BaseColor(200, 200, 200);

            // Tabela
            PdfPTable tabela = new PdfPTable(7);
            tabela.WidthPercentage = 100;
            tabela.SetWidths(new float[] { 7f, 10f, 13f, 18f, 30f, 11f, 11f });
            tabela.HeaderRows = 1; // Repete cabeçalho automaticamente

            // Cabeçalhos
            AddHeader(tabela, "Id", fonteCabecalho, corCabecalho);
            AddHeader(tabela, "Tabern.", fonteCabecalho, corCabecalho);
            AddHeader(tabela, "Tipo", fonteCabecalho, corCabecalho);
            AddHeader(tabela, "Beneficiário", fonteCabecalho, corCabecalho);
            AddHeader(tabela, "Descrição", fonteCabecalho, corCabecalho);
            AddHeader(tabela, "Valor", fonteCabecalho, corCabecalho);
            AddHeader(tabela, "Data", fonteCabecalho, corCabecalho);

            // Dados com linhas horizontais finas
            foreach (var item in dadosFiltrados)
            {
                PdfPCell AddCell(string valor, int alinhamento = Element.ALIGN_LEFT)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(valor, fonteDados))
                    {
                        Border = PdfPCell.BOTTOM_BORDER,
                        BorderColor = corLinha,
                        HorizontalAlignment = alinhamento
                    };
                    return cell;
                }

                tabela.AddCell(AddCell(item.Id.ToString()));
                tabela.AddCell(AddCell(item.Agencia?.Nome ?? ""));
                tabela.AddCell(AddCell(item.TipoDespesa?.Nome ?? ""));
                tabela.AddCell(AddCell(item.Cadastro?.Nome ?? ""));
                tabela.AddCell(AddCell(item.Descricao ?? ""));
                tabela.AddCell(AddCell(item.Valor.ToString("N2", new CultureInfo("pt-PT")), Element.ALIGN_RIGHT));
                tabela.AddCell(AddCell(item.DataCadastro.ToString("dd/MM/yyyy"), Element.ALIGN_CENTER));
            }

            Paragraph total = new Paragraph(
               "\nTotal: " + totalGeral.ToString("N2", new CultureInfo("pt-PT")),
               FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10)
           );

            total.Alignment = Element.ALIGN_RIGHT;

            doc.Add(total);
            doc.Add(tabela);
            doc.Close();

            return File(stream.ToArray(), "application/pdf", "Saídas.pdf");
        }
        private void AddHeader(PdfPTable table, string texto, iTextSharp.text.Font fonte, BaseColor cor)
        {
            PdfPCell cell = new PdfPCell(new Phrase(texto, fonte));

            cell.BackgroundColor = cor;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidthBottom = 2;
            cell.BorderWidthTop = 0;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthRight = 0;
            cell.FixedHeight = 18;

            table.AddCell(cell);
        }


        public IActionResult RelatorioSaidaAgrupadoTab(
           DateTime? dataInicio,
           DateTime? dataFim,
           int? agenciaId,
           int? tipoId,
           int? cadastroId,
           int? linhaId,
           int? contaId)
        {
            int usuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;

            // Todas agências (para incluir as que não têm entradas)
            var todasAgencias = _context.Agencias.ToList();

            // Entradas filtradas
            var lista = _saidaRepositorio.BuscarTodosRelatorio().AsQueryable();

            if (dataInicio.HasValue)
                lista = lista.Where(e => e.DataCadastro >= dataInicio.Value.Date);

            if (dataFim.HasValue)
                lista = lista.Where(e => e.DataCadastro < dataFim.Value.Date.AddDays(1));

            if (agenciaId.HasValue)
                lista = lista.Where(e => e.AgenciaId == agenciaId);

            // Agrupar, somar e ordenar
            var saidasAgrupadas = todasAgencias
                .GroupJoin(
                    lista,
                    a => a.Id,
                    e => e.AgenciaId,
                    (a, saidasAgencia) => new SaidaModel
                    {
                        Agencia = new AgenciaModel { Nome = a.Nome },
                        Valor = saidasAgencia.Sum(x => (decimal?)x.Valor) ?? 0
                    }
                )
                .OrderByDescending(x => x.Valor)
                .ToList();

            // Adicionar Total Geral
            var totalGeral = saidasAgrupadas.Sum(x => x.Valor);
            saidasAgrupadas.Add(new SaidaModel
            {
                Agencia = new AgenciaModel { Nome = "Soma de todos" },
                Valor = totalGeral
            });

            var viewModel = new SaidaViewModel
            {
                ListaSaidas = saidasAgrupadas
            };

            return View(viewModel);
        }

        public IActionResult ImprimirSaidaAgrupadoTab(
            DateTime? dataInicio,
            DateTime? dataFim,
            int? agenciaId,
            int? tipoId,
            int? cadastroId,
            int? linhaId,
            int? contaId)
        {
            int usuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
            var todasAgencias = _context.Agencias.ToList();
            var lista = _saidaRepositorio.BuscarTodosRelatorio().AsQueryable();

            if (dataInicio.HasValue)
                lista = lista.Where(e => e.DataCadastro >= dataInicio.Value.Date);

            if (dataFim.HasValue)
                lista = lista.Where(e => e.DataCadastro < dataFim.Value.Date.AddDays(1));

            if (agenciaId.HasValue)
                lista = lista.Where(e => e.AgenciaId == agenciaId);

            var dadosAgrupados = todasAgencias
                .GroupJoin(
                    lista,
                    a => a.Id,
                    e => e.AgenciaId,
                    (a, saidasAgencia) => new
                    {
                        AgenciaNome = a.Nome,
                        ValorTotal = saidasAgencia.Sum(x => (decimal?)x.Valor) ?? 0
                    }
                )
                .OrderByDescending(x => x.ValorTotal)
                .ToList();

            // Adicionar linha Total Geral
            var totalGeral = dadosAgrupados.Sum(x => x.ValorTotal);
            dadosAgrupados.Add(new { AgenciaNome = "Soma de todos", ValorTotal = totalGeral });

            using MemoryStream stream = new MemoryStream();
            float pxPorMm = 72 / 25.5f;
            iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.A4,
                15 * pxPorMm, 15 * pxPorMm, 60 * pxPorMm, 25 * pxPorMm);

            PdfWriter writer = PdfWriter.GetInstance(doc, stream);

            string logoPath = Path.Combine(_env.WebRootPath, "images", "logo.png");
            string usuario = usuarioId > 0
                ? _context.Usuarios.Where(u => u.Id == usuarioId).Select(u => u.Login).FirstOrDefault() ?? "Desconhecido"
                : "Desconhecido";

            string empresa = "IGREJA CRENTES DA BÍBLIA EM MOÇAMBIQUE\nTABERNÁCULO DE MAPUTO\nInterdenominacional";
            string escritura = "_____________________________________________________________________________________________________________________________________________________" +
                               "\n\nMALAQUIAS 4:5-6                                                                          LUCAS 17:30                                                                          APOCALIPSE 10:7";
            string titulo = "\nSaídas agrupadas por Tabernáculo";
            writer.PageEvent = new PdfHeaderFooter(logoPath, empresa, escritura, titulo, usuario);

            doc.Open();

            var fonteFiltro = FontFactory.GetFont(FontFactory.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            Paragraph filtros = new Paragraph { SpacingAfter = 10 };
            string periodo = (dataInicio.HasValue || dataFim.HasValue)
                ? $"Período: {dataInicio:dd/MM/yyyy} a {dataFim:dd/MM/yyyy}"
                : "Período: Todo período";
            filtros.Add(new Phrase(periodo + "\n", fonteFiltro));
            doc.Add(filtros);

            BaseColor corCabecalho = new BaseColor(240, 240, 240);
            BaseColor corLinha = new BaseColor(200, 200, 200);
            var fonteCabecalho = FontFactory.GetFont(FontFactory.HELVETICA, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            var fonteDados = FontFactory.GetFont(FontFactory.COURIER, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            PdfPTable tabela = new PdfPTable(2) { WidthPercentage = 100 };
            tabela.SetWidths(new float[] { 70f, 30f });
            tabela.HeaderRows = 1;
            AddHeader(tabela, "Tabernáculo", fonteCabecalho, corCabecalho);
            AddHeader(tabela, "Valor", fonteCabecalho, corCabecalho);

            foreach (var item in dadosAgrupados)
            {
                PdfPCell AddCell(string valor, int alinhamento = Element.ALIGN_LEFT)
                {
                    return new PdfPCell(new Phrase(valor, fonteDados))
                    {
                        Border = PdfPCell.BOTTOM_BORDER,
                        BorderColor = corLinha,
                        HorizontalAlignment = alinhamento
                    };
                }

                tabela.AddCell(AddCell(item.AgenciaNome));
                tabela.AddCell(AddCell(item.ValorTotal.ToString("N2", new CultureInfo("pt-PT")), Element.ALIGN_RIGHT));
            }

            doc.Add(tabela);
            doc.Close();

            return File(stream.ToArray(), "application/pdf", "SaidaAgrupadoTab.pdf");
        }

        public IActionResult RelatorioSaidaAgrupadoTipo(
          DateTime? dataInicio,
          DateTime? dataFim,
          int? agenciaId,
          int? tipoId,
          int? cadastroId,
          int? linhaId,
          int? contaId)
        {
            int usuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;

            // Todas agências (para incluir as que não têm entradas)
            var todasTipos = _context.TipoDespesas.ToList();

            // Entradas filtradas
            var lista = _saidaRepositorio.BuscarTodosRelatorio().AsQueryable();

            if (dataInicio.HasValue)
                lista = lista.Where(e => e.DataCadastro >= dataInicio.Value.Date);

            if (dataFim.HasValue)
                lista = lista.Where(e => e.DataCadastro < dataFim.Value.Date.AddDays(1));

            if (tipoId.HasValue)
                lista = lista.Where(e => e.TipoDespesaId == tipoId);

            // Agrupar, somar e ordenar
            var saidasAgrupadas = todasTipos
                .GroupJoin(
                    lista,
                    a => a.Id,
                    e => e.TipoDespesaId,
                    (a, saidasTipo) => new SaidaModel
                    {
                        TipoDespesa = new TipoDespesaModel { Nome = a.Nome },
                        Valor = saidasTipo.Sum(x => (decimal?)x.Valor) ?? 0
                    }
                )
                .OrderByDescending(x => x.Valor)
                .ToList();

            // Adicionar Total Geral
            var totalGeral = saidasAgrupadas.Sum(x => x.Valor);
            saidasAgrupadas.Add(new SaidaModel
            {
                TipoDespesa = new TipoDespesaModel { Nome = "Soma de todos" },
                Valor = totalGeral
            });

            var viewModel = new SaidaViewModel
            {
                ListaSaidas = saidasAgrupadas
            };

            return View(viewModel);
        }

        public IActionResult ImprimirSaidaAgrupadoTipo(
            DateTime? dataInicio,
            DateTime? dataFim,
            int? agenciaId,
            int? tipoId,
            int? cadastroId,
            int? linhaId,
            int? contaId)
        {
            int usuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
            var todasTipos = _context.TipoDespesas.ToList();
            var lista = _saidaRepositorio.BuscarTodosRelatorio().AsQueryable();

            if (dataInicio.HasValue)
                lista = lista.Where(e => e.DataCadastro >= dataInicio.Value.Date);

            if (dataFim.HasValue)
                lista = lista.Where(e => e.DataCadastro < dataFim.Value.Date.AddDays(1));

            if (tipoId.HasValue)
                lista = lista.Where(e => e.TipoDespesaId == tipoId);

            var dadosAgrupados = todasTipos
                .GroupJoin(
                    lista,
                    a => a.Id,
                    e => e.TipoDespesaId,
                    (a, saidasTipo) => new
                    {
                        TipoSaida = a.Nome,
                        ValorTotal = saidasTipo.Sum(x => (decimal?)x.Valor) ?? 0
                    }
                )
                .OrderByDescending(x => x.ValorTotal)
                .ToList();

            // Adicionar linha Total Geral
            var totalGeral = dadosAgrupados.Sum(x => x.ValorTotal);
            dadosAgrupados.Add(new { TipoSaida = "Soma de todos", ValorTotal = totalGeral });

            using MemoryStream stream = new MemoryStream();
            float pxPorMm = 72 / 25.5f;
            iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.A4,
                15 * pxPorMm, 15 * pxPorMm, 60 * pxPorMm, 25 * pxPorMm);

            PdfWriter writer = PdfWriter.GetInstance(doc, stream);

            string logoPath = Path.Combine(_env.WebRootPath, "images", "logo.png");
            string usuario = usuarioId > 0
                ? _context.Usuarios.Where(u => u.Id == usuarioId).Select(u => u.Login).FirstOrDefault() ?? "Desconhecido"
                : "Desconhecido";

            string empresa = "IGREJA CRENTES DA BÍBLIA EM MOÇAMBIQUE\nTABERNÁCULO DE MAPUTO\nInterdenominacional";
            string escritura = "_____________________________________________________________________________________________________________________________________________________" +
                               "\n\nMALAQUIAS 4:5-6                                                                          LUCAS 17:30                                                                          APOCALIPSE 10:7";
            string titulo = "\nSaídas agrupadas por Tipo";
            writer.PageEvent = new PdfHeaderFooter(logoPath, empresa, escritura, titulo, usuario);

            doc.Open();

            var fonteFiltro = FontFactory.GetFont(FontFactory.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            Paragraph filtros = new Paragraph { SpacingAfter = 10 };
            string periodo = (dataInicio.HasValue || dataFim.HasValue)
                ? $"Período: {dataInicio:dd/MM/yyyy} a {dataFim:dd/MM/yyyy}"
                : "Período: Todo período";
            filtros.Add(new Phrase(periodo + "\n", fonteFiltro));
            doc.Add(filtros);

            BaseColor corCabecalho = new BaseColor(240, 240, 240);
            BaseColor corLinha = new BaseColor(200, 200, 200);
            var fonteCabecalho = FontFactory.GetFont(FontFactory.HELVETICA, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            var fonteDados = FontFactory.GetFont(FontFactory.COURIER, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            PdfPTable tabela = new PdfPTable(2) { WidthPercentage = 100 };
            tabela.SetWidths(new float[] { 70f, 30f });
            tabela.HeaderRows = 1;
            AddHeader(tabela, "Tipo de saída", fonteCabecalho, corCabecalho);
            AddHeader(tabela, "Valor", fonteCabecalho, corCabecalho);

            foreach (var item in dadosAgrupados)
            {
                PdfPCell AddCell(string valor, int alinhamento = Element.ALIGN_LEFT)
                {
                    return new PdfPCell(new Phrase(valor, fonteDados))
                    {
                        Border = PdfPCell.BOTTOM_BORDER,
                        BorderColor = corLinha,
                        HorizontalAlignment = alinhamento
                    };
                }

                tabela.AddCell(AddCell(item.TipoSaida));
                tabela.AddCell(AddCell(item.ValorTotal.ToString("N2", new CultureInfo("pt-PT")), Element.ALIGN_RIGHT));
            }

            doc.Add(tabela);
            doc.Close();

            return File(stream.ToArray(), "application/pdf", "SaidaAgrupadoTipo.pdf");
        }


        public IActionResult RelatorioSaidaAgrupadoConta(
          DateTime? dataInicio,
          DateTime? dataFim,
          int? agenciaId,
          int? tipoId,
          int? cadastroId,
          int? linhaId,
          int? contaId)
        {
            int usuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;

            // Todas agências (para incluir as que não têm entradas)
            var todasContas = _context.Contas.ToList();

            // Entradas filtradas
            var lista = _saidaRepositorio.BuscarTodosRelatorio().AsQueryable();

            if (dataInicio.HasValue)
                lista = lista.Where(e => e.DataCadastro >= dataInicio.Value.Date);

            if (dataFim.HasValue)
                lista = lista.Where(e => e.DataCadastro < dataFim.Value.Date.AddDays(1));

            if (contaId.HasValue)
                lista = lista.Where(e => e.ContaId == contaId);

            // Agrupar, somar e ordenar
            var saidasAgrupadas = todasContas
                .GroupJoin(
                    lista,
                    a => a.Id,
                    e => e.ContaId,
                    (a, saidasConta) => new SaidaModel
                    {
                        Conta = new ContaModel { Nome = a.Nome },
                        Valor = saidasConta.Sum(x => (decimal?)x.Valor) ?? 0
                    }
                )
                .OrderByDescending(x => x.Valor)
                .ToList();

            // Adicionar Total Geral
            var totalGeral = saidasAgrupadas.Sum(x => x.Valor);
            saidasAgrupadas.Add(new SaidaModel
            {
                Conta = new ContaModel { Nome = "Soma de todos" },
                Valor = totalGeral
            });

            var viewModel = new SaidaViewModel
            {
                ListaSaidas = saidasAgrupadas
            };

            return View(viewModel);
        }

        public IActionResult ImprimirSaidaAgrupadoConta(
            DateTime? dataInicio,
            DateTime? dataFim,
            int? agenciaId,
            int? tipoId,
            int? cadastroId,
            int? linhaId,
            int? contaId)
        {
            int usuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
            var todasContas = _context.Contas.ToList();
            var lista = _saidaRepositorio.BuscarTodosRelatorio().AsQueryable();

            if (dataInicio.HasValue)
                lista = lista.Where(e => e.DataCadastro >= dataInicio.Value.Date);

            if (dataFim.HasValue)
                lista = lista.Where(e => e.DataCadastro < dataFim.Value.Date.AddDays(1));

            if (contaId.HasValue)
                lista = lista.Where(e => e.ContaId == contaId);

            var dadosAgrupados = todasContas
                .GroupJoin(
                    lista,
                    a => a.Id,
                    e => e.ContaId,
                    (a, saidasConta) => new
                    {
                        Conta = a.Nome,
                        ValorTotal = saidasConta.Sum(x => (decimal?)x.Valor) ?? 0
                    }
                )
                .OrderByDescending(x => x.ValorTotal)
                .ToList();

            // Adicionar linha Total Geral
            var totalGeral = dadosAgrupados.Sum(x => x.ValorTotal);
            dadosAgrupados.Add(new { Conta = "Soma de todos", ValorTotal = totalGeral });

            using MemoryStream stream = new MemoryStream();
            float pxPorMm = 72 / 25.5f;
            iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.A4,
                15 * pxPorMm, 15 * pxPorMm, 60 * pxPorMm, 25 * pxPorMm);

            PdfWriter writer = PdfWriter.GetInstance(doc, stream);

            string logoPath = Path.Combine(_env.WebRootPath, "images", "logo.png");
            string usuario = usuarioId > 0
                ? _context.Usuarios.Where(u => u.Id == usuarioId).Select(u => u.Login).FirstOrDefault() ?? "Desconhecido"
                : "Desconhecido";

            string empresa = "IGREJA CRENTES DA BÍBLIA EM MOÇAMBIQUE\nTABERNÁCULO DE MAPUTO\nInterdenominacional";
            string escritura = "_____________________________________________________________________________________________________________________________________________________" +
                               "\n\nMALAQUIAS 4:5-6                                                                          LUCAS 17:30                                                                          APOCALIPSE 10:7";
            string titulo = "\nSaídas agrupadas por Conta";
            writer.PageEvent = new PdfHeaderFooter(logoPath, empresa, escritura, titulo, usuario);

            doc.Open();

            var fonteFiltro = FontFactory.GetFont(FontFactory.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            Paragraph filtros = new Paragraph { SpacingAfter = 10 };
            string periodo = (dataInicio.HasValue || dataFim.HasValue)
                ? $"Período: {dataInicio:dd/MM/yyyy} a {dataFim:dd/MM/yyyy}"
                : "Período: Todo período";
            filtros.Add(new Phrase(periodo + "\n", fonteFiltro));
            doc.Add(filtros);

            BaseColor corCabecalho = new BaseColor(240, 240, 240);
            BaseColor corLinha = new BaseColor(200, 200, 200);
            var fonteCabecalho = FontFactory.GetFont(FontFactory.HELVETICA, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            var fonteDados = FontFactory.GetFont(FontFactory.COURIER, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            PdfPTable tabela = new PdfPTable(2) { WidthPercentage = 100 };
            tabela.SetWidths(new float[] { 70f, 30f });
            tabela.HeaderRows = 1;
            AddHeader(tabela, "Conta", fonteCabecalho, corCabecalho);
            AddHeader(tabela, "Valor", fonteCabecalho, corCabecalho);

            foreach (var item in dadosAgrupados)
            {
                PdfPCell AddCell(string valor, int alinhamento = Element.ALIGN_LEFT)
                {
                    return new PdfPCell(new Phrase(valor, fonteDados))
                    {
                        Border = PdfPCell.BOTTOM_BORDER,
                        BorderColor = corLinha,
                        HorizontalAlignment = alinhamento
                    };
                }

                tabela.AddCell(AddCell(item.Conta));
                tabela.AddCell(AddCell(item.ValorTotal.ToString("N2", new CultureInfo("pt-PT")), Element.ALIGN_RIGHT));
            }

            doc.Add(tabela);
            doc.Close();

            return File(stream.ToArray(), "application/pdf", "SaidaAgrupadoConta.pdf");
        }


        public IActionResult RelatorioSaidaAgrupadoLinha(
          DateTime? dataInicio,
          DateTime? dataFim,
          int? agenciaId,
          int? tipoId,
          int? cadastroId,
          int? linhaId,
          int? contaId)
        {
            int usuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;

            // Todas agências (para incluir as que não têm entradas)
            var todasLinhas = _context.Linhas.ToList();

            // Entradas filtradas
            var lista = _saidaRepositorio.BuscarTodosRelatorio().AsQueryable();

            if (dataInicio.HasValue)
                lista = lista.Where(e => e.DataCadastro >= dataInicio.Value.Date);

            if (dataFim.HasValue)
                lista = lista.Where(e => e.DataCadastro < dataFim.Value.Date.AddDays(1));

            if (linhaId.HasValue)
                lista = lista.Where(e => e.LinhaId == linhaId);

            // Agrupar, somar e ordenar
            var saidasAgrupadas = todasLinhas
                .GroupJoin(
                    lista,
                    a => a.Id,
                    e => e.LinhaId,
                    (a, saidasLinha) => new SaidaModel
                    {
                        Linha = new LinhaModel { Nome = a.Nome },
                        Valor = saidasLinha.Sum(x => (decimal?)x.Valor) ?? 0
                    }
                )
                .OrderByDescending(x => x.Valor)
                .ToList();

            // Adicionar Total Geral
            var totalGeral = saidasAgrupadas.Sum(x => x.Valor);
            saidasAgrupadas.Add(new SaidaModel
            {
                Linha = new LinhaModel { Nome = "Soma de todos" },
                Valor = totalGeral
            });

            var viewModel = new SaidaViewModel
            {
                ListaSaidas = saidasAgrupadas
            };

            return View(viewModel);
        }

        public IActionResult ImprimirSaidaAgrupadoLinha(
            DateTime? dataInicio,
            DateTime? dataFim,
            int? agenciaId,
            int? tipoId,
            int? cadastroId,
            int? linhaId,
            int? contaId)
        {
            int usuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
            var todasLinhas = _context.Linhas.ToList();
            var lista = _saidaRepositorio.BuscarTodosRelatorio().AsQueryable();

            if (dataInicio.HasValue)
                lista = lista.Where(e => e.DataCadastro >= dataInicio.Value.Date);

            if (dataFim.HasValue)
                lista = lista.Where(e => e.DataCadastro < dataFim.Value.Date.AddDays(1));

            if (linhaId.HasValue)
                lista = lista.Where(e => e.LinhaId == linhaId);

            var dadosAgrupados = todasLinhas
                .GroupJoin(
                    lista,
                    a => a.Id,
                    e => e.LinhaId,
                    (a, saidasLinha) => new
                    {
                        Linha = a.Nome,
                        ValorTotal = saidasLinha.Sum(x => (decimal?)x.Valor) ?? 0
                    }
                )
                .OrderByDescending(x => x.ValorTotal)
                .ToList();

            // Adicionar linha Total Geral
            var totalGeral = dadosAgrupados.Sum(x => x.ValorTotal);
            dadosAgrupados.Add(new { Linha = "Soma de todos", ValorTotal = totalGeral });

            using MemoryStream stream = new MemoryStream();
            float pxPorMm = 72 / 25.5f;
            iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.A4,
                15 * pxPorMm, 15 * pxPorMm, 60 * pxPorMm, 25 * pxPorMm);

            PdfWriter writer = PdfWriter.GetInstance(doc, stream);

            string logoPath = Path.Combine(_env.WebRootPath, "images", "logo.png");
            string usuario = usuarioId > 0
                ? _context.Usuarios.Where(u => u.Id == usuarioId).Select(u => u.Login).FirstOrDefault() ?? "Desconhecido"
                : "Desconhecido";

            string empresa = "IGREJA CRENTES DA BÍBLIA EM MOÇAMBIQUE\nTABERNÁCULO DE MAPUTO\nInterdenominacional";
            string escritura = "_____________________________________________________________________________________________________________________________________________________" +
                               "\n\nMALAQUIAS 4:5-6                                                                          LUCAS 17:30                                                                          APOCALIPSE 10:7";
            string titulo = "\nSaídas agrupadas por Linha";
            writer.PageEvent = new PdfHeaderFooter(logoPath, empresa, escritura, titulo, usuario);

            doc.Open();

            var fonteFiltro = FontFactory.GetFont(FontFactory.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            Paragraph filtros = new Paragraph { SpacingAfter = 10 };
            string periodo = (dataInicio.HasValue || dataFim.HasValue)
                ? $"Período: {dataInicio:dd/MM/yyyy} a {dataFim:dd/MM/yyyy}"
                : "Período: Todo período";
            filtros.Add(new Phrase(periodo + "\n", fonteFiltro));
            doc.Add(filtros);

            BaseColor corCabecalho = new BaseColor(240, 240, 240);
            BaseColor corLinha = new BaseColor(200, 200, 200);
            var fonteCabecalho = FontFactory.GetFont(FontFactory.HELVETICA, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            var fonteDados = FontFactory.GetFont(FontFactory.COURIER, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            PdfPTable tabela = new PdfPTable(2) { WidthPercentage = 100 };
            tabela.SetWidths(new float[] { 70f, 30f });
            tabela.HeaderRows = 1;
            AddHeader(tabela, "Linha", fonteCabecalho, corCabecalho);
            AddHeader(tabela, "Valor", fonteCabecalho, corCabecalho);

            foreach (var item in dadosAgrupados)
            {
                PdfPCell AddCell(string valor, int alinhamento = Element.ALIGN_LEFT)
                {
                    return new PdfPCell(new Phrase(valor, fonteDados))
                    {
                        Border = PdfPCell.BOTTOM_BORDER,
                        BorderColor = corLinha,
                        HorizontalAlignment = alinhamento
                    };
                }

                tabela.AddCell(AddCell(item.Linha));
                tabela.AddCell(AddCell(item.ValorTotal.ToString("N2", new CultureInfo("pt-PT")), Element.ALIGN_RIGHT));
            }

            doc.Add(tabela);
            doc.Close();

            return File(stream.ToArray(), "application/pdf", "SaidaAgrupadoLinha.pdf");
        }
    }
}
