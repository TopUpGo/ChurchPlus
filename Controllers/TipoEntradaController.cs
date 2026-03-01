using Analise.Filters;
using Analise.Models;
using Analise.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace Analise.Controllers
{
    [PaginaRestritaSomenteAdmin]
    public class TipoEntradaController : Controller
    {
        private readonly ITipoEntradaRepositorio _cargoRepositorio;
        public TipoEntradaController(ITipoEntradaRepositorio cargoRepositorio)
        {
            _cargoRepositorio = cargoRepositorio;
        }

        // GET: Criar
        public IActionResult Criar()
        {
            var viewModel = new TipoEntradaViewModel
            {
                TipoEntradaNome = new TipoEntradaModel(),
                ListaTipoEntradas = _cargoRepositorio.BuscarTodos()
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

            var viewModel = new TipoEntradaViewModel
            {
                TipoEntradaNome = cargoSelecionado,
                ListaTipoEntradas = _cargoRepositorio.BuscarTodos()
            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Criar(TipoEntradaViewModel viewModel)
        {
            try
            {
                Console.WriteLine("Iniciando criação...");
                Console.WriteLine("Valor recebido: " + viewModel?.TipoEntradaNome?.Nome);

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

                    viewModel.ListaTipoEntradas = _cargoRepositorio.BuscarTodos();
                    TempData["MensagemErro"] = "Dados inválidos! Verifique os campos e tente novamente.";
                    return View(viewModel);
                }

                _cargoRepositorio.Adicionar(viewModel.TipoEntradaNome);
                TempData["MensagemSucesso"] = "Registado com sucesso!";
                return RedirectToAction("Criar");
            }
            catch (Exception erro)
            {
                Console.WriteLine("Erro ao criar: " + erro.Message);
                viewModel.ListaTipoEntradas = _cargoRepositorio.BuscarTodos();
                TempData["MensagemErro"] = $"Erro ao registrar: {erro.Message}";
                return View(viewModel);
            }
        }

        // POST: Editar
        [HttpPost]
        public IActionResult Editar(TipoEntradaViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _cargoRepositorio.Actualizar(viewModel.TipoEntradaNome);
                    TempData["MensagemSucesso"] = "Actualizado com sucesso!";
                    return RedirectToAction("Criar");
                }

                viewModel.ListaTipoEntradas = _cargoRepositorio.BuscarTodos();
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
