using Analise.Data;
using Analise.Enuns;
using Analise.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace Analise.Repositorio
{
    public class TipoDespesaRepositorio : ITipoDespesaRepositorio
    {
        private readonly BancoContext _context;
        public TipoDespesaRepositorio(BancoContext bancoContext)
        {
            this._context = bancoContext;
        }
        public TipoDespesaModel ListarPorId(int id)
        {
            return _context.TipoDespesas.FirstOrDefault(x => x.Id == id);
        }
        public TipoDespesaModel Adicionar(TipoDespesaModel registo)
        {
            registo.DataCadastro = DateTime.Now;
            _context.TipoDespesas.Add(registo);
            _context.SaveChanges();
            return registo;
        }
        public TipoDespesaModel Actualizar(TipoDespesaModel registo)
        {
            TipoDespesaModel registoDB = ListarPorId(registo.Id);
            if (registoDB == null) throw new System.Exception("Erro na actualização!");
            registoDB.Nome = registo.Nome;

            _context.TipoDespesas.Update(registoDB);
            _context.SaveChanges();
            return registoDB;
        }
        public List<TipoDespesaModel> BuscarTodos()
        {
            return _context.TipoDespesas.OrderBy(t => t.Nome).ToList();
        }
    }
}
