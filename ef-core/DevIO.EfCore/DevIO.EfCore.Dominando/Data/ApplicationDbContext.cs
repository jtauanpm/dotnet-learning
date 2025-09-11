using DevIO.EfCore.Dominando.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace DevIO.EfCore.Dominando.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Departamento> Departamentos { get; set; }
    public DbSet<Funcionario> Funcionarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connString = Configuration.Configuration.GetConfiguration()["ConnectionStrings:Dev_IO_EfCore_Dominando"];

        // optionsBuilder.UseSqlServer(connString, builder => builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
        optionsBuilder.UseSqlServer(connString)
            .EnableSensitiveDataLogging()
            // .UseLazyLoadingProxies()
            .LogTo(Console.WriteLine, new [] {CoreEventId.ContextInitialized, RelationalEventId.CommandExecuted},
                LogLevel.Information, DbContextLoggerOptions.LocalTime | DbContextLoggerOptions.SingleLine);
        
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // modelBuilder.Entity<Departamento>().HasQueryFilter(d => !d.Excluido);
    }
}