using Analise.Data;
using Analise.Filters;
using Analise.Helper;
using Analise.Models;
using Analise.Repositorio;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.Globalization;
using System.IO;
using System.Reflection.Metadata;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Analise.Controllers
{
    public class FechoController : Controller
    {
        private readonly BancoContext _context;
        private readonly IContasCobrarRepositorio _contasCobrarRepositorio;
        private readonly ISaidaRepositorio _saidaRepositorio;
        private readonly IFechoRepositorio _fechoRepositorio;
        private readonly IContasPagarRepositorio _contasPagarRepositorio;
        private readonly IEntradaRepositorio _entradaRepositorio;
        private readonly ICadastroRepositorio _cadastroRepositorio;
        private readonly IAgenciaRepositorio _agenciaRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ITipoEntradaRepositorio _tipoEntradaRepositorio;
        private readonly ILinhaRepositorio _linhaRepositorio;
        private readonly IContaRepositorio _contaRepositorio;
        private readonly IWebHostEnvironment _env;
        public FechoController(IEntradaRepositorio cargoRepositorio,
                                    IContasCobrarRepositorio contasCobrarRepositorio,
                                    IContasPagarRepositorio contasPagarRepositorio,
                                    ISaidaRepositorio saidaRepositorio,
                                    IFechoRepositorio fechoRepositorio,
                                    ICadastroRepositorio cadastroRepositorio,
                                    ITipoEntradaRepositorio tipoEntradaRepositorio,
                                    IUsuarioRepositorio usuarioRepositorio,
                                    ILinhaRepositorio linhaRepositorio,
                                    IAgenciaRepositorio agenciaRepositorio,
                                    IContaRepositorio contaRepositorio,
                                    BancoContext bancoContext,
                                    IWebHostEnvironment env)
        {
            _entradaRepositorio = cargoRepositorio;
            _contasCobrarRepositorio = contasCobrarRepositorio;
            _contasPagarRepositorio = contasPagarRepositorio;
            _fechoRepositorio = fechoRepositorio;
            _saidaRepositorio = saidaRepositorio;
            _cadastroRepositorio = cadastroRepositorio;
            _tipoEntradaRepositorio = tipoEntradaRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _linhaRepositorio = linhaRepositorio;
            _agenciaRepositorio = agenciaRepositorio;
            _contaRepositorio = contaRepositorio;
            this._context = bancoContext;
            _env = env;
        }

        public IActionResult Relatorio(int? id, DateTime? dataInicio)
        {
            var dataReferencia = dataInicio ?? DateTime.Today;

            // 🔹 ABERTURA (Estado 1)
            var abertura = _context.Fechos
                .FirstOrDefault(f => f.DataCadastro.Date == dataReferencia.Date
                                  && f.EstadoId == 1);

            // 🔹 FECHO (Estado 2)
            var fecho = _context.Fechos
                .FirstOrDefault(f => f.DataCadastro.Date == dataReferencia.Date
                                  && f.EstadoId == 2);

            // 🔹 Se não existir abertura
            if (abertura == null)
            {
                abertura = new FechoModel
                {
                    DataCadastro = dataReferencia,
                    EstadoId = 1,
                    Caixa = 0,
                    BCI1 = 0,
                    BCI2 = 0,
                    Mpesa = 0,
                    Corrente = 0,
                    Construcao = 0,
                    Inventario = 0,
                    Cobranca = 0,
                    Passivo = 0
                };
            }

            // 🔹 Se não existir fecho
            if (fecho == null)
            {
                fecho = new FechoModel
                {
                    DataCadastro = dataReferencia,
                    EstadoId = 2
                };
            }

            // 🔹 Entradas
            var entradas = _context.Entradas
                .Include(e => e.Agencia)
                .Include(e => e.TipoEntrada)
                .Include(e => e.Cadastro)
                .Where(e => e.DataCadastro.Date == dataReferencia.Date)
                .ToList();

            // 🔹 Saídas
            var saidas = _context.Saidas
                .Include(s => s.Agencia)
                .Include(s => s.TipoDespesa)
                .Include(s => s.Cadastro)
                .Where(s => s.DataCadastro.Date == dataReferencia.Date)
                .ToList();

            decimal totalEntradas = entradas.Sum(e => e.Valor);
            decimal totalSaidas = saidas.Sum(s => s.Total);

            var viewModel = new FechoViewModel
            {
                FechoNome = abertura,
                FechoFinal = fecho,
                ListaEntradas = entradas,
                ListaSaidas = saidas,
                TotalEntradas = totalEntradas,
                TotalSaidas = totalSaidas
            };

            return View(viewModel);
        }

        public IActionResult ImprimirDiario(DateTime data)
        {
            int usuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;

            var abertura = _context.Fechos
                .FirstOrDefault(f => f.DataCadastro.Date == data.Date && f.EstadoId == 1);

            var fecho = _context.Fechos
                .FirstOrDefault(f => f.DataCadastro.Date == data.Date && f.EstadoId == 2);

            var entradas = _context.Entradas
                .Include(e => e.Agencia)
                .Include(e => e.TipoEntrada)
                .Where(e => e.DataCadastro.Date == data.Date)
                .ToList();

            var saidas = _context.Saidas
                .Include(s => s.Agencia)
                .Include(s => s.TipoDespesa)
                .Where(s => s.DataCadastro.Date == data.Date)
                .ToList();

            //totais

            decimal totalAbertura =
                (abertura?.Caixa ?? 0) +
                (abertura?.BCI1 ?? 0) +
                (abertura?.BCI2 ?? 0) +
                (abertura?.Mpesa ?? 0);

            decimal totalFecho =
                (fecho?.Caixa ?? 0) +
                (fecho?.BCI1 ?? 0) +
                (fecho?.BCI2 ?? 0) +
                (fecho?.Mpesa ?? 0);
            //Saldos de abertura
            string aberturaCaixa = "Saldo de abertura em caixa: " + (abertura?.Caixa ?? 0).ToString("N2");
            string aberturaBCI1 = "Saldo de abertura no BCI 1: " + (abertura?.BCI1 ?? 0).ToString("N2");
            string aberturaBCI2 = "Saldo de abertura no BCI 2: " + (abertura?.BCI2 ?? 0).ToString("N2");
            string aberturaMpesa = "Saldo de abertura no M-Pesa: " + (abertura?.Mpesa ?? 0).ToString("N2");
            string totalAberturaTexto = "Total de Abertura: " + totalAbertura.ToString("N2");
            string aberturaMaputo = "Saldo de abertura em Maputo: " + (abertura?.Maputo ?? 0).ToString("N2");
            string aberturaManhica = "Saldo de abertura em Manhiça: " + (abertura?.Manhica ?? 0).ToString("N2");
            string aberturaRomao = "Saldo de abertura em Romão: " + (abertura?.Romao ?? 0).ToString("N2");
            string aberturaMahubo = "Saldo de abertura em Mahubo: " + (abertura?.Mahubo ?? 0).ToString("N2");
            string aberturaMaguaza = "Saldo de abertura em Maguaza: " + (abertura?.Maguaza ?? 0).ToString("N2");
            string aberturaFevereiro = "Saldo de abertura em 3 de Fevereiro: " + (abertura?.Fevereiro ?? 0).ToString("N2");
            string aberturaMaluana = "Saldo de abertura em Maluana: " + (abertura?.Maluana ?? 0).ToString("N2");
            string aberturaMalhampsene = "Saldo de abertura em Malhampsene: " + (abertura?.Malhampsene ?? 0).ToString("N2");
            string aberturaCorrente = "Saldo de abertura conta Corrente: " + (abertura?.Corrente ?? 0).ToString("N2");
            string aberturaConstrucao = "Saldo de abertura conta Construção: " + (abertura?.Construcao ?? 0).ToString("N2");
            string aberturaInventario = "Inventário da abertura: " + (abertura?.Inventario ?? 0).ToString("N2");
            string aberturaCobranca = "Cobrança da abertura: " + (abertura?.Cobranca ?? 0).ToString("N2");
            string aberturaPassivo = "Passivo da abertura: " + (abertura?.Passivo ?? 0).ToString("N2");

            //Saldos de fecho
            string fechoCaixa = "Saldo de fecho em caixa: " + (fecho?.Caixa ?? 0).ToString("N2");
            string fechoBCI1 = "Saldo de fecho no BCI 1: " + (fecho?.BCI1 ?? 0).ToString("N2");
            string fechoBCI2 = "Saldo de fecho no BCI 2: " + (fecho?.BCI2 ?? 0).ToString("N2");
            string fechoMpesa = "Saldo de fecho no M-Pesa: " + (fecho?.Mpesa ?? 0).ToString("N2");
            string totalFechoTexto = "Total de Fecho: " + totalFecho.ToString("N2");
            string fechoMaputo = "Saldo de fecho em Maputo: " + (fecho?.Maputo ?? 0).ToString("N2");
            string fechoManhica = "Saldo de fecho em Manhiça: " + (fecho?.Manhica ?? 0).ToString("N2");
            string fechoRomao = "Saldo de fecho em Romão: " + (fecho?.Romao ?? 0).ToString("N2");
            string fechoMahubo = "Saldo de fecho em Mahubo: " + (fecho?.Mahubo ?? 0).ToString("N2");
            string fechoMaguaza = "Saldo de fecho em Maguaza: " + (fecho?.Maguaza ?? 0).ToString("N2");
            string fechoFevereiro = "Saldo de fecho em 3 de Fevereiro: " + (fecho?.Fevereiro ?? 0).ToString("N2");
            string fechoMaluana = "Saldo de fecho em Maluana: " + (fecho?.Maluana ?? 0).ToString("N2");
            string fechoMalhampsene = "Saldo de fecho em Malhampsene: " + (fecho?.Malhampsene ?? 0).ToString("N2");
            string fechoCorrente = "Saldo de fecho conta Corrente: " + (fecho?.Corrente ?? 0).ToString("N2");
            string fechoConstrucao = "Saldo de fecho conta Construção: " + (fecho?.Construcao ?? 0).ToString("N2");
            string fechoInventario = "Inventário do fecho: " + (fecho?.Inventario ?? 0).ToString("N2");
            string fechoCobranca = "Cobrança do fecho: " + (fecho?.Cobranca ?? 0).ToString("N2");
            string fechoPassivo = "Passivo do fecho: " + (fecho?.Passivo ?? 0).ToString("N2");

            //Totais
            decimal somaEntradas = entradas.Sum(e => e.Valor);
            decimal somaSaidas = saidas.Sum(s => s.Total);

            string totalEntradasTexto = "Total entradas: " + somaEntradas.ToString("N2");
            string totalSaidasTexto = "Total saídas: " + somaSaidas.ToString("N2");

            string tituloEntradas = "Entradas";
            string tituloSaida = "Saídas";

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
            string titulo = "\nDiário: " + data.ToString("dd/MM/yyyy");

            writer.PageEvent = new PdfHeaderFooter(logoPath, empresa, escritura, titulo, usuario);

            doc.Open();

            // 🔹 RESUMO DOS FILTROS
            iTextSharp.text.Font fonteFiltro =
                FontFactory.GetFont(FontFactory.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            // 🔹 RESUMO DOS FILTROS NEGRITO
            iTextSharp.text.Font fonteFiltroNegrito =
                FontFactory.GetFont(FontFactory.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            Paragraph filtrosAbertura = new Paragraph();
            filtrosAbertura.SpacingAfter = 10;

            Paragraph filtrosFecho = new Paragraph();
            filtrosFecho.SpacingAfter = 10;

            Paragraph filtrosTotalEntradas = new Paragraph();
            filtrosTotalEntradas.SpacingAfter = 10;

            Paragraph filtrosTotalSaidas = new Paragraph();
            filtrosTotalSaidas.SpacingAfter = 10;

            Paragraph filtrosTituloEntradas = new Paragraph();
            filtrosTituloEntradas.SpacingAfter = 10;

            Paragraph filtrosTituloSaidas = new Paragraph();
            filtrosTituloSaidas.SpacingAfter = 10;

            var fonteTabela = FontFactory.GetFont(FontFactory.COURIER, 8);

            PdfPTable tabelaFiltros = new PdfPTable(2);
            tabelaFiltros.WidthPercentage = 100;
            tabelaFiltros.SetWidths(new float[] { 50f, 50f });

            iTextSharp.text.Font fonteTitulo = fonteFiltro;

            // Linha separadora limpa (sem espaços extras)
            LineSeparator linha = new LineSeparator();
            linha.LineWidth = 1f;

            // Método auxiliar para adicionar linha sem espaço exagerado
            PdfPCell CriarLinha()
            {
                return new PdfPCell(new Phrase(" "))
                {
                    Border = Rectangle.BOTTOM_BORDER,
                    BorderWidthBottom = 0.5f,
                    Padding = 0,
                    Colspan = 1
                };
            }
            PdfPCell celulaAbertura = new PdfPCell();
            celulaAbertura.Border = Rectangle.NO_BORDER;

            celulaAbertura.AddElement(new Paragraph("SALDO DE ABERTURA", fonteFiltroNegrito)
            {
                SpacingAfter = 5f
            });

            celulaAbertura.AddElement(new Paragraph(aberturaCaixa, fonteFiltro));
            celulaAbertura.AddElement(new Paragraph(aberturaBCI1, fonteFiltro));
            celulaAbertura.AddElement(new Paragraph(aberturaBCI2, fonteFiltro));
            celulaAbertura.AddElement(new Paragraph(aberturaMpesa, fonteFiltro));

            // Total abertura
            celulaAbertura.AddElement(new Paragraph(totalAberturaTexto, fonteFiltroNegrito)
            {
                SpacingAfter = 5f
            });

            // Linha separadora
            celulaAbertura.AddElement(new Chunk(linha));

            // Tabernáculos
            celulaAbertura.AddElement(new Paragraph("SALDO EM TABERNÁCULOS", fonteFiltroNegrito)
            {
                SpacingAfter = 5f
            });

            celulaAbertura.AddElement(new Paragraph(aberturaMaputo, fonteFiltro));
            celulaAbertura.AddElement(new Paragraph(aberturaManhica, fonteFiltro));
            celulaAbertura.AddElement(new Paragraph(aberturaRomao, fonteFiltro));
            celulaAbertura.AddElement(new Paragraph(aberturaMahubo, fonteFiltro));
            celulaAbertura.AddElement(new Paragraph(aberturaMaguaza, fonteFiltro));
            celulaAbertura.AddElement(new Paragraph(aberturaFevereiro, fonteFiltro));
            celulaAbertura.AddElement(new Paragraph(aberturaMaluana, fonteFiltro));
            celulaAbertura.AddElement(new Paragraph(aberturaMalhampsene, fonteFiltro));

            // Linha
            celulaAbertura.AddElement(new Chunk(linha));

            // Linhas / Balanço
            celulaAbertura.AddElement(new Paragraph("SALDO EM LINHAS", fonteFiltroNegrito)
            {
                SpacingBefore = 5f,
                SpacingAfter = 5f
            });

            celulaAbertura.AddElement(new Paragraph(aberturaCorrente, fonteFiltroNegrito));
            celulaAbertura.AddElement(new Paragraph(aberturaConstrucao, fonteFiltroNegrito));

            // Linha após construção
            celulaAbertura.AddElement(new Chunk(linha));

            // Inventário
            celulaAbertura.AddElement(new Paragraph(aberturaInventario, fonteFiltroNegrito));

            // Linha após inventário
            celulaAbertura.AddElement(new Chunk(linha));

            celulaAbertura.AddElement(new Paragraph(aberturaCobranca, fonteFiltroNegrito));
            celulaAbertura.AddElement(new Paragraph(aberturaPassivo, fonteFiltroNegrito));

            tabelaFiltros.AddCell(celulaAbertura);

            PdfPCell celulaFecho = new PdfPCell();
            celulaFecho.Border = Rectangle.NO_BORDER;

            celulaFecho.AddElement(new Paragraph("SALDO DE FECHO", fonteFiltroNegrito)
            {
                SpacingAfter = 5f
            });

            celulaFecho.AddElement(new Paragraph(fechoCaixa, fonteFiltro));
            celulaFecho.AddElement(new Paragraph(fechoBCI1, fonteFiltro));
            celulaFecho.AddElement(new Paragraph(fechoBCI2, fonteFiltro));
            celulaFecho.AddElement(new Paragraph(fechoMpesa, fonteFiltro));

            // Total fecho
            celulaFecho.AddElement(new Paragraph(totalFechoTexto, fonteFiltroNegrito)
            {
                SpacingAfter = 5f
            });

            celulaFecho.AddElement(new Chunk(linha));

            celulaFecho.AddElement(new Paragraph("SALDO EM TABERNÁCULOS", fonteFiltroNegrito)
            {
                SpacingAfter = 5f
            });

            celulaFecho.AddElement(new Paragraph(fechoMaputo, fonteFiltro));
            celulaFecho.AddElement(new Paragraph(fechoManhica, fonteFiltro));
            celulaFecho.AddElement(new Paragraph(fechoRomao, fonteFiltro));
            celulaFecho.AddElement(new Paragraph(fechoMahubo, fonteFiltro));
            celulaFecho.AddElement(new Paragraph(fechoMaguaza, fonteFiltro));
            celulaFecho.AddElement(new Paragraph(fechoFevereiro, fonteFiltro));
            celulaFecho.AddElement(new Paragraph(fechoMaluana, fonteFiltro));
            celulaFecho.AddElement(new Paragraph(fechoMalhampsene, fonteFiltro));

            celulaFecho.AddElement(new Chunk(linha));

            celulaFecho.AddElement(new Paragraph("SALDO EM LINHAS", fonteFiltroNegrito)
            {
                SpacingBefore = 5f,
                SpacingAfter = 5f
            });

            celulaFecho.AddElement(new Paragraph(fechoCorrente, fonteFiltroNegrito));
            celulaFecho.AddElement(new Paragraph(fechoConstrucao, fonteFiltroNegrito));

            celulaFecho.AddElement(new Chunk(linha));

            celulaFecho.AddElement(new Paragraph(fechoInventario, fonteFiltroNegrito));

            celulaFecho.AddElement(new Chunk(linha));

            celulaFecho.AddElement(new Paragraph(fechoCobranca, fonteFiltroNegrito));
            celulaFecho.AddElement(new Paragraph(fechoPassivo, fonteFiltroNegrito));

            tabelaFiltros.AddCell(celulaFecho);

            doc.Add(tabelaFiltros);
            doc.Add(new Paragraph("\n"));

            filtrosTotalEntradas.Add(new Phrase(totalEntradasTexto + "\n", fonteFiltroNegrito));
            filtrosTotalSaidas.Add(new Phrase(totalSaidasTexto + "\n", fonteFiltroNegrito));

            // ===============================
            // 🔹 TABELA ENTRADAS
            // ===============================

            BaseColor corCabecalho = new BaseColor(240, 240, 240);
            iTextSharp.text.Font fonteCabecalho = FontFactory.GetFont(FontFactory.HELVETICA, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font fonteDados = FontFactory.GetFont(FontFactory.COURIER, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            BaseColor corLinha = new BaseColor(200, 200, 200);

            // Tabela
            PdfPTable tabela = new PdfPTable(5);
            tabela.WidthPercentage = 100;
            tabela.SetWidths(new float[] { 10f, 20f, 15f, 40f, 15f });
            tabela.HeaderRows = 1; // Repete cabeçalho automaticamente

            // Cabeçalhos
            AddHeader(tabela, "Id", fonteCabecalho, corCabecalho);
            AddHeader(tabela, "Tabernáculo", fonteCabecalho, corCabecalho);
            AddHeader(tabela, "Tipo", fonteCabecalho, corCabecalho);
            AddHeader(tabela, "Descrição", fonteCabecalho, corCabecalho);
            AddHeader(tabela, "Valor", fonteCabecalho, corCabecalho);

            // Dados com linhas horizontais finas
            foreach (var item in entradas)
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
                tabela.AddCell(AddCell(item.TipoEntrada?.Nome ?? ""));
                tabela.AddCell(AddCell(item.Descricao ?? ""));
                tabela.AddCell(AddCell(item.Valor.ToString("N2", new CultureInfo("pt-PT")), Element.ALIGN_RIGHT));
            }

            doc.Add(tabela);
            doc.Add(filtrosTotalEntradas);

            // ===============================
            // 🔹 TABELA SAÍDAS
            // ===============================

            PdfPTable tabelaSaida = new PdfPTable(5);
            tabelaSaida.WidthPercentage = 100;
            tabelaSaida.SetWidths(new float[] { 10f, 20f, 15f, 40f, 15f });
            tabelaSaida.HeaderRows = 1; // Repete cabeçalho automaticamente

            // Cabeçalhos
            AddHeader(tabelaSaida, "Id", fonteCabecalho, corCabecalho);
            AddHeader(tabelaSaida, "Tabernáculo", fonteCabecalho, corCabecalho);
            AddHeader(tabelaSaida, "Tipo", fonteCabecalho, corCabecalho);
            AddHeader(tabelaSaida, "Descrição", fonteCabecalho, corCabecalho);
            AddHeader(tabelaSaida, "Valor", fonteCabecalho, corCabecalho);

            // Dados com linhas horizontais finas
            foreach (var item in saidas)
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

                tabelaSaida.AddCell(AddCell(item.Id.ToString()));
                tabelaSaida.AddCell(AddCell(item.Agencia?.Nome ?? ""));
                tabelaSaida.AddCell(AddCell(item.TipoDespesa?.Nome ?? ""));
                tabelaSaida.AddCell(AddCell(item.Descricao ?? ""));
                tabelaSaida.AddCell(AddCell(item.Valor.ToString("N2", new CultureInfo("pt-PT")), Element.ALIGN_RIGHT));
            }

            doc.Add(tabelaSaida);
            doc.Add(filtrosTotalSaidas);

            doc.Close();

            return File(stream.ToArray(), "application/pdf", $"Relatorio_Diario_{data:ddMMyyyy}.pdf");
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
    }
}
