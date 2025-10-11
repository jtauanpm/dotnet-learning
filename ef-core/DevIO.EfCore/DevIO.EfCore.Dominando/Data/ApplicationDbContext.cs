using System.Reflection;
using DevIO.EfCore.Dominando.Configurations;
using DevIO.EfCore.Dominando.Converters;
using DevIO.EfCore.Dominando.Domain;
using DevIO.EfCore.Dominando.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;

namespace DevIO.EfCore.Dominando.Data;

public class ApplicationDbContext : DbContext
{
    // private readonly StreamWriter _writer = new("log_ef_core.txt", append: true);
    public DbSet<Departamento> Departamentos { get; set; }
    public DbSet<Funcionario> Funcionarios { get; set; }
    public DbSet<Estado> Estados { get; set; }
    public DbSet<Dictionary<string, object>> Configuracoes => Set<Dictionary<string, object>>("Configuracoes");
    
    public DbSet<Atributo> Atributos { get; set; }
    
    // public DbSet<Aeroporto> Aeroportos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connString = Configuration.GetConfiguration()["ConnectionStrings:Dev_IO_EfCore_Dominando"];

        // optionsBuilder.UseSqlServer(connString, builder => builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
        optionsBuilder.UseSqlServer(connString, options => 
                options.MaxBatchSize(100).EnableRetryOnFailure(2, TimeSpan.FromSeconds(1), null))
            .EnableSensitiveDataLogging()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution)
            .EnableDetailedErrors()
            .LogTo(Console.WriteLine, LogLevel.Information)
            .AddInterceptors(new InterceptadorDeComandos(), new InterceptadorDeConexao(), new InterceptadorPersistencia());
            // .UseLazyLoadingProxies()
            // .LogTo(Console.WriteLine, new [] {CoreEventId.ContextInitialized, RelationalEventId.CommandExecuted},
            //     LogLevel.Information, DbContextLoggerOptions.LocalTime | DbContextLoggerOptions.SingleLine);
            // .LogTo(_writer.WriteLine, LogLevel.Information);
        
        base.OnConfiguring(optionsBuilder);
    }

    // public override void Dispose()
    // {
    //     base.Dispose();
    //     _writer.Dispose();
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.RegisterFunctions();
        
        // Register functions by FluentAPI
        modelBuilder
            .HasDbFunction(typeof(UserDefinedFunctions)
                .GetMethod(nameof(UserDefinedFunctions.Left),
                    new[] { typeof(string), typeof(int) })!)
            .IsBuiltIn();
        
        modelBuilder
            .HasDbFunction(typeof(UserDefinedFunctions)
                .GetMethod(nameof(UserDefinedFunctions.ConverterParaLetrasMaiusculas),
                    new[] { typeof(string) })!)
            .HasName("ConverterParaLetrasMaiusculas");
        
        modelBuilder
            .HasDbFunction(typeof(UserDefinedFunctions)
                .GetMethod(nameof(UserDefinedFunctions.DateDiff),
                    new[] { typeof(string) })!)
            .HasName("DATEDIFF")
            .HasTranslation(a =>
            {
                var args = a.ToList();
                var constante = (SqlConstantExpression)args[0];
                args[0] = new SqlFragmentExpression(constante.Value.ToString());

                return new SqlFunctionExpression("DATEDIFF", args, false, new[] { false, false, false }, typeof(int),
                    null);
            });
            
        // modelBuilder.Entity<Departamento>().HasQueryFilter(d => !d.Excluido);
        
        #region Collations

        // modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AI");
        // modelBuilder.Entity<Departamento>().Property(d => d.Descricao).UseCollation("SQL_Latin1_General_CP1_CS_AS");

        #endregion
        
        #region Sequences
        // modelBuilder.HasSequence<int>("MinhaSequencia", "sequencias")
        //     .StartsAt(1)
        //     .IncrementsBy(2)
        //     .HasMin(1)
        //     .HasMax(10)
        //     .IsCyclic();
        //
        // modelBuilder.Entity<Departamento>().Property(d => d.Id).HasDefaultValueSql("NEXT VALUE FOR sequencias.MinhaSequencia");
        #endregion

        #region Index

        // modelBuilder.Entity<Departamento>()
        //     .HasIndex(d => new { d.Descricao, d.Ativo })
        //     .HasDatabaseName("idx_meu_indice_composto")
        //     .HasFilter("Descricao IS NOT NULL")
        //     .HasFillFactor(80)
        //     .IsUnique();

        #endregion

        modelBuilder.Entity<Estado>()
            .HasData(new Estado {Id = 1, Nome = "São Paulo"}, new Estado {Id = 2, Nome = "Paraíba"});

        #region Schemas

        // modelBuilder.HasDefaultSchema("cadastro");
        // modelBuilder.Entity<Estado>()
        //     .ToTable("Estados", "SegundoEsquema");

        #endregion

        #region Converters

        // var converter = new ValueConverter<ContractType, string>(p => p.ToString(), p => Enum.Parse<ContractType>(p));
        
        var builtInConverter = new EnumToStringConverter<ContractType>();

        modelBuilder.Entity<Funcionario>()
            .Property(f => f.ContractType)
            .HasConversion(builtInConverter);
            // .HasConversion(p => p.ToString(), p => Enum.Parse<ContractType>(p));

            modelBuilder.Entity<Funcionario>()
                .Property(f => f.Gender)
                .HasConversion(new ConversorCustomizado());

        #endregion

        modelBuilder.Entity<Departamento>().Property<DateTime>("UltimaAtualizacao");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        modelBuilder.SharedTypeEntity<Dictionary<string, object>>("Configuracoes", c =>
        {
            c.Property<int>("Id");

            c.Property<string>("Chave")
                .HasColumnType("VARCHAR(40)")
                .IsRequired();

            c.Property<string>("Valor")
                .HasColumnType("VARCHAR(255)")
                .IsRequired();
        });
        
        base.OnModelCreating(modelBuilder);
    }
}