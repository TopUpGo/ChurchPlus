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
    public class LinhaController : Controller
    {
        private readonly ILinhaRepositorio _cargoRepositorio;
        private readonly BancoContext _context;
        public LinhaController(BancoContext bancoContext, ILinhaRepositorio cargoRepositorio)
        {
            _cargoRepositorio = cargoRepositorio;
            this._context = bancoContext;
        }

        // GET: Criar
        public IActionResult Criar()
        {
            var viewModel = new LinhaViewModel
            {
                LinhaNome = new LinhaModel(),
                ListaLinhas = _cargoRepositorio.BuscarTodos()
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

            var viewModel = new LinhaViewModel
            {
                LinhaNome = cargoSelecionado,   // ✅ Agora carrega os dados corretos
                ListaLinhas = _cargoRepositorio.BuscarTodos()
            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Criar(LinhaViewModel viewModel)
        {
            try
            {
                Console.WriteLine("Iniciando criação...");
                Console.WriteLine("Valor recebido: " + viewModel?.LinhaNome?.Nome);

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

                    viewModel.ListaLinhas = _cargoRepositorio.BuscarTodos();
                    TempData["MensagemErro"] = "Dados inválidos! Verifique os campos e tente novamente.";
                    return View(viewModel);
                }

                _cargoRepositorio.Adicionar(viewModel.LinhaNome);
                TempData["MensagemSucesso"] = "Registado com sucesso!";
                return RedirectToAction("Criar");
            }
            catch (Exception erro)
            {
                Console.WriteLine("Erro ao criar: " + erro.Message);
                viewModel.ListaLinhas = _cargoRepositorio.BuscarTodos();
                TempData["MensagemErro"] = $"Erro ao registrar: {erro.Message}";
                return View(viewModel);
            }
        }

        // POST: Editar
        [HttpPost]
        public IActionResult Editar(LinhaViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _cargoRepositorio.Actualizar(viewModel.LinhaNome);
                    TempData["MensagemSucesso"] = "Actualizado com sucesso!";
                    return RedirectToAction("Criar");
                }
                viewModel.ListaLinhas = _cargoRepositorio.BuscarTodos();
                return View(viewModel);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, erro ao actualizar. Detalhes: {erro.Message}";
                return RedirectToAction("Criar");
            }
        }

        public IActionResult Transferir()
        {
            var model = new TransferenciaViewModelLinha
            {
                ListaLinhas = _context.Linhas
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Nome
                    }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Transferir(TransferenciaViewModelLinha model)
        {
            if (model.ContaOrigemId == model.ContaDestinoId)
            {
                ModelState.AddModelError("", "Linha origem e destino não podem ser iguais.");
            }

            if (model.Valor <= 0)
            {
                ModelState.AddModelError("", "Valor inválido.");
            }

            if (!ModelState.IsValid)
            {
                model.ListaLinhas = _context.Linhas
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
                var origem = _context.Linhas.First(c => c.Id == model.ContaOrigemId);
                var destino = _context.Linhas.First(c => c.Id == model.ContaDestinoId);

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
