using Analise.Models;
using Microsoft.EntityFrameworkCore;

namespace Analise.Data
{
    public class BancoContext :DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {

        }
        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<AgenciaModel> Agencias { get; set; }
        public DbSet<CargoModel> cargos { get; set; }
        public DbSet<EstadoModel> estados { get; set; }
        public DbSet<TicketModel> Tickets { get; set; }
        public DbSet<CadastroModel> Cadastros { get; set; }
        public DbSet<ClienteModel> Clientes { get; set; }
        public DbSet<CanalModel> Canais { get; set; }
        public DbSet<ExtratoModel> Extratos { get; set; }
        public DbSet<RecebimentoModel> Recebimentos { get; set; }
        public DbSet<FechoModel> Fechos { get; set; }
        public DbSet<TipoDespesaModel> TipoDespesas { get; set; }
        public DbSet<TipoEntradaModel> TipoEntradas { get; set; }
        public DbSet<TipoSaidaModel> TipoSaidas { get; set; }
        public DbSet<TipoServicoModel> TipoServicos { get; set; }
        public DbSet<EventoModel> Eventos { get; set; }
        public DbSet<LinhaModel> Linhas { get; set; }
        public DbSet<ContaModel> Contas { get; set; }
        public DbSet<ContasCobrarModel> CobrarContas { get; set; }
        public DbSet<ContasPagarModel> PagarContas { get; set; }
        public DbSet<InventarioModel> Inventarios { get; set; }
        public DbSet<EntradaModel> Entradas { get; set; }
        public DbSet<SaidaModel> Saidas { get; set; }
        public DbSet<TurmaModel> Turmas { get; set; }
        public DbSet<ListaModel> Listas { get; set; }
        public DbSet<AlunoModel> Alunos { get; set; }
        public DbSet<LicaoModel> Licaos { get; set; }
        public DbSet<AulaModel> Aulas { get; set; }
        public DbSet<ParticipanteEventoModel> ParticipanteEventos { get; set; }
        public DbSet<EntradaEventoModel> EntradaEventos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new RegistoMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
