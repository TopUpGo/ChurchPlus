using Analise.Data;
using Analise.Enuns;
using Analise.Helper;
using Analise.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace Analise.Repositorio
{
    public class LicaoRepositorio : ILicaoRepositorio
    {
        private readonly BancoContext _context;
        
        public LicaoRepositorio(BancoContext bancoContext)
        {
            this._context = bancoContext;           
        }
        public LicaoModel ListarPorId(int id)
        {
            return _context.Licaos.FirstOrDefault(x => x.Id == id);
        }
        public LicaoModel Adicionar(LicaoModel registo)
        {
            registo.DataCadastro = DateTime.Now;
            _context.Licaos.Add(registo);
            _context.SaveChanges();
            return registo;
        }
        public LicaoModel Actualizar(LicaoModel registo)
        {
            LicaoModel registoDB = ListarPorId(registo.Id);
            if (registoDB == null) throw new System.Exception("Erro na actualização!");
            registoDB.Turma = registo.Turma;
            registoDB.Descricao = registo.Descricao;

            _context.Licaos.Update(registoDB);
            _context.SaveChanges();
            return registoDB;
        }
        public List<LicaoModel> BuscarTodos()
        {
            return _context.Licaos.ToList();
        }
    }
}
