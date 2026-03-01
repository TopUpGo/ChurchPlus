using Analise.Data;
using Analise.Filters;
using Analise.Models;
using Analise.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace Analise.Controllers
{
    public class ContaController : Controller
    {
        private readonly IContaRepositorio _cargoRepositorio;
        private readonly BancoContext _context;
        public ContaController(BancoContext bancoContext, IContaRepositorio cargoRepositorio)
        {
            _cargoRepositorio = cargoRepositorio;
            this._context = bancoContext;
        }

        // GET: Criar
        public IActionResult Criar()
        {
            var viewModel = new ContaViewModel
            {
                ContaNome = new ContaModel(),
                ListaContas = _cargoRepositorio.BuscarTodos()
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
            
            var viewModel = new ContaViewModel
            {
                ContaNome = new ContaModel(),
                ListaContas = _cargoRepositorio.BuscarTodos()
            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Criar(ContaViewModel viewModel)
        {
            try
            {
                Console.WriteLine("Iniciando criação...");
                Console.WriteLine("Valor recebido: " + viewModel?.ContaNome?.Nome);

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

                    viewModel.ListaContas = _cargoRepositorio.BuscarTodos();
                    TempData["MensagemErro"] = "Dados inválidos! Verifique os campos e tente novamente.";
                    return View(viewModel);
                }

                _cargoRepositorio.Adicionar(viewModel.ContaNome);
                TempData["MensagemSucesso"] = "Registado com sucesso!";
                return RedirectToAction("Criar");
            }
            catch (Exception erro)
            {
                Console.WriteLine("Erro ao criar: " + erro.Message);
                viewModel.ListaContas = _cargoRepositorio.BuscarTodos();
                TempData["MensagemErro"] = $"Erro ao registrar: {erro.Message}";
                return View(viewModel);
            }
        }

        // POST: Editar
        [HttpPost]
        public IActionResult Editar(ContaViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _cargoRepositorio.Actualizar(viewModel.ContaNome);
                    TempData["MensagemSucesso"] = "Actualizado com sucesso!";
                    return RedirectToAction("Criar");
                }
                viewModel.ListaContas = _cargoRepositorio.BuscarTodos();
                return View(viewModel);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, erro ao actualizar. Detalhes: {erro.Message}";
                return RedirectToAction("Editar");
            }
        }

        public IActionResult Transferir()
        {
            var model = new TransferenciaViewModel
            {
                ListaContas = _context.Contas
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Nome
                    }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Transferir(TransferenciaViewModel model)
        {
            if (model.ContaOrigemId == model.ContaDestinoId)
            {
                ModelState.AddModelError("", "Conta origem e destino não podem ser iguais.");
            }

            if (model.Valor <= 0)
            {
                ModelState.AddModelError("", "Valor inválido.");
            }

            if (!ModelState.IsValid)
            {
                model.ListaContas = _context.Contas
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Nome
                    }).ToList();

                return View(model);
            }

            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var origem = _context.Contas.First(c => c.Id == model.ContaOrigemId);
                var destino = _context.Contas.First(c => c.Id == model.ContaDestinoId);

                if (origem.Saldo < model.Valor)
                {
                    ModelState.AddModelError("", "Saldo insuficiente.");
                    return View(model);
                }

                // Debita origem
                origem.Debito += model.Valor;

                // Credita destino
                destino.Credito += model.Valor;

                _context.SaveChanges();

                transaction.Commit();

                TempData["MensagemSucesso"] = "Transferência realizada com sucesso!";
                return RedirectToAction("Criar");
            }
            catch
            {
                transaction.Rollback();
                TempData["MensagemErro"] = "Erro ao processar transferência.";
                return RedirectToAction("Transferir");
            }
        }
    }
}
