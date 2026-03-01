using Analise.Filters;
using Analise.Helper;
using Analise.Models;
using Analise.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Win32;
using System.Diagnostics.Contracts;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Analise.Controllers
{
    public class TicketController : Controller
    {
        private readonly ISessao _sessao;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IAgenciaRepositorio _agenciaRepositorio;
        private readonly ITicketRepositorio _ticketRepositorio;
        public TicketController(ITicketRepositorio ticketRepositorio, ISessao sessao,IUsuarioRepositorio usuarioRepositorio,
            IAgenciaRepositorio agenciaRepositorio)
        {
            _sessao = sessao;
            _usuarioRepositorio = usuarioRepositorio;
            _agenciaRepositorio = agenciaRepositorio;
            _ticketRepositorio = ticketRepositorio;
        }

        public IActionResult Index(string estado, string dataInicio, string dataFim)
        {
            DateTime? dtInicio = null;
            DateTime? dtFim = null;

            if (!string.IsNullOrEmpty(dataInicio) && DateTime.TryParse(dataInicio, out var tempInicio))
                dtInicio = tempInicio;

            if (!string.IsNullOrEmpty(dataFim) && DateTime.TryParse(dataFim, out var tempFim))
                dtFim = tempFim;

            var tickets = _ticketRepositorio.BuscarFiltrados(estado, dtInicio, dtFim);

            // Para manter filtros na view
            ViewBag.EstadoSelecionado = estado;
            ViewBag.DataInicio = dtInicio?.ToString("yyyy-MM-dd");
            ViewBag.DataFim = dtFim?.ToString("yyyy-MM-dd");

            // SelectList para DropDown
            ViewBag.Estados = new SelectList(new[] { "Pendente", "Processo", "Resolvido", "Fechado" }, estado);

            return View(tickets);
        }


        [HttpGet]
        public IActionResult Editar(int id)
        {
            TicketModel usuario = _ticketRepositorio.ListarPorId(id);

            if (usuario == null)
            {
                TempData["MensagemErro"] = "Não encontrado.";
                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(TicketModel agencia)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Buscar os dados atuais no banco para preservar o arquivo antigo
                    var agenciaExistente = _ticketRepositorio.ListarPorId(agencia.Id);

                    if (agenciaExistente == null)
                    {
                        TempData["MensagemErro"] = "Não encontrada!";
                        return RedirectToAction("Index");
                    }

                    //int usuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
                    //int agenciaId = HttpContext.Session.GetInt32("AgenciaId") ?? 0;

                    _ticketRepositorio.Actualizar(agencia);
                    TempData["MensagemSucesso"] = "Actualizado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View("Editar", agencia);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, erro ao actualizar! Detalhes: {erro.Message}";
                return RedirectToAction("Editar", new { id = agencia.Id });
            }
        }
    }
}
