using System.Diagnostics;
using System.Transactions;
using DevIO.EfCore.Dominando.Data;
using DevIO.EfCore.Dominando.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

UsingBuiltInFunction();
return;

static void UsingBuiltInFunction()
{
    using var db = new ApplicationDbContext();
    
    var result = db.Departamentos.Select(d => ApplicationDbContext.Left(d.Descricao, 4)).First();
    Console.WriteLine(result);
}

static void UsingCustomTransactions()
{
    EnsureDeletedAndCreate();
    
    using var db = new ApplicationDbContext();
    
    var strategy = db.Database.CreateExecutionStrategy();

    strategy.Execute(() =>
    {
        using var transaction = db.Database.BeginTransaction();

        try
        {
            // tudo que precisa estar no retry + transação
            db.Departamentos.Add(new Departamento { Descricao = "desc", Ativo = true });
            db.SaveChanges();
            
            transaction.CreateSavepoint("save_point");

            var dep = db.Departamentos.First();
            dep.Descricao = "desc updated";
            db.SaveChanges();
        
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.RollbackToSavepoint("save_point");
        }
    });

    var transactionOptions = new TransactionOptions
    {
        IsolationLevel = IsolationLevel.ReadCommitted
    };
    
    using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
    {
        scope.Complete();
    }
}

static void SimpleRead()
{
    EnsureDeletedAndCreate();
    using var db = new ApplicationDbContext();
    var departamentos = db.Departamentos.ToList();
}

static void ReadShadowProperties()
{
    using var db = new ApplicationDbContext();
    var departamentos = db.Departamentos
        .Where(d => EF.Property<DateTime>(d, "UltimaAtualizacao") < DateTime.Now)
        .ToArray();
}

static void InsertShadowProperties()
{
    using var db = new ApplicationDbContext();
    EnsureDeletedAndCreate();

    var dep = new Departamento
    {
        Descricao = "Description"
    };

    db.Departamentos.Add(dep);
    db.Entry(dep).Property("UltimaAtualizacao").CurrentValue = DateTime.Now;
    
    db.SaveChanges();
}

static void InsertFuncionario()
{
    using var db = new ApplicationDbContext();

    db.Departamentos.Add(new Departamento
    {
        Descricao = "desc",
        Ativo = true
    });
    
    db.Funcionarios.Add(new Funcionario
    {
        Nome = "Funcionario",
        DepartamentoId = 1,
        CPF = "",
        RG = "",
        ContractType = ContractType.CLT,
        Gender = Gender.Male
    });
    db.SaveChanges();

    db.Funcionarios.ToList().ForEach(Console.WriteLine);
}

static void PrintarCreateDbScript()
{
    using var dbContext = new ApplicationDbContext();
    var script = dbContext.Database.GenerateCreateScript();
    Console.WriteLine(script);
}

static void ExecutarEstrategiaResiliencia()
{
    using var db = new ApplicationDbContext();

    var strategy = db.Database.CreateExecutionStrategy();
    strategy.Execute(() =>
    {
        using var transaction = db.Database.BeginTransaction();

        db.Departamentos.Add(new Departamento { Descricao = "Departamento Transacao" });
        db.SaveChanges();

        transaction.Commit();
    });
}

static void TempoComandoGeral()
{
    using var db = new ApplicationDbContext();
    db.Database.SetCommandTimeout(10);
    db.Database.ExecuteSqlRaw("WAITFOR DELAY '00:00:07';SELECT 1");
}

static void BatchOperation()
{
    using var db = new ApplicationDbContext();

    for (int i = 0; i < 50; i++)
    {
        db.Departamentos.Add(new Departamento
        {
            Descricao = $"Departamento {i}",
            Ativo = true,
            Excluido = false
        });
    }

    db.SaveChanges();
}

static void ConsultarDepartamentos()
{
    using var db = new ApplicationDbContext();
    var departamentos = db.Departamentos.Where(d => d.Id > 0).ToArray();
}

static void EnsureCreatedAndDeleted()
{
    using var db = new ApplicationDbContext();
    db.Database.EnsureCreated();

    db.Database.EnsureDeleted();
}

static void EnsureDeletedAndCreate()
{
    using var db = new ApplicationDbContext();
    db.Database.EnsureDeleted();
    
    db.Database.EnsureCreated();
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
        Console.WriteLine(departamento);
    }
}

static void ScriptGeralBancoDeDados()
{
    using var dbContext = new ApplicationDbContext();
    var script = dbContext.Database.GenerateCreateScript();
    Console.WriteLine(script);
}
