using Analise.Data;
using Analise.Enuns;
using Analise.Helper;
using Analise.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace Analise.Repositorio
{
    public class EntradaRepositorio : IEntradaRepositorio
    {
        private readonly BancoContext _context;
        
        public EntradaRepositorio(BancoContext bancoContext)
        {
            this._context = bancoContext;           
        }
        public EntradaModel ListarPorId(int id)
        {
            return _context.Entradas.FirstOrDefault(x => x.Id == id);
        }
        public EntradaModel Adicionar(EntradaModel registo)
        {
            registo.DataCadastro = DateTime.Now;
            _context.Entradas.Add(registo);
            _context.SaveChanges();
            return registo;
        }
        public EntradaModel Actualizar(EntradaModel registo)
        {
            EntradaModel registoDB = ListarPorId(registo.Id);
            if (registoDB == null) throw new System.Exception("Erro na actualização!");
            registoDB.TipoEntradaId = registo.TipoEntradaId;
            registoDB.LinhaId = registo.LinhaId;
            registoDB.ContaId = registo.ContaId;
            registoDB.Descricao = registo.Descricao;
            registoDB.Valor = registo.Valor;
            registoDB.CadastroId = registo.CadastroId;
            registoDB.AgenciaId = registo.AgenciaId;

            _context.Entradas.Update(registoDB);
            _context.SaveChanges();
            return registoDB;
        }
        public List<EntradaModel> BuscarTodos()
        {
            DateTime hoje = DateTime.Today;
            return _context.Entradas.Where(e => e.DataCadastro >= hoje && e.DataCadastro < hoje.AddDays(1)).ToList();
        }
        public List<EntradaModel> BuscarTodosRelatorio()
        {
            return _context.Entradas.ToList();
        }
    }
}
