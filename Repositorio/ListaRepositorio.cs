using Analise.Data;
using Analise.Enuns;
using Analise.Helper;
using Analise.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace Analise.Repositorio
{
    public class ListaRepositorio : IListaRepositorio
    {
        private readonly BancoContext _context;
        
        public ListaRepositorio(BancoContext bancoContext)
        {
            this._context = bancoContext;           
        }
        public ListaModel ListarPorId(int id)
        {
            return _context.Listas.FirstOrDefault(x => x.Id == id);
        }
        public ListaModel Adicionar(ListaModel registo)
        {
            registo.DataCadastro = DateTime.Now;
            _context.Listas.Add(registo);
            _context.SaveChanges();
            return registo;
        }
        public ListaModel Actualizar(ListaModel registo)
        {
            ListaModel registoDB = ListarPorId(registo.Id);
            if (registoDB == null) throw new System.Exception("Erro na actualização!");
            registoDB.AlunoId = registo.AlunoId;
            registoDB.TurmaId = registo.TurmaId;
            registoDB.Presenca = registo.Presenca;
            registoDB.Falta = registo.Falta;
            registoDB.Classificacao = registo.Classificacao;
            registoDB.EstadoId = registo.EstadoId;

            _context.Listas.Update(registoDB);
            _context.SaveChanges();
            return registoDB;
        }
        public List<ListaModel> BuscarTodos()
        {
            return _context.Listas.ToList();
        }
    }
}
