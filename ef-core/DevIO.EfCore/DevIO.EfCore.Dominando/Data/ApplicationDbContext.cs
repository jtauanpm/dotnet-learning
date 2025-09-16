using DevIO.EfCore.Dominando.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace DevIO.EfCore.Dominando.Data;

public class ApplicationDbContext : DbContext
{
    // private readonly StreamWriter _writer = new("log_ef_core.txt", append: true);
    public DbSet<Departamento> Departamentos { get; set; }
    public DbSet<Funcionario> Funcionarios { get; set; }
    public DbSet<Estado> Estados { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connString = Configuration.Configuration.GetConfiguration()["ConnectionStrings:Dev_IO_EfCore_Dominando"];

        // optionsBuilder.UseSqlServer(connString, builder => builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
        optionsBuilder.UseSqlServer(connString, options => 
                options.MaxBatchSize(100).EnableRetryOnFailure(2, TimeSpan.FromSeconds(1), null))
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
            .LogTo(Console.WriteLine, LogLevel.Information);
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
        // modelBuilder.Entity<Departamento>().HasQueryFilter(d => !d.Excluido);
        
        #region Collations

        // modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AI");
        // modelBuilder.Entity<Departamento>().Property(d => d.Descricao).UseCollation("SQL_Latin1_General_CP1_CS_AS");

        #endregion
        
        modelBuilder.Entity<Estado>()
            .HasData(new Estado {Id = 1, Nome = "São Paulo"}, new Estado {Id = 2, Nome = "Paraíba"});
        
        base.OnModelCreating(modelBuilder);
    }
}