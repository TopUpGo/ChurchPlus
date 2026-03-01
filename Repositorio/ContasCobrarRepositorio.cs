using Analise.Data;
using Analise.Enuns;
using Analise.Helper;
using Analise.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace Analise.Repositorio
{
    public class ContasCobrarRepositorio : IContasCobrarRepositorio
    {
        private readonly BancoContext _context;
        
        public ContasCobrarRepositorio(BancoContext bancoContext)
        {
            this._context = bancoContext;           
        }
        public ContasCobrarModel ListarPorId(int id)
        {
            return _context.CobrarContas
                .Include(x => x.Cadastro)
               .Include(x => x.Agencia)
               .Include(x => x.Usuario)
               .FirstOrDefault(x => x.Id == id);
        }
        public ContasCobrarModel Adicionar(ContasCobrarModel registo)
        {
            registo.DataCadastro = DateTime.Now;
            _context.CobrarContas.Add(registo);
            _context.SaveChanges();
            return registo;
        }
        public ContasCobrarModel Actualizar(ContasCobrarModel registo)
        {
            ContasCobrarModel registoDB = ListarPorId(registo.Id);
            if (registoDB == null) throw new System.Exception("Erro na actualização!");
            registoDB.Pago = registo.Pago;
            registoDB.Valor = registo.Valor;

            _context.CobrarContas.Update(registoDB);
            _context.SaveChanges();
            return registoDB;
        }
        public List<ContasCobrarModel> BuscarTodos()
        {
            return _context.CobrarContas
                .Include(x => x.Cadastro)
            .Include(x => x.Agencia)
            .Include(x => x.Usuario)
            .ToList();
        }
    }
}
