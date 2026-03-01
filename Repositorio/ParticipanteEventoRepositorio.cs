using Analise.Data;
using Analise.Enuns;
using Analise.Helper;
using Analise.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace Analise.Repositorio
{
    public class ParticipanteEventoRepositorio : IParticipanteEventoRepositorio
    {
        private readonly BancoContext _context;
        
        public ParticipanteEventoRepositorio(BancoContext bancoContext)
        {
            this._context = bancoContext;           
        }
        public ParticipanteEventoModel ListarPorId(int id)
        {
            return _context.ParticipanteEventos.FirstOrDefault(x => x.Id == id);
        }
        public ParticipanteEventoModel Adicionar(ParticipanteEventoModel registo)
        {
            registo.DataCadastro = DateTime.Now;
            _context.ParticipanteEventos.Add(registo);
            _context.SaveChanges();
            return registo;
        }
        public ParticipanteEventoModel Actualizar(ParticipanteEventoModel registo)
        {
            ParticipanteEventoModel registoDB = ListarPorId(registo.Id);
            if (registoDB == null) throw new System.Exception("Erro na actualização!");
            registoDB.EventoId = registo.EventoId;
            registoDB.CadastroId = registo.CadastroId;
            registoDB.Valor = registo.Valor;
            registoDB.Pago = registo.Pago;

            _context.ParticipanteEventos.Update(registoDB);
            _context.SaveChanges();
            return registoDB;
        }
        public List<ParticipanteEventoModel> BuscarTodos()
        {
            return _context.ParticipanteEventos.ToList();
        }
    }
}
