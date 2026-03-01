using Analise.Data;
using Analise.Enuns;
using Analise.Helper;
using Analise.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace Analise.Repositorio
{
    public class AulaRepositorio : IAulaRepositorio
    {
        private readonly BancoContext _context;
        
        public AulaRepositorio(BancoContext bancoContext)
        {
            this._context = bancoContext;           
        }
        public AulaModel ListarPorId(int id)
        {
            return _context.Aulas.FirstOrDefault(x => x.Id == id);
        }
        public AulaModel Adicionar(AulaModel registo)
        {
            registo.DataCadastro = DateTime.Now;
            _context.Aulas.Add(registo);
            _context.SaveChanges();
            return registo;
        }
        public AulaModel Actualizar(AulaModel registo)
        {
            AulaModel registoDB = ListarPorId(registo.Id);
            if (registoDB == null) throw new System.Exception("Erro na actualização!");
            registoDB.LicaoId = registo.LicaoId;
            registoDB.TurmaId = registo.TurmaId;
            registoDB.AlunoId = registo.AlunoId;
            registoDB.Presenca = registo.Presenca;
            registoDB.Professor = registo.Professor;
            registoDB.Adjunto1 = registo.Adjunto1;
            registoDB.Adjunto2 = registo.Adjunto2;

            _context.Aulas.Update(registoDB);
            _context.SaveChanges();
            return registoDB;
        }
        public List<AulaModel> BuscarTodos()
        {
            return _context.Aulas.ToList();
        }
    }
}
