using Analise.Data;
using Analise.Enuns;
using Analise.Helper;
using Analise.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace Analise.Repositorio
{
    public class AlunoRepositorio : IAlunoRepositorio
    {
        private readonly BancoContext _context;
        
        public AlunoRepositorio(BancoContext bancoContext)
        {
            this._context = bancoContext;           
        }
        public AlunoModel ListarPorId(int id)
        {
            return _context.Alunos.FirstOrDefault(x => x.Id == id);
        }
        public AlunoModel Adicionar(AlunoModel registo)
        {
            registo.DataCadastro = DateTime.Now;
            _context.Alunos.Add(registo);
            _context.SaveChanges();
            return registo;
        }
        public AlunoModel Actualizar(AlunoModel registo)
        {
            AlunoModel registoDB = ListarPorId(registo.Id);
            if (registoDB == null) throw new System.Exception("Erro na actualização!");
            registoDB.Nome = registo.Nome;
            registoDB.Nascimento = registo.Nascimento;
            registoDB.Documento = registo.Documento;
            registoDB.Contacto = registo.Contacto;
            registoDB.Pai = registo.Pai;
            registoDB.Mae = registo.Mae;
            registoDB.EstadoId = registo.EstadoId;

            _context.Alunos.Update(registoDB);
            _context.SaveChanges();
            return registoDB;
        }
        public List<AlunoModel> BuscarTodos()
        {
            return _context.Alunos.ToList();
        }
    }
}
