// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using DevIO.EfCore.Dominando.Configuration;
using DevIO.EfCore.Dominando.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

Settings.Configuration = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

// EnsureCreatedAndDeleted();
// GapDoEnsureCreated();
// HealthCheckBancoDeDados();
GerenciarEstadoDaConexao(false);
GerenciarEstadoDaConexao(true);
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
