using Analise.Data;
using Analise.Enuns;
using Analise.Helper;
using Analise.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace Analise.Repositorio
{
    public class FechoRepositorio : IFechoRepositorio
    {
        private readonly BancoContext _context;
        
        public FechoRepositorio(BancoContext bancoContext)
        {
            this._context = bancoContext;           
        }
        public FechoModel ListarPorId(int id)
        {
            return _context.Fechos.FirstOrDefault(x => x.Id == id);
        }       
        public List<FechoModel> BuscarTodosAbertura()
        {
            return _context.Fechos.Where(e => e.EstadoId == 1).ToList();
        }
        public List<FechoModel> BuscarTodosFecho()
        {
            return _context.Fechos.Where(e => e.EstadoId == 2).ToList();
        }
       
    }
}
