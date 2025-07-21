using DevIO.IntroducaoEfCore.App.Domain;
using Microsoft.EntityFrameworkCore;

namespace DevIO.IntroducaoEfCore.App.Data;

public class ApplicationContext : DbContext
{
    public DbSet<Pedido> Pedido { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //TODO: place it on a secret store
        optionsBuilder.UseSqlServer();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //TODO: Estudar e anotar sobre Assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
    }
}