using Analise.Data;
using Analise.Enuns;
using Analise.Helper;
using Analise.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace Analise.Repositorio
{
    public class ContasPagarRepositorio : IContasPagarRepositorio
    {
        private readonly BancoContext _context;
        
        public ContasPagarRepositorio(BancoContext bancoContext)
        {
            this._context = bancoContext;           
        }
        public ContasPagarModel ListarPorId(int id)
        {
            return _context.PagarContas
               .Include(x => x.Cadastro)
               .Include(x => x.Agencia)
               .Include(x => x.Usuario)
               .FirstOrDefault(x => x.Id == id);
        }
        public ContasPagarModel Adicionar(ContasPagarModel registo)
        {
            registo.DataCadastro = DateTime.Now;
            _context.PagarContas.Add(registo);
            _context.SaveChanges();
            return registo;
        }
        public ContasPagarModel Actualizar(ContasPagarModel registo)
        {
            ContasPagarModel registoDB = ListarPorId(registo.Id);
            if (registoDB == null) throw new System.Exception("Erro na actualização!");
            registoDB.Pago = registo.Pago;
            registoDB.Valor = registo.Valor;

            _context.PagarContas.Update(registoDB);
            _context.SaveChanges();
            return registoDB;
        }
        public List<ContasPagarModel> BuscarTodos()
        {
            return _context.PagarContas
            .Include(x => x.Cadastro)
            .Include(x => x.Agencia)
            .Include(x => x.Usuario)
            .ToList();
        }
    }
}
