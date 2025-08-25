using DevIO.EfCore.Dominando.Data;
using Microsoft.EntityFrameworkCore;

namespace DevIO.EfCore.Dominando.Samples;

public static class MigrationManagementSamples
{
    static void MigracoesPendentes()
    {
        using var dbContext = new ApplicationDbContext();
        var migracoesPendentes =  dbContext.Database.GetPendingMigrations().ToList();
    
        Console.WriteLine($"Total: {migracoesPendentes.Count}");

        foreach (var migracao in migracoesPendentes)
        {
            Console.WriteLine(migracao);
        }
    }

    static void AplicandoMigracaoEmTempoDeExecucao()
    {
        using var dbContext = new ApplicationDbContext();
        dbContext.Database.Migrate();
    }

    static void TodasMigracoes()
    {
        using var dbContext = new ApplicationDbContext();
        var migracoes = dbContext.Database.GetMigrations().ToList();
    
        Console.WriteLine($"Total: {migracoes}");

        foreach (var migracao in migracoes)
        {
            Console.WriteLine($"Migração: {migracao}");
        }
    }

    static void MigracoesJaAplicadas()
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