using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Tab.Migrations
{
    /// <inheritdoc />
    public partial class CriarPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Representante = table.Column<string>(type: "text", nullable: false),
                    Endereco = table.Column<string>(type: "text", nullable: false),
                    Credito = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Debito = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agencias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cadastros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Nascimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Documento = table.Column<string>(type: "text", nullable: true),
                    Contacto = table.Column<string>(type: "text", nullable: true),
                    Contribuinte = table.Column<string>(type: "text", nullable: true),
                    Obreiro = table.Column<string>(type: "text", nullable: true),
                    Cliente = table.Column<string>(type: "text", nullable: true),
                    Fornecedor = table.Column<string>(type: "text", nullable: true),
                    Professor = table.Column<string>(type: "text", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cadastros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cargos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Cargo = table.Column<string>(type: "text", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cargos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Credito = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Debito = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "estados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Estado = table.Column<string>(type: "text", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Linhas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Credito = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Debito = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Linhas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Usuario = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Assunto = table.Column<string>(type: "text", nullable: true),
                    Problema = table.Column<string>(type: "text", nullable: true),
                    Solucao = table.Column<string>(type: "text", nullable: true),
                    Estado = table.Column<string>(type: "text", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoDespesas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDespesas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoEntradas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoEntradas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoSaidas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoSaidas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoServicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoServicos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Perfil = table.Column<int>(type: "integer", nullable: false),
                    Senha = table.Column<string>(type: "text", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataActualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Criar = table.Column<string>(type: "text", nullable: false),
                    Editar = table.Column<string>(type: "text", nullable: false),
                    Visualizar = table.Column<string>(type: "text", nullable: false),
                    Administrar = table.Column<string>(type: "text", nullable: false),
                    AgenciaId = table.Column<int>(type: "integer", nullable: true),
                    CargoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Agencias_AgenciaId",
                        column: x => x.AgenciaId,
                        principalTable: "Agencias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Usuarios_cargos_CargoId",
                        column: x => x.CargoId,
                        principalTable: "cargos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: true),
                    Nascimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Documento = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Contacto = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Pai = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Mae = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EstadoId = table.Column<int>(type: "integer", nullable: true),
                    UsuarioId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alunos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Alunos_estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "estados",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Canais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Conta = table.Column<string>(type: "text", nullable: false),
                    Nib = table.Column<string>(type: "text", nullable: false),
                    Titular = table.Column<string>(type: "text", nullable: false),
                    Banco = table.Column<string>(type: "text", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataActualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Canais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Canais_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Nuit = table.Column<string>(type: "text", nullable: false),
                    Telefone = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    Pais = table.Column<string>(type: "text", nullable: false),
                    Endereco = table.Column<string>(type: "text", nullable: false),
                    Pep = table.Column<string>(type: "text", nullable: false),
                    DetalhePep = table.Column<string>(type: "text", nullable: false),
                    Nascimento = table.Column<string>(type: "text", nullable: false),
                    Segmento = table.Column<string>(type: "text", nullable: false),
                    Registo = table.Column<string>(type: "text", nullable: false),
                    Banco = table.Column<string>(type: "text", nullable: false),
                    Conta = table.Column<string>(type: "text", nullable: false),
                    NIB = table.Column<string>(type: "text", nullable: false),
                    Titular = table.Column<string>(type: "text", nullable: false),
                    Limite = table.Column<decimal>(type: "numeric", nullable: false),
                    Prazo = table.Column<decimal>(type: "numeric", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataActualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: true),
                    AgenciaId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clientes_Agencias_AgenciaId",
                        column: x => x.AgenciaId,
                        principalTable: "Agencias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Clientes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CobrarContas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CadastroId = table.Column<int>(type: "integer", nullable: true),
                    AgenciaId = table.Column<int>(type: "integer", nullable: true),
                    Valor = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Pago = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CobrarContas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CobrarContas_Agencias_AgenciaId",
                        column: x => x.AgenciaId,
                        principalTable: "Agencias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CobrarContas_Cadastros_CadastroId",
                        column: x => x.CadastroId,
                        principalTable: "Cadastros",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CobrarContas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EntradaEventos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventoId = table.Column<int>(type: "integer", nullable: true),
                    CadastroId = table.Column<int>(type: "integer", nullable: true),
                    Valor = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntradaEventos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntradaEventos_Cadastros_CadastroId",
                        column: x => x.CadastroId,
                        principalTable: "Cadastros",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EntradaEventos_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EntradaEventos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Entradas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TipoEntradaId = table.Column<int>(type: "integer", nullable: true),
                    LinhaId = table.Column<int>(type: "integer", nullable: true),
                    ContaId = table.Column<int>(type: "integer", nullable: true),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    Valor = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CadastroId = table.Column<int>(type: "integer", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AgenciaId = table.Column<int>(type: "integer", nullable: true),
                    UsuarioId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entradas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entradas_Agencias_AgenciaId",
                        column: x => x.AgenciaId,
                        principalTable: "Agencias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Entradas_Cadastros_CadastroId",
                        column: x => x.CadastroId,
                        principalTable: "Cadastros",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Entradas_Contas_ContaId",
                        column: x => x.ContaId,
                        principalTable: "Contas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Entradas_Linhas_LinhaId",
                        column: x => x.LinhaId,
                        principalTable: "Linhas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Entradas_TipoEntradas_TipoEntradaId",
                        column: x => x.TipoEntradaId,
                        principalTable: "TipoEntradas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Entradas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Extratos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    Credito = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Debito = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CanalId = table.Column<int>(type: "integer", nullable: true),
                    LinhaId = table.Column<int>(type: "integer", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: true),
                    AgenciaId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Extratos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Extratos_Agencias_AgenciaId",
                        column: x => x.AgenciaId,
                        principalTable: "Agencias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Extratos_Contas_CanalId",
                        column: x => x.CanalId,
                        principalTable: "Contas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Extratos_Linhas_LinhaId",
                        column: x => x.LinhaId,
                        principalTable: "Linhas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Extratos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Fechos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: true),
                    AgenciaId = table.Column<int>(type: "integer", nullable: true),
                    EstadoId = table.Column<int>(type: "integer", nullable: true),
                    Caixa = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    BCI1 = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    BCI2 = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Mpesa = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Construcao = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Corrente = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Inventario = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Cobranca = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Passivo = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Maputo = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Manhica = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Romao = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Mahubo = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Maguaza = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Fevereiro = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Maluana = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Malhampsene = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fechos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fechos_Agencias_AgenciaId",
                        column: x => x.AgenciaId,
                        principalTable: "Agencias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Fechos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Fechos_estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "estados",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Inventarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Custo = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ValorActual = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    AnoAquisicao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TabernaculoId = table.Column<int>(type: "integer", nullable: true),
                    AgenciaId = table.Column<int>(type: "integer", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventarios_Agencias_AgenciaId",
                        column: x => x.AgenciaId,
                        principalTable: "Agencias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inventarios_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PagarContas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CadastroId = table.Column<int>(type: "integer", nullable: true),
                    AgenciaId = table.Column<int>(type: "integer", nullable: true),
                    Valor = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Pago = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagarContas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PagarContas_Agencias_AgenciaId",
                        column: x => x.AgenciaId,
                        principalTable: "Agencias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PagarContas_Cadastros_CadastroId",
                        column: x => x.CadastroId,
                        principalTable: "Cadastros",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PagarContas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ParticipanteEventos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventoId = table.Column<int>(type: "integer", nullable: true),
                    CadastroId = table.Column<int>(type: "integer", nullable: true),
                    Valor = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Pago = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipanteEventos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParticipanteEventos_Cadastros_CadastroId",
                        column: x => x.CadastroId,
                        principalTable: "Cadastros",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ParticipanteEventos_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ParticipanteEventos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Saidas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TipoDespesaId = table.Column<int>(type: "integer", nullable: true),
                    LinhaId = table.Column<int>(type: "integer", nullable: true),
                    ContaId = table.Column<int>(type: "integer", nullable: true),
                    DataReferencia = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    Valor = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Quantidade = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CadastroId = table.Column<int>(type: "integer", nullable: true),
                    TipoServicoId = table.Column<int>(type: "integer", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AgenciaId = table.Column<int>(type: "integer", nullable: true),
                    UsuarioId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Saidas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Saidas_Agencias_AgenciaId",
                        column: x => x.AgenciaId,
                        principalTable: "Agencias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Saidas_Cadastros_CadastroId",
                        column: x => x.CadastroId,
                        principalTable: "Cadastros",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Saidas_Contas_ContaId",
                        column: x => x.ContaId,
                        principalTable: "Contas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Saidas_Linhas_LinhaId",
                        column: x => x.LinhaId,
                        principalTable: "Linhas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Saidas_TipoDespesas_TipoDespesaId",
                        column: x => x.TipoDespesaId,
                        principalTable: "TipoDespesas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Saidas_TipoServicos_TipoServicoId",
                        column: x => x.TipoServicoId,
                        principalTable: "TipoServicos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Saidas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Turmas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: true),
                    Professor1 = table.Column<int>(type: "integer", nullable: true),
                    Cadastro1Id = table.Column<int>(type: "integer", nullable: false),
                    Professor2 = table.Column<int>(type: "integer", nullable: true),
                    Cadastro2Id = table.Column<int>(type: "integer", nullable: false),
                    Professor3 = table.Column<int>(type: "integer", nullable: true),
                    Cadastro3Id = table.Column<int>(type: "integer", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AgenciaId = table.Column<int>(type: "integer", nullable: true),
                    UsuarioId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turmas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Turmas_Agencias_AgenciaId",
                        column: x => x.AgenciaId,
                        principalTable: "Agencias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Turmas_Cadastros_Cadastro1Id",
                        column: x => x.Cadastro1Id,
                        principalTable: "Cadastros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Turmas_Cadastros_Cadastro2Id",
                        column: x => x.Cadastro2Id,
                        principalTable: "Cadastros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Turmas_Cadastros_Cadastro3Id",
                        column: x => x.Cadastro3Id,
                        principalTable: "Cadastros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Turmas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Recebimentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClienteId = table.Column<int>(type: "integer", nullable: true),
                    Valor = table.Column<decimal>(type: "numeric", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataActualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: true),
                    AgenciaId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recebimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recebimentos_Agencias_AgenciaId",
                        column: x => x.AgenciaId,
                        principalTable: "Agencias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Recebimentos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Recebimentos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Licaos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TurmaId = table.Column<int>(type: "integer", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: true),
                    UsuarioId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licaos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Licaos_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turmas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Licaos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Listas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AlunoId = table.Column<int>(type: "integer", nullable: true),
                    TurmaId = table.Column<int>(type: "integer", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Presenca = table.Column<decimal>(type: "numeric", nullable: true),
                    Falta = table.Column<decimal>(type: "numeric", nullable: true),
                    Classificacao = table.Column<string>(type: "text", nullable: true),
                    EstadoId = table.Column<int>(type: "integer", nullable: true),
                    UsuarioId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Listas_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Listas_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turmas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Listas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Listas_estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "estados",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Aulas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AlunoId = table.Column<int>(type: "integer", nullable: true),
                    LicaoId = table.Column<int>(type: "integer", nullable: true),
                    TurmaId = table.Column<int>(type: "integer", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Presenca = table.Column<decimal>(type: "numeric", nullable: true),
                    Professor = table.Column<int>(type: "integer", nullable: true),
                    CadastroId = table.Column<int>(type: "integer", nullable: false),
                    Adjunto1 = table.Column<int>(type: "integer", nullable: true),
                    Cadastro1Id = table.Column<int>(type: "integer", nullable: false),
                    Adjunto2 = table.Column<int>(type: "integer", nullable: true),
                    Cadastro2Id = table.Column<int>(type: "integer", nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aulas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aulas_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Aulas_Cadastros_Cadastro1Id",
                        column: x => x.Cadastro1Id,
                        principalTable: "Cadastros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Aulas_Cadastros_Cadastro2Id",
                        column: x => x.Cadastro2Id,
                        principalTable: "Cadastros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Aulas_Cadastros_CadastroId",
                        column: x => x.CadastroId,
                        principalTable: "Cadastros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Aulas_Licaos_LicaoId",
                        column: x => x.LicaoId,
                        principalTable: "Licaos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Aulas_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turmas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Aulas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_EstadoId",
                table: "Alunos",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_UsuarioId",
                table: "Alunos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Aulas_AlunoId",
                table: "Aulas",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Aulas_Cadastro1Id",
                table: "Aulas",
                column: "Cadastro1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Aulas_Cadastro2Id",
                table: "Aulas",
                column: "Cadastro2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Aulas_CadastroId",
                table: "Aulas",
                column: "CadastroId");

            migrationBuilder.CreateIndex(
                name: "IX_Aulas_LicaoId",
                table: "Aulas",
                column: "LicaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Aulas_TurmaId",
                table: "Aulas",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Aulas_UsuarioId",
                table: "Aulas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Canais_UsuarioId",
                table: "Canais",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_AgenciaId",
                table: "Clientes",
                column: "AgenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_UsuarioId",
                table: "Clientes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_CobrarContas_AgenciaId",
                table: "CobrarContas",
                column: "AgenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_CobrarContas_CadastroId",
                table: "CobrarContas",
                column: "CadastroId");

            migrationBuilder.CreateIndex(
                name: "IX_CobrarContas_UsuarioId",
                table: "CobrarContas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_EntradaEventos_CadastroId",
                table: "EntradaEventos",
                column: "CadastroId");

            migrationBuilder.CreateIndex(
                name: "IX_EntradaEventos_EventoId",
                table: "EntradaEventos",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_EntradaEventos_UsuarioId",
                table: "EntradaEventos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Entradas_AgenciaId",
                table: "Entradas",
                column: "AgenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Entradas_CadastroId",
                table: "Entradas",
                column: "CadastroId");

            migrationBuilder.CreateIndex(
                name: "IX_Entradas_ContaId",
                table: "Entradas",
                column: "ContaId");

            migrationBuilder.CreateIndex(
                name: "IX_Entradas_LinhaId",
                table: "Entradas",
                column: "LinhaId");

            migrationBuilder.CreateIndex(
                name: "IX_Entradas_TipoEntradaId",
                table: "Entradas",
                column: "TipoEntradaId");

            migrationBuilder.CreateIndex(
                name: "IX_Entradas_UsuarioId",
                table: "Entradas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Extratos_AgenciaId",
                table: "Extratos",
                column: "AgenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Extratos_CanalId",
                table: "Extratos",
                column: "CanalId");

            migrationBuilder.CreateIndex(
                name: "IX_Extratos_LinhaId",
                table: "Extratos",
                column: "LinhaId");

            migrationBuilder.CreateIndex(
                name: "IX_Extratos_UsuarioId",
                table: "Extratos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Fechos_AgenciaId",
                table: "Fechos",
                column: "AgenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Fechos_EstadoId",
                table: "Fechos",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Fechos_UsuarioId",
                table: "Fechos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_AgenciaId",
                table: "Inventarios",
                column: "AgenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_UsuarioId",
                table: "Inventarios",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Licaos_TurmaId",
                table: "Licaos",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Licaos_UsuarioId",
                table: "Licaos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Listas_AlunoId",
                table: "Listas",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Listas_EstadoId",
                table: "Listas",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Listas_TurmaId",
                table: "Listas",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Listas_UsuarioId",
                table: "Listas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_PagarContas_AgenciaId",
                table: "PagarContas",
                column: "AgenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_PagarContas_CadastroId",
                table: "PagarContas",
                column: "CadastroId");

            migrationBuilder.CreateIndex(
                name: "IX_PagarContas_UsuarioId",
                table: "PagarContas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipanteEventos_CadastroId",
                table: "ParticipanteEventos",
                column: "CadastroId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipanteEventos_EventoId",
                table: "ParticipanteEventos",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipanteEventos_UsuarioId",
                table: "ParticipanteEventos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Recebimentos_AgenciaId",
                table: "Recebimentos",
                column: "AgenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Recebimentos_ClienteId",
                table: "Recebimentos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Recebimentos_UsuarioId",
                table: "Recebimentos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Saidas_AgenciaId",
                table: "Saidas",
                column: "AgenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Saidas_CadastroId",
                table: "Saidas",
                column: "CadastroId");

            migrationBuilder.CreateIndex(
                name: "IX_Saidas_ContaId",
                table: "Saidas",
                column: "ContaId");

            migrationBuilder.CreateIndex(
                name: "IX_Saidas_LinhaId",
                table: "Saidas",
                column: "LinhaId");

            migrationBuilder.CreateIndex(
                name: "IX_Saidas_TipoDespesaId",
                table: "Saidas",
                column: "TipoDespesaId");

            migrationBuilder.CreateIndex(
                name: "IX_Saidas_TipoServicoId",
                table: "Saidas",
                column: "TipoServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Saidas_UsuarioId",
                table: "Saidas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_AgenciaId",
                table: "Turmas",
                column: "AgenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_Cadastro1Id",
                table: "Turmas",
                column: "Cadastro1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_Cadastro2Id",
                table: "Turmas",
                column: "Cadastro2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_Cadastro3Id",
                table: "Turmas",
                column: "Cadastro3Id");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_UsuarioId",
                table: "Turmas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_AgenciaId",
                table: "Usuarios",
                column: "AgenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_CargoId",
                table: "Usuarios",
                column: "CargoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aulas");

            migrationBuilder.DropTable(
                name: "Canais");

            migrationBuilder.DropTable(
                name: "CobrarContas");

            migrationBuilder.DropTable(
                name: "EntradaEventos");

            migrationBuilder.DropTable(
                name: "Entradas");

            migrationBuilder.DropTable(
                name: "Extratos");

            migrationBuilder.DropTable(
                name: "Fechos");

            migrationBuilder.DropTable(
                name: "Inventarios");

            migrationBuilder.DropTable(
                name: "Listas");

            migrationBuilder.DropTable(
                name: "PagarContas");

            migrationBuilder.DropTable(
                name: "ParticipanteEventos");

            migrationBuilder.DropTable(
                name: "Recebimentos");

            migrationBuilder.DropTable(
                name: "Saidas");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "TipoSaidas");

            migrationBuilder.DropTable(
                name: "Licaos");

            migrationBuilder.DropTable(
                name: "TipoEntradas");

            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Contas");

            migrationBuilder.DropTable(
                name: "Linhas");

            migrationBuilder.DropTable(
                name: "TipoDespesas");

            migrationBuilder.DropTable(
                name: "TipoServicos");

            migrationBuilder.DropTable(
                name: "Turmas");

            migrationBuilder.DropTable(
                name: "estados");

            migrationBuilder.DropTable(
                name: "Cadastros");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Agencias");

            migrationBuilder.DropTable(
                name: "cargos");
        }
    }
}
