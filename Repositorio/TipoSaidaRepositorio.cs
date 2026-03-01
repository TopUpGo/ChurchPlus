using Analise.Data;
using Analise.Enuns;
using Analise.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace Analise.Repositorio
{
    public class TipoSaidaRepositorio : ITipoSaidaRepositorio
    {
        private readonly BancoContext _context;
        public TipoSaidaRepositorio(BancoContext bancoContext)
        {
            this._context = bancoContext;
        }
        public TipoSaidaModel ListarPorId(int id)
        {
            return _context.TipoSaidas.FirstOrDefault(x => x.Id == id);
        }
        public TipoSaidaModel Adicionar(TipoSaidaModel registo)
        {
            registo.DataCadastro = DateTime.Now;
            _context.TipoSaidas.Add(registo);
            _context.SaveChanges();
            return registo;
        }
        public TipoSaidaModel Actualizar(TipoSaidaModel registo)
        {
            TipoSaidaModel registoDB = ListarPorId(registo.Id);
            if (registoDB == null) throw new System.Exception("Erro na actualização!");
            registoDB.Nome = registo.Nome;

            _context.TipoSaidas.Update(registoDB);
            _context.SaveChanges();
            return registoDB;
        }
        public List<TipoSaidaModel> BuscarTodos()
        {
            return _context.TipoSaidas.OrderBy(t => t.Nome).ToList();
        }
    }
}
