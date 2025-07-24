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
            .UseSqlServer("",
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
        MapearPropriedadesEsquecidas(modelBuilder);
    }

    private void MapearPropriedadesEsquecidas(ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            // Obtém todas as propriedades do tipo string
            var properties = entity.GetProperties().Where(p => p.ClrType == typeof(string));
            foreach (var property in properties)
            {
                // Verifica se nenhum tipo foi passado para a coluna e se foi setado algum valor máximo para a propriedade
                if (string.IsNullOrEmpty(property.GetColumnType()) && !property.GetMaxLength().HasValue)
                {
                    // property.SetMaxLength(100);
                    property.SetColumnType("VARCHAR(100)");
                }
            }
        }
    }
}