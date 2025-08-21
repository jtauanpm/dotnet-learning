using DevIO.EfCore.Dominando.Configuration;
using DevIO.EfCore.Dominando.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DevIO.EfCore.Dominando.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Departamento> Departamentos { get; set; }
    public DbSet<Departamento> Funcionarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connString = Settings.Configuration["ConnectionStrings:Dev_IO_EfCore_Dominando"];

        optionsBuilder.UseSqlServer(connString)
            .EnableSensitiveDataLogging()
            .LogTo(Console.WriteLine, LogLevel.Warning);
        
        base.OnConfiguring(optionsBuilder);
    }
}