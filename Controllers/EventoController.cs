using Analise.Filters;
using Analise.Models;
using Analise.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace Analise.Controllers
{
    [PaginaRestritaSomenteAdmin]
    public class EventoController : Controller
    {
        private readonly IEventoRepositorio _cargoRepositorio;
        public EventoController(IEventoRepositorio cargoRepositorio)
        {
            _cargoRepositorio = cargoRepositorio;
        }

        // GET: Criar
        public IActionResult Criar()
        {
            var viewModel = new EventoViewModel
            {
                EventoNome = new EventoModel(),
                ListaEventos = _cargoRepositorio.BuscarTodos()
            };

            return View(viewModel);
        }

        // GET: Editar
        public IActionResult Editar(int id)
        {
            var cargoSelecionado = _cargoRepositorio.ListarPorId(id);

            if (cargoSelecionado == null)
            {
                TempData["MensagemErro"] = "Não encontrado.";
                return RedirectToAction("Criar");
            }

            var viewModel = new EventoViewModel
            {
                EventoNome = cargoSelecionado,
                ListaEventos = _cargoRepositorio.BuscarTodos()
            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Criar(EventoViewModel viewModel)
        {
            try
            {
                Console.WriteLine("Iniciando criação...");
                Console.WriteLine("Valor recebido: " + viewModel?.EventoNome?.Nome);

                if (ModelState.IsValid)
                {
                    Console.WriteLine("ModelState inválido. Erros:");
                    foreach (var campo in ModelState)
                    {
                        foreach (var erro in campo.Value.Errors)
                        {
                            Console.WriteLine($"Erro no campo {campo.Key}: {erro.ErrorMessage}");
                        }
                    }

                    viewModel.ListaEventos = _cargoRepositorio.BuscarTodos();
                    TempData["MensagemErro"] = "Dados inválidos! Verifique os campos e tente novamente.";
                    return View(viewModel);
                }

                _cargoRepositorio.Adicionar(viewModel.EventoNome);
                TempData["MensagemSucesso"] = "Registado com sucesso!";
                return RedirectToAction("Criar");
            }
            catch (Exception erro)
            {
                Console.WriteLine("Erro ao criar: " + erro.Message);
                viewModel.ListaEventos = _cargoRepositorio.BuscarTodos();
                TempData["MensagemErro"] = $"Erro ao registrar: {erro.Message}";
                return View(viewModel);
            }
        }

        // POST: Editar
        [HttpPost]
        public IActionResult Editar(EventoViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _cargoRepositorio.Actualizar(viewModel.EventoNome);
                    TempData["MensagemSucesso"] = "Actualizado com sucesso!";
                    return RedirectToAction("Criar");
                }

                viewModel.ListaEventos = _cargoRepositorio.BuscarTodos();
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
