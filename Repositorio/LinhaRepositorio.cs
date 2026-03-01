using Analise.Data;
using Analise.Enuns;
using Analise.Helper;
using Analise.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace Analise.Repositorio
{
    public class LinhaRepositorio : ILinhaRepositorio
    {
        private readonly BancoContext _context;
        
        public LinhaRepositorio(BancoContext bancoContext)
        {
            this._context = bancoContext;           
        }
        public LinhaModel ListarPorId(int id)
        {
            return _context.Linhas.FirstOrDefault(x => x.Id == id);
        }
        public LinhaModel Adicionar(LinhaModel registo)
        {
            registo.DataCadastro = DateTime.Now;
            _context.Linhas.Add(registo);
            _context.SaveChanges();
            return registo;
        }
        public LinhaModel Actualizar(LinhaModel registo)
        {
            LinhaModel registoDB = ListarPorId(registo.Id);
            if (registoDB == null) throw new System.Exception("Erro na actualização!");
            registoDB.Nome = registo.Nome;
            //registoDB.Credito = registo.Credito;
            //registoDB.Debito = registo.Debito;

            _context.Linhas.Update(registoDB);
            _context.SaveChanges();
            return registoDB;
        }
        public List<LinhaModel> BuscarTodos()
        {
            return _context.Linhas.ToList();
        }
    }
}
