using Analise.Data;
using Analise.Enuns;
using Analise.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace Analise.Repositorio
{
    public class TipoServicoRepositorio : ITipoServicoRepositorio
    {
        private readonly BancoContext _context;
        public TipoServicoRepositorio(BancoContext bancoContext)
        {
            this._context = bancoContext;
        }
        public TipoServicoModel ListarPorId(int id)
        {
            return _context.TipoServicos.FirstOrDefault(x => x.Id == id);
        }
        public TipoServicoModel Adicionar(TipoServicoModel registo)
        {
            registo.DataCadastro = DateTime.Now;
            _context.TipoServicos.Add(registo);
            _context.SaveChanges();
            return registo;
        }
        public TipoServicoModel Actualizar(TipoServicoModel registo)
        {
            TipoServicoModel registoDB = ListarPorId(registo.Id);
            if (registoDB == null) throw new System.Exception("Erro na actualização!");
            registoDB.Nome = registo.Nome;

            _context.TipoServicos.Update(registoDB);
            _context.SaveChanges();
            return registoDB;
        }
        public List<TipoServicoModel> BuscarTodos()
        {
            return _context.TipoServicos.ToList();
        }
    }
}
