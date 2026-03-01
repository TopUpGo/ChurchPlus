using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Analise.Models;
using Analise.Data;
using Analise.Repositorio;
using Analise.Helper;
using Microsoft.Extensions.ObjectPool;

var builder = WebApplication.CreateBuilder(args);
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
// Registrar SMTP Settings
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SMTP"));

// Registrar serviço de email
builder.Services.AddTransient<EmailService>();
builder.Services.AddTransient<TicketRepositorio>();

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddEntityFrameworkSqlServer().AddDbContext<BancoContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DataBase")));
builder.WebHost.UseUrls("http://*:5285");
builder.Services.AddDbContext<BancoContext>(options =>options.UseNpgsql(builder.Configuration.GetConnectionString("DataBase")));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<ISessao, Sessao>();
builder.Services.AddScoped<IEmail, Email>();
builder.Services.AddScoped<IAgenciaRepositorio, AgenciaRepositorio>();
builder.Services.AddScoped<ICargoRepositorio, CargoRepositorio>();
builder.Services.AddScoped<IEstadoRepositorio, EstadoRepositorio>();
builder.Services.AddScoped<ITicketRepositorio, TicketRepositorio>();
builder.Services.AddScoped<ICadastroRepositorio, CadastroRepositorio>();
builder.Services.AddScoped<ITipoDespesaRepositorio, TipoDespesaRepositorio>();
builder.Services.AddScoped<ITipoSaidaRepositorio, TipoSaidaRepositorio>();
builder.Services.AddScoped<ITipoEntradaRepositorio, TipoEntradaRepositorio>();
builder.Services.AddScoped<ITipoServicoRepositorio, TipoServicoRepositorio>();
builder.Services.AddScoped<IEventoRepositorio, EventoRepositorio>();
builder.Services.AddScoped<IContaRepositorio, ContaRepositorio>();
builder.Services.AddScoped<ILinhaRepositorio, LinhaRepositorio>();
builder.Services.AddScoped<IContasPagarRepositorio, ContasPagarRepositorio>();
builder.Services.AddScoped<IContasCobrarRepositorio, ContasCobrarRepositorio>();
builder.Services.AddScoped<IInventarioRepositorio, InventarioRepositorio>();

builder.Services.AddScoped<IEntradaRepositorio, EntradaRepositorio>();
builder.Services.AddScoped<ISaidaRepositorio, SaidaRepositorio>();
builder.Services.AddScoped<ITurmaRepositorio, TurmaRepositorio>();
builder.Services.AddScoped<IListaRepositorio, ListaRepositorio>();
builder.Services.AddScoped<IAlunoRepositorio, AlunoRepositorio>();
builder.Services.AddScoped<ILicaoRepositorio, LicaoRepositorio>();
builder.Services.AddScoped<IAulaRepositorio, AulaRepositorio>();
builder.Services.AddScoped<IParticipanteEventoRepositorio, ParticipanteEventoRepositorio>();
builder.Services.AddScoped<IEntradaEventoRepositorio, EntradaEventoRepositorio>();
builder.Services.AddScoped<IFechoRepositorio, FechoRepositorio>();


builder.Services.AddSession(o =>
{
    o.Cookie.HttpOnly=true;
    o.Cookie.IsEssential=true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();