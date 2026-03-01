using Analise.Data;
using Analise.Enuns;
using Analise.Helper;
using Analise.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace Analise.Repositorio
{
    public class EntradaEventoRepositorio : IEntradaEventoRepositorio
    {
        private readonly BancoContext _context;
        
        public EntradaEventoRepositorio(BancoContext bancoContext)
        {
            this._context = bancoContext;           
        }
        public EntradaEventoModel ListarPorId(int id)
        {
            return _context.EntradaEventos.FirstOrDefault(x => x.Id == id);
        }
        public EntradaEventoModel Adicionar(EntradaEventoModel registo)
        {
            registo.DataCadastro = DateTime.Now;
            _context.EntradaEventos.Add(registo);
            _context.SaveChanges();
            return registo;
        }
        public EntradaEventoModel Actualizar(EntradaEventoModel registo)
        {
            EntradaEventoModel registoDB = ListarPorId(registo.Id);
            if (registoDB == null) throw new System.Exception("Erro na actualização!");
            registoDB.EventoId = registo.EventoId;
            registoDB.CadastroId = registo.CadastroId;
            registoDB.Valor = registo.Valor;

            _context.EntradaEventos.Update(registoDB);
            _context.SaveChanges();
            return registoDB;
        }
        public List<EntradaEventoModel> BuscarTodos()
        {
            return _context.EntradaEventos.ToList();
        }
    }
}
