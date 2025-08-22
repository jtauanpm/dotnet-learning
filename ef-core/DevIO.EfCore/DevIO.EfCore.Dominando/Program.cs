// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using DevIO.EfCore.Dominando.Data;
using DevIO.EfCore.Dominando.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

// EnsureCreatedAndDeleted();
// GapDoEnsureCreated();
// HealthCheckBancoDeDados();
// GerenciarEstadoDaConexao(false);
// GerenciarEstadoDaConexao(true);
// SqlInjection();

// MigracoesPendentes();
// AplicandoMigracaoEmTempoDeExecucao();
// MigracoesPendentes();
// TodasMigracoes();
MigracoesJaAplicadas();
return;

static void EnsureCreatedAndDeleted()
{
    using var db = new ApplicationDbContext();
    db.Database.EnsureCreated();

    db.Database.EnsureDeleted();
}

static void GapDoEnsureCreated()
{
    using var dbContext = new ApplicationDbContext();
    using var dbCidades = new ApplicationDbContextCidade();
    
    dbContext.Database.EnsureCreated();
    dbCidades.Database.EnsureCreated();

    var databaseCreate = dbCidades.GetService<IRelationalDatabaseCreator>();
    databaseCreate.CreateTables();
}

static void HealthCheckBancoDeDados()
{
    using var dbContext = new ApplicationDbContext();
    
    var canConnect = dbContext.Database.CanConnect();
    Console.WriteLine($"Can connect to database: {canConnect}");
}

static void GerenciarEstadoDaConexao(bool gerenciarEstadoConexao)
{
    // warmup
    new ApplicationDbContext().Departamentos.AsNoTracking().Any();

    var count = 0;
    using var dbContext = new ApplicationDbContext();
    var time = Stopwatch.StartNew();
    
    var conexao = dbContext.Database.GetDbConnection();
    
    conexao.StateChange += (_, _) => ++count;
    
    if (gerenciarEstadoConexao)
    {
        conexao.Open();
    }

    for (var i = 0; i < 200; i++)
    {
        dbContext.Departamentos.AsNoTracking().Any();
    }
    
    if (gerenciarEstadoConexao)
    {
        conexao.Close();
    }
    
    time.Stop();
    var mensagem = $"Tempo: {time.Elapsed}, Estado gerenciado: {gerenciarEstadoConexao}, Contador: {count}";
    
    Console.WriteLine(mensagem);
}

static void ExecuteSql()
{
    using var dbContext = new ApplicationDbContext();
    
    // 1st option
    using var cmd = dbContext.Database.GetDbConnection().CreateCommand();
    cmd.CommandText = "SELECT 1";
    cmd.ExecuteNonQuery();
    
    // 2nd option
    var descricao = "TESTE";
    dbContext.Database.ExecuteSqlRaw("UPDATE Departamentos SET Descricao = {0} WHERE Id = 1", descricao);
    
    // 3rd option
    dbContext.Database.ExecuteSqlInterpolated($"UPDATE Departamentos SET Descricao = {descricao} WHERE Id = 1");
}

static void SqlInjection()
{
    using var dbContext = new ApplicationDbContext();
    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
    
    dbContext.Departamentos.AddRange(
        new Departamento
        {
            Descricao = "Departamento 01"
        },
        new Departamento
        {
            Descricao = "Departamento 02"
        });
    dbContext.SaveChanges();
    
    // SQL Injection
    var descricao = "Teste' or 1= '1";
    dbContext.Database.ExecuteSqlRaw($"UPDATE Departamentos SET Descricao = 'AtaqueSqlInjection' WHERE Descricao = '{descricao}'");
    
    foreach (var departamento in dbContext.Departamentos.AsNoTracking())
    {
        Console.WriteLine($"Id: {departamento.Id}, Descricao: {departamento.Descricao}");
    }
}

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
