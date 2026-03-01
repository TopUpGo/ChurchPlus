using Analise.Data;
using Analise.Enuns;
using Analise.Helper;
using Analise.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace Analise.Repositorio
{
    public class CadastroRepositorio : ICadastroRepositorio
    {
        private readonly BancoContext _context;
        
        public CadastroRepositorio(BancoContext bancoContext)
        {
            this._context = bancoContext;           
        }
        public CadastroModel ListarPorId(int id)
        {
            return _context.Cadastros.FirstOrDefault(x => x.Id == id);
        }
        public CadastroModel Adicionar(CadastroModel registo)
        {
            registo.DataCadastro = DateTime.Now;
            _context.Cadastros.Add(registo);
            _context.SaveChanges();
            return registo;
        }
        public CadastroModel Actualizar(CadastroModel registo)
        {
            CadastroModel registoDB = ListarPorId(registo.Id);
            if (registoDB == null) throw new System.Exception("Erro na actualização!");
            registoDB.Nome = registo.Nome;
            registoDB.Nascimento = registo.Nascimento;
            registoDB.Documento = registo.Documento;
            registoDB.Contacto = registo.Contacto;
            registoDB.Contribuinte = registo.Contribuinte;
            registoDB.Obreiro = registo.Obreiro;
            registoDB.Cliente = registo.Cliente;
            registoDB.Fornecedor = registo.Fornecedor;
            registoDB.Professor = registo.Professor;

            _context.Cadastros.Update(registoDB);
            _context.SaveChanges();
            return registoDB;
        }
        public List<CadastroModel> BuscarTodos()
        {
            return _context.Cadastros.OrderBy(t => t.Nome).ToList();
        }
    }
}
