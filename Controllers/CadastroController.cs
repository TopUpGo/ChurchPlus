using Analise.Filters;
using Analise.Models;
using Analise.Repositorio;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Win32;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Metadata;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Analise.Controllers
{
    public class CadastroController : Controller
    {
        private readonly ICadastroRepositorio _cargoRepositorio;
        private readonly IEstadoRepositorio _estadoRepositorio;
        private readonly IAgenciaRepositorio _agenciaRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IWebHostEnvironment _env;
        public CadastroController(
             ICadastroRepositorio cargoRepositorio,
             IEstadoRepositorio estadoRepositorio,
             IUsuarioRepositorio usuarioRepositorio,
             IAgenciaRepositorio agenciaRepositorio,
             IWebHostEnvironment env)
            {
            _cargoRepositorio = cargoRepositorio;
            _estadoRepositorio = estadoRepositorio;
            _agenciaRepositorio = agenciaRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _env = env;
        }

        //Impressao
        public IActionResult ImprimirCadastros()
        {
            using MemoryStream stream = new MemoryStream();

            float pxPorMm = 72 / 25.5f;

            iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.A4,
                15 * pxPorMm,
                15 * pxPorMm,
                15 * pxPorMm,
                20 * pxPorMm);

            PdfWriter.GetInstance(doc, stream);

            doc.Open();

            // ================= CONFIG =================

            BaseColor corCabecalho = new BaseColor(220, 220, 220);

            iTextSharp.text.Font fonteTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
            iTextSharp.text.Font fonteCabecalho = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9);
            iTextSharp.text.Font fonteDados = FontFactory.GetFont(FontFactory.HELVETICA, 8);

            // ================= LOGO =================

            string logoPath = Path.Combine(_env.WebRootPath, "images", "logo.png");

            if (System.IO.File.Exists(logoPath))
            {
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoPath);
                logo.ScaleToFit(80f, 80f);
                logo.Alignment = Element.ALIGN_CENTER;
                doc.Add(logo);
            }

            // ================= TITULO =================

            Paragraph titulo = new Paragraph("LISTA DE CADASTROS\n\n", fonteTitulo);
            titulo.Alignment = Element.ALIGN_CENTER;
            doc.Add(titulo);

            // ================= TABELA =================

            PdfPTable tabela = new PdfPTable(5);
            tabela.WidthPercentage = 100;

            float[] larguras = { 8f, 30f, 20f, 20f, 22f };
            tabela.SetWidths(larguras);

            // Cabeçalhos
            AddHeader(tabela, "Id", fonteCabecalho, corCabecalho);
            AddHeader(tabela, "Nome", fonteCabecalho, corCabecalho);
            AddHeader(tabela, "Documento", fonteCabecalho, corCabecalho);
            AddHeader(tabela, "Nascimento", fonteCabecalho, corCabecalho);
            AddHeader(tabela, "Contacto", fonteCabecalho, corCabecalho);

            // ================= DADOS =================iTextSharp

            var lista = _cargoRepositorio.BuscarTodos();

            foreach (var item in lista)
            {
                tabela.AddCell(new Phrase(item.Id.ToString(), fonteDados));
                tabela.AddCell(new Phrase(item.Nome ?? "", fonteDados));
                tabela.AddCell(new Phrase(item.Documento ?? "", fonteDados));
                tabela.AddCell(new Phrase(item.Nascimento.HasValue ? item.Nascimento.Value.ToString("dd/MM/yyyy"): "",fonteDados));
                tabela.AddCell(new Phrase(item.Contacto ?? "", fonteDados));
            }

            doc.Add(tabela);

            doc.Close();

            return File(stream.ToArray(), "application/pdf", "Lista_Cadastros.pdf");
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
        // GET: Criar
        public IActionResult Criar()
        {
            int usuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
            int agenciaId = HttpContext.Session.GetInt32("AgenciaId") ?? 0;
            //var estados = _estadoRepositorio.BuscarTodos()
            //    .Select(m => new SelectListItem
            //    {
            //        Value = m.Id.ToString(),
            //        Text = m.Estado
            //    }).ToList();

            //var tabernaculos = _agenciaRepositorio.BuscarTodos()
            //    .Select(m => new SelectListItem
            //    {
            //        Value = m.Id.ToString(),
            //        Text = m.Nome
            //    }).ToList();

            var pedidoModel = new CadastroModel
            {
                //ListaAgencias = tabernaculos,
                //ListaEstados = estados
            };


            var viewModel = new CadastroViewModel
            {
                CadastroNome = pedidoModel, 
                ListaCadastros = _cargoRepositorio.BuscarTodos()
            };

            return View(viewModel);
        }

        // GET: Editar
        public IActionResult Editar(int id)
        {
            int usuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
            int agenciaId = HttpContext.Session.GetInt32("AgenciaId") ?? 0;

            var cadastroSelecionado = _cargoRepositorio.ListarPorId(id);

            if (cadastroSelecionado == null)
            {
                TempData["MensagemErro"] = "Não encontrado.";
                return RedirectToAction("Criar");
            }

            var viewModel = new CadastroViewModel
            {
                CadastroNome = cadastroSelecionado,
                ListaCadastros = _cargoRepositorio.BuscarTodos()
            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Criar(CadastroViewModel agencia)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Recarrega dropdowns
                    //agencia.CadastroNome.ListaAgencias = _agenciaRepositorio.BuscarTodos()
                    //    .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Nome })
                    //    .ToList();

                    //agencia.CadastroNome.ListaEstados = _estadoRepositorio.BuscarTodos()
                    //    .Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Estado })
                    //    .ToList();

                    // ✅ Agora preenche ListaCadastros
                    agencia.ListaCadastros = _cargoRepositorio.BuscarTodos();

                    TempData["MensagemErro"] = "Dados inválidos! Corrija os erros e tente novamente.";
                    return View(agencia);
                }

                // ✅ Se chegou aqui → ModelState válido
                //agencia.CadastroNome.UsuarioId = usuarioId;
                _cargoRepositorio.Adicionar(agencia.CadastroNome);

                TempData["MensagemSucesso"] = "Registado com sucesso!";
                return RedirectToAction("Criar");
            }
            catch (Exception erro)
            {
                Console.WriteLine("Erro: " + erro.InnerException?.Message);

                TempData["MensagemErro"] = $"Ops, erro ao registar! Detalhes: {erro.InnerException?.Message ?? erro.Message}";

                // Recarrega dropdowns
                //agencia.CadastroNome.ListaAgencias = _agenciaRepositorio.BuscarTodos()
                //    .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Nome })
                //    .ToList();

                //agencia.CadastroNome.ListaEstados = _estadoRepositorio.BuscarTodos()
                //    .Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Estado })
                //    .ToList();

                // ✅ IMPORTANTE: preenche ListaCadastros no erro também!
                agencia.ListaCadastros = _cargoRepositorio.BuscarTodos();

                return View(agencia);
            }
        }


        // POST: Editar
        [HttpPost]
        public IActionResult Editar(CadastroViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    //viewModel.CadastroNome.UsuarioId = usuarioId;
                    _cargoRepositorio.Actualizar(viewModel.CadastroNome);
                    TempData["MensagemSucesso"] = "Actualizado com sucesso!";
                    return RedirectToAction("Criar");
                }
                viewModel.ListaCadastros = _cargoRepositorio.BuscarTodos();
                return View(viewModel);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, erro ao actualizar. Detalhes: {erro.Message}";
                return RedirectToAction("Criar");
            }
        }

    }
}
