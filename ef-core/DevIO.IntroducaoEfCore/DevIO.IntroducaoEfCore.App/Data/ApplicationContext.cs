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
            //TODO: place connectionString on a secret store
            .UseSqlServer("Server=localhost,1433;Database=Desenvolvedor_IO;User Id=sa;Password=Jordanna123.;Encrypt=False;",
                opt => opt.EnableRetryOnFailure(
                    maxRetryCount: 2, 
                    maxRetryDelay: TimeSpan.FromSeconds(5), 
                    errorNumbersToAdd: null).MigrationsHistoryTable("NomeTabelaMigrations", "schema")
                );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //TODO: Estudar e anotar sobre Assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
    }
}