using Analise.Data;
using Analise.Enuns;
using Analise.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace Analise.Repositorio
{
    public class TicketRepositorio : ITicketRepositorio
    {
        private readonly BancoContext _context;

        public TicketRepositorio(BancoContext context)
        {
            _context = context;
        }

        public void Adicionar(TicketModel ticket)
        {
            _context.Tickets.Add(ticket);
            _context.SaveChanges();
        }
        public TicketModel ListarPorId(int id)
        {
            return _context.Tickets.FirstOrDefault(x => x.Id == id);
        }
        public List<TicketModel> BuscarTodos()
        {
            return _context.Tickets.ToList();
            //return _context.Tickets
            //       .Where(t => t.Estado != "Fechado")
            //       .ToList();
        }
        public List<TicketModel> BuscarFiltrados(string estado, DateTime? dataInicio, DateTime? dataFim)
        {
            var query = _context.Tickets.AsQueryable();

            if (!string.IsNullOrEmpty(estado))
                query = query.Where(t => t.Estado == estado);

            if (dataInicio.HasValue)
                query = query.Where(t => t.DataCriacao.Date >= dataInicio.Value.Date);

            if (dataFim.HasValue)
                query = query.Where(t => t.DataCriacao.Date <= dataFim.Value.Date);

            return query.ToList();
        }

        public TicketModel Actualizar(TicketModel registo)
        {
            TicketModel registoDB = ListarPorId(registo.Id);
            if (registoDB == null) throw new System.Exception("Erro na actualização do ticket!");
            registoDB.Solucao = registo.Solucao;
            registoDB.Estado = registo.Estado;
            //registoDB.DataCriacao = DateTime.Now;

            _context.Tickets.Update(registoDB);
            _context.SaveChanges();
            return registoDB;
        }
    }
}
