using Analise.Data;
using Analise.Enuns;
using Analise.Helper;
using Analise.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace Analise.Repositorio
{
    public class ContaRepositorio : IContaRepositorio
    {
        private readonly BancoContext _context;
        
        public ContaRepositorio(BancoContext bancoContext)
        {
            this._context = bancoContext;           
        }
        public ContaModel ListarPorId(int id)
        {
            return _context.Contas.FirstOrDefault(x => x.Id == id);
        }
        public ContaModel Adicionar(ContaModel registo)
        {
            registo.DataCadastro = DateTime.Now;
            _context.Contas.Add(registo);
            _context.SaveChanges();
            return registo;
        }
        public ContaModel Actualizar(ContaModel registo)
        {
            ContaModel registoDB = ListarPorId(registo.Id);
            if (registoDB == null) throw new System.Exception("Erro na actualização!");
            registoDB.Nome = registo.Nome;
            //registoDB.Credito = registo.Credito;
            //registoDB.Debito = registo.Debito;

            _context.Contas.Update(registoDB);
            _context.SaveChanges();
            return registoDB;
        }
        public List<ContaModel> BuscarTodos()
        {
            return _context.Contas.ToList();
        }
    }
}
