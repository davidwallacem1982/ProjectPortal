using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Portal.Core.Entities;

namespace Portal.Infra.Configuration
{
    public class Context : DbContext
    {
        ////Construtor do Context
        public Context(DbContextOptions<Context> options) : base(options)
        {
            //Cria o Banco s não existir
            //Database.EnsureCreated();
        }
        /// <summary>
        /// Define o Log do EF para exibir os comandos SQL no output de Debug
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_myLoggerFactory);
            base.OnConfiguring(optionsBuilder);
        }
        public static readonly LoggerFactory _myLoggerFactory =
            new LoggerFactory(new[] {
            new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()
        });

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Arquivos>()
                .HasOne(p => p.Atendimentos)
                .WithMany(b => b.Arquivos)
                .HasForeignKey(p => p.AtendimentoID)
                .HasPrincipalKey(b => b.Id);

            modelBuilder.Entity<Arquivos>()
                .HasOne(p => p.Sinistros)
                .WithMany(b => b.Arquivos)
                .HasForeignKey(p => p.SinistroID)
                .HasPrincipalKey(b => b.Id);

            modelBuilder.Entity<CheckLists>()
                .HasOne(p => p.TiposArquivos)
                .WithMany(b => b.CheckLists)
                .HasForeignKey(p => p.TiposID)
                .HasPrincipalKey(b => b.Id);
        }

        // Documentos
        public DbSet<Documentos_CTEs> Documentos_CTEs { get; set; }
        public DbSet<Documentos_MDFEs> Documentos_MDFEs { get; set; }
        public DbSet<Documentos_NFEs> Documentos_NFEs { get; set; }
        public DbSet<Documentos_ATM> Documentos_ATM { get; set; }
        public DbSet<Documentos_ATM_Lotes> Documentos_ATM_Lotes { get; set; }

        // Cadastros
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Embarcadores> Embarcadores { get; set; }
        public DbSet<GRs> GRs { get; set; }
        public DbSet<Motoristas> Motoristas { get; set; }
        public DbSet<Transportadores> Transportadores { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Usuarios_IP> Usuarios_IP { get; set; }
        public DbSet<Veiculos> Veiculos { get; set; }
        public DbSet<Seguradoras> Seguradoras { get; set; }
        public DbSet<Produtores> Produtores { get; set; }
        public DbSet<Operacionais> Operacionais { get; set; }
        public DbSet<Unidades> Unidades { get; set; }
        public DbSet<Contatos> Contatos { get; set; }
        public DbSet<Rel_Contatos_TiposAssuntos> Rel_Contatos_TiposAssuntos { get; set; }
        public DbSet<TiposAssuntos> TiposAssuntos { get; set; }
        public DbSet<Rel_Usuarios_Produtores> Rel_Usuarios_Produtores { get; set; }

        // Cobrança
        public DbSet<Verificacoes> Verificacoes { get; set; }

        // Relacionamentos
        public DbSet<rel_CTs_MDFEs> rel_CTs_MDFEs { get; set; }
        public DbSet<rel_NFs_MDFEs> rel_NFs_MDFEs { get; set; }
        public DbSet<rel_NFs_CTEs> rel_NFs_CTEs { get; set; }
        public DbSet<Motoristas_Consultas> Motoristas_Consultas { get; set; }
        public DbSet<Veiculos_Consultas> Veiculos_Consultas { get; set; }
        public DbSet<rel_Usuarios_Unidades> rel_Usuarios_Unidades { get; set; }
        public DbSet<rel_Usuarios_Clientes> rel_Usuarios_Clientes { get; set; }
        public DbSet<Produtores_Clientes> Produtores_Clientes { get; set; }
        public DbSet<Clientes_GRs> Clientes_GRs { get; set; }

        // Seguros
        public DbSet<RamosSeguros> RamosSeguros { get; set; }
        public DbSet<Apolices> Apolices { get; set; }
        public DbSet<DDRs> DDRs { get; set; }

        // Sinistros/Atendimentos
        public DbSet<Sinistros> Sinistros { get; set; }
        public DbSet<Documentos_Sinistros> DocumentosSinistros { get; set; }
        public DbSet<Atendimentos> Atendimentos { get; set; }
        public DbSet<Documentos_Atendimentos> DocumentosAtendimentos { get; set; }
        public DbSet<Reguladoras> Reguladoras { get; set; }
        public DbSet<TiposDocumentos> TiposDocumentos { get; set; }
        public DbSet<TiposFretes> TiposFretes { get; set; }
        public DbSet<TiposLotacoes> TiposLotacoes { get; set; }
        public DbSet<TiposOperacoes> TiposOperacoes { get; set; }
        public DbSet<TiposPistas> TiposPistas { get; set; }
        public DbSet<TiposRodagens> TiposRodagens { get; set; }
        public DbSet<TiposSinistros> TiposSinistros { get; set; }
        public DbSet<TiposAtendimentos> TiposAtendimentos { get; set; }
        public DbSet<TiposSituacaosMotoristas> TiposSituacaosMotoristas { get; set; }
        public DbSet<TiposTempoPistas> TiposTempoPistas { get; set; }
        public DbSet<TiposVinculos> TiposVinculos { get; set; }
        public DbSet<TiposRastreadores> TiposRastreadores { get; set; }
        public DbSet<TiposSituacoes> TiposSituacoes { get; set; }
        public DbSet<TiposNaturezaSinistros> TiposNaturezaSinistros { get; set; }
        public DbSet<TiposTempoMotoristas> TiposTempoMotoristas { get; set; }
        public DbSet<TiposConservacaoPistas> TiposConservacaoPistas { get; set; }
        public DbSet<TiposConcessionadas> TiposConcessionadas { get; set; }
        public DbSet<TiposNiveis> TiposNiveis { get; set; }
        public DbSet<TiposCulpabilidadeMotoristas> TiposCulpabilidadeMotoristas { get; set; }
        public DbSet<Ufs> Ufs { get; set; }
        public DbSet<Cidades> Cidades { get; set; }
        public DbSet<Criticidades> Criticidades { get; set; }
        public DbSet<Historicos> Historicos { get; set; }
        public DbSet<Arquivos> Arquivos { get; set; }
        public DbSet<TiposArquivos> TiposArquivos { get; set; }
        public DbSet<EmailsClientes> EmailsClientes { get; set; }
        public DbSet<CamposBloqueados> CamposBloqueados { get; set; }

        // Acesso Menus
        public DbSet<Menus> Menus { get; set; }
        public DbSet<Menus_TipoAcesso> Menus_TipoAcesso { get; set; }
        public DbSet<TipoAcessos> TipoAcessos { get; set; }
        public DbSet<RegrasAcessos> RegrasAcessos { get; set; }

        // Faturamento
        public DbSet<Faturamentos> Faturamentos { get; set; }
        public DbSet<TiposProdutos> TiposProdutos { get; set; }
        public DbSet<Produtos> Produtos { get; set; }
        public DbSet<Cliente_Produtos> Cliente_Produtos { get; set; }
        public DbSet<Rel_Produtores_Clientes_Produtos> Rel_Produtores_Clientes_Produtos { get; set; }
        public DbSet<Rel_Seguradoras_Clientes_Produtos> Rel_Seguradoras_Clientes_Produtos { get; set; }
        public DbSet<Empresas> Empresas { get; set; }
        public DbSet<LancamentosFuturos> LancamentosFuturos { get; set; }
        public DbSet<TipoAverbacoes> TipoAverbacoes { get; set; }
        public DbSet<TipoRegraAverbacoes> TipoRegraAverbacoes { get; set; }
        public DbSet<TipoEmpresaAverbacoes> TipoEmpresaAverbacoes { get; set; }
        public DbSet<TipoAlteracoes> TipoAlteracoes { get; set; }
        public DbSet<TipoMotivoAlteracoes> TipoMotivoAlteracoes { get; set; }
        public DbSet<StatusArquivos> StatusArquivos { get; set; }

        // Usuario Cliente
        public DbSet<TiposSituacoesUsuarios> TiposSituacoesUsuarios { get; set; }

        // CheckList
        public DbSet<CheckLists> CheckLists { get; set; }

        // log
        public DbSet<Tabelas> Tabelas { get; set; }
        public DbSet<Campos> Campos { get; set; }
        public DbSet<Controllers> Controllers { get; set; }
        public DbSet<Methods> Methods { get; set; }
        public DbSet<HttpMethods> HttpMethods { get; set; }
        public DbSet<RegrasEnvioEmails> RegrasEnvioEmails { get; set; }
        public DbSet<Feriados> Feriados { get; set; }
        public DbSet<ExcecaoRegrasAlertaEmails> ExcecaoRegrasAlertaEmails { get; set; }

        //Contatos
        public DbSet<ContatosPortal> ContatosPortal { get; set; }
        public DbSet<ListaContatosPortal> ListaContatosPortal { get; set; }
        public DbSet<Cargo> Cargo { get; set; }
    }
}
