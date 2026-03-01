using Analise.Enuns;
using System.ComponentModel.DataAnnotations;

namespace Analise.Models
{
    public class TicketModel
    {
        public int Id { get; set; }

        public string? Usuario { get; set; }   // Login do usuário
        public string? Email { get; set; }     // Email do usuário
        public string? Assunto { get; set; }     // Senha gerada
        public string? Problema { get; set; }     // Senha gerada
        public string? Solucao { get; set; }     // Senha gerada
        public string? Estado { get; set; }     // Senha gerada
        public DateTimeOffset DataCriacao { get; set; } // Data do ticket
    }
}
