using Analise.Data;
using Analise.Enuns;
using Analise.Helper;
using Analise.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace Analise.Repositorio
{
    public class SaidaRepositorio : ISaidaRepositorio
    {
        private readonly BancoContext _context;
        
        public SaidaRepositorio(BancoContext bancoContext)
        {
            this._context = bancoContext;           
        }
        public SaidaModel ListarPorId(int id)
        {
            return _context.Saidas.FirstOrDefault(x => x.Id == id);
        }
        public SaidaModel Adicionar(SaidaModel registo)
        {
            registo.DataCadastro = DateTime.Now;
            _context.Saidas.Add(registo);
            _context.SaveChanges();
            return registo;
        }
        public SaidaModel Actualizar(SaidaModel registo)
        {
            SaidaModel registoDB = ListarPorId(registo.Id);
            if (registoDB == null) throw new System.Exception("Erro na actualização!");
            registoDB.TipoDespesaId = registo.TipoDespesaId;
            registoDB.DataReferencia = registo.DataReferencia;
            registoDB.LinhaId = registo.LinhaId;
            registoDB.ContaId = registo.ContaId;
            registoDB.Descricao = registo.Descricao;
            registoDB.Valor = registo.Valor;
            registoDB.Quantidade = registo.Quantidade;
            registoDB.CadastroId = registo.CadastroId;
            registoDB.AgenciaId = registo.AgenciaId;

            _context.Saidas.Update(registoDB);
            _context.SaveChanges();
            return registoDB;
        }
        public List<SaidaModel> BuscarTodos()
        {
            DateTime hoje = DateTime.Today;
            return _context.Saidas.Where(e => e.DataCadastro >= hoje && e.DataCadastro < hoje.AddDays(1)).ToList();
        }
        public List<SaidaModel> BuscarTodosRelatorio()
        {
            return _context.Saidas.ToList();
        }
    }
}
