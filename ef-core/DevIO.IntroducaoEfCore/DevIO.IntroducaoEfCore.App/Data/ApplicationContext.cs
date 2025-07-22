using DevIO.IntroducaoEfCore.App.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DevIO.IntroducaoEfCore.App.Data;

public class ApplicationContext : DbContext
{
    private static readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(p => p.AddConsole());
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
        optionsBuilder
            .UseLoggerFactory(_loggerFactory)
            .EnableSensitiveDataLogging()
            //TODO: place it on a secret store
            .UseSqlServer();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //TODO: Estudar e anotar sobre Assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
    }
}