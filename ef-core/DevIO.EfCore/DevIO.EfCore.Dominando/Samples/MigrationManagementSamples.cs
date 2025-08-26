using DevIO.EfCore.Dominando.Data;
using Microsoft.EntityFrameworkCore;

namespace DevIO.EfCore.Dominando.Samples;

public static class MigrationManagementSamples
{
    public static void MigracoesPendentes()
    {
        using var dbContext = new ApplicationDbContext();
        var migracoesPendentes =  dbContext.Database.GetPendingMigrations().ToList();
    
        Console.WriteLine($"Total: {migracoesPendentes.Count}");

        foreach (var migracao in migracoesPendentes)
        {
            Console.WriteLine(migracao);
        }
    }

    public static void AplicandoMigracaoEmTempoDeExecucao()
    {
        using var dbContext = new ApplicationDbContext();
        dbContext.Database.Migrate();
    }

    public static void TodasMigracoes()
    {
        using var dbContext = new ApplicationDbContext();
        var migracoes = dbContext.Database.GetMigrations().ToList();
    
        Console.WriteLine($"Total: {migracoes}");

        foreach (var migracao in migracoes)
        {
            Console.WriteLine($"Migração: {migracao}");
        }
    }

    public static void MigracoesJaAplicadas()
    {
        using var dbContext = new ApplicationDbContext();
        var migracoes = dbContext.Database.GetAppliedMigrations().ToList();
    
        Console.WriteLine($"Total: {migracoes.Count}");

        foreach (var migracao in migracoes)
        {
            Console.WriteLine($"Migração: {migracao}");
        }
    }
}