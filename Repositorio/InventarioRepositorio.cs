using Analise.Data;
using Analise.Enuns;
using Analise.Helper;
using Analise.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace Analise.Repositorio
{
    public class InventarioRepositorio : IInventarioRepositorio
    {
        private readonly BancoContext _context;
        
        public InventarioRepositorio(BancoContext bancoContext)
        {
            this._context = bancoContext;           
        }
        public InventarioModel ListarPorId(int id)
        {
            return _context.Inventarios.FirstOrDefault(x => x.Id == id);
        }
        public InventarioModel Adicionar(InventarioModel registo)
        {
            registo.DataCadastro = DateTime.Now;
            _context.Inventarios.Add(registo);
            _context.SaveChanges();
            return registo;
        }
        public InventarioModel Actualizar(InventarioModel registo)
        {
            InventarioModel registoDB = ListarPorId(registo.Id);
            if (registoDB == null) throw new System.Exception("Erro na actualização!");
            registoDB.Nome = registo.Nome;
            registoDB.Custo = registo.Custo;
            registoDB.ValorActual = registo.ValorActual;
            registoDB.AnoAquisicao = registo.AnoAquisicao;

            _context.Inventarios.Update(registoDB);
            _context.SaveChanges();
            return registoDB;
        }
        public List<InventarioModel> BuscarTodos()
        {
            return _context.Inventarios.ToList();
        }
    }
}
