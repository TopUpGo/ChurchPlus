using Analise.Data;
using Analise.Enuns;
using Analise.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace Analise.Repositorio
{
    public class TipoEntradaRepositorio : ITipoEntradaRepositorio
    {
        private readonly BancoContext _context;
        public TipoEntradaRepositorio(BancoContext bancoContext)
        {
            this._context = bancoContext;
        }
        public TipoEntradaModel ListarPorId(int id)
        {
            return _context.TipoEntradas.FirstOrDefault(x => x.Id == id);
        }
        public TipoEntradaModel Adicionar(TipoEntradaModel registo)
        {
            registo.DataCadastro = DateTime.Now;
            _context.TipoEntradas.Add(registo);
            _context.SaveChanges();
            return registo;
        }
        public TipoEntradaModel Actualizar(TipoEntradaModel registo)
        {
            TipoEntradaModel registoDB = ListarPorId(registo.Id);
            if (registoDB == null) throw new System.Exception("Erro na actualização!");
            registoDB.Nome = registo.Nome;

            _context.TipoEntradas.Update(registoDB);
            _context.SaveChanges();
            return registoDB;
        }
        public List<TipoEntradaModel> BuscarTodos()
        {
            return _context.TipoEntradas.OrderBy(t => t.Nome).ToList();
        }
    }
}
