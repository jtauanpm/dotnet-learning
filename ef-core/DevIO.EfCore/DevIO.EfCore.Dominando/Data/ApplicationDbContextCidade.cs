using DevIO.EfCore.Dominando.Configurations;
using DevIO.EfCore.Dominando.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DevIO.EfCore.Dominando.Data;

public class ApplicationDbContextCidade : DbContext
{
    public DbSet<Cidade> Cidades { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connString = Configuration.GetConfiguration()["ConnectionStrings:Dev_IO_EfCore_Dominando"];

        optionsBuilder.UseSqlServer(connString)
            .EnableSensitiveDataLogging()
            .LogTo(Console.WriteLine, LogLevel.Information);
        
        base.OnConfiguring(optionsBuilder);
    }
}