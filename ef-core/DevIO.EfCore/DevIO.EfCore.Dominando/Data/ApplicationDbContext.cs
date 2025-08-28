using DevIO.EfCore.Dominando.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DevIO.EfCore.Dominando.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Departamento> Departamentos { get; set; }
    public DbSet<Funcionario> Funcionarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connString = Configuration.Configuration.GetConfiguration()["ConnectionStrings:Dev_IO_EfCore_Dominando"];

        optionsBuilder.UseSqlServer(connString)
            .EnableSensitiveDataLogging()
            // .UseLazyLoadingProxies()
            .LogTo(Console.WriteLine, LogLevel.Information);
        
        base.OnConfiguring(optionsBuilder);
    }
}