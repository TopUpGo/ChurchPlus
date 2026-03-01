using Analise.Data;
using Analise.Enuns;
using Analise.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace Analise.Repositorio
{
    public class EventoRepositorio : IEventoRepositorio
    {
        private readonly BancoContext _context;
        public EventoRepositorio(BancoContext bancoContext)
        {
            this._context = bancoContext;
        }
        public EventoModel ListarPorId(int id)
        {
            return _context.Eventos.FirstOrDefault(x => x.Id == id);
        }
        public EventoModel Adicionar(EventoModel registo)
        {
            registo.DataCadastro = DateTime.Now;
            _context.Eventos.Add(registo);
            _context.SaveChanges();
            return registo;
        }
        public EventoModel Actualizar(EventoModel registo)
        {
            EventoModel registoDB = ListarPorId(registo.Id);
            if (registoDB == null) throw new System.Exception("Erro na actualização!");
            registoDB.Nome = registo.Nome;

            _context.Eventos.Update(registoDB);
            _context.SaveChanges();
            return registoDB;
        }
        public List<EventoModel> BuscarTodos()
        {
            return _context.Eventos.ToList();
        }
    }
}
