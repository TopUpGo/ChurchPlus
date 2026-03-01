using Analise.Data;
using Analise.Enuns;
using Analise.Helper;
using Analise.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace Analise.Repositorio
{
    public class TurmaRepositorio : ITurmaRepositorio
    {
        private readonly BancoContext _context;
        
        public TurmaRepositorio(BancoContext bancoContext)
        {
            this._context = bancoContext;           
        }
        public TurmaModel ListarPorId(int id)
        {
            return _context.Turmas.FirstOrDefault(x => x.Id == id);
        }
        public TurmaModel Adicionar(TurmaModel registo)
        {
            registo.DataCadastro = DateTime.Now;
            _context.Turmas.Add(registo);
            _context.SaveChanges();
            return registo;
        }
        public TurmaModel Actualizar(TurmaModel registo)
        {
            TurmaModel registoDB = ListarPorId(registo.Id);
            if (registoDB == null) throw new System.Exception("Erro na actualização!");
            registoDB.Nome = registo.Nome;
            registoDB.Professor1 = registo.Professor1;
            registoDB.Professor3 = registo.Professor3;
            registoDB.Professor2 = registo.Professor2;
            registoDB.AgenciaId = registo.AgenciaId;

            _context.Turmas.Update(registoDB);
            _context.SaveChanges();
            return registoDB;
        }
        public List<TurmaModel> BuscarTodos()
        {
            return _context.Turmas.ToList();
        }
    }
}
