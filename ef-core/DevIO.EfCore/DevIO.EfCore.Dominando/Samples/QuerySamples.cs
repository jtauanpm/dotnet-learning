using System.Data;
using DevIO.EfCore.Dominando.Data;
using DevIO.EfCore.Dominando.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DevIO.EfCore.Dominando.Samples;

public class QuerySamples
{
    public static void DivisaoConsultas()
    {
        using var dbContext = new ApplicationDbContext();
        Utils.SetupData(dbContext);
        
        var departamentos = dbContext.Departamentos
            .Include(d => d.Funcionarios)
            .Where(d =>  d.Id < 3)
            .AsSplitQuery()
            .AsSingleQuery()
            .ToList();

        foreach (var departamento in departamentos)
        {
            Console.WriteLine(departamento);

            foreach (var funcionario in departamento.Funcionarios)
            {
                Console.WriteLine("Funcionário:" + funcionario);
            }
        }
    }
    
    public static void ConsultaComTAG()
    {
        using var dbContext = new ApplicationDbContext();
        Utils.SetupData(dbContext);
        
        var departamentos = dbContext.Departamentos
            .TagWith("Estou enviando um comentário para o servidor")
            .ToList();

        foreach (var departamento in departamentos)
        {
            Console.WriteLine(departamento);
        }
    }
    
    public static void ConsultaInterpolada()
    {
        using var dbContext = new ApplicationDbContext();
        Utils.SetupData(dbContext);
        
        var id = 1;
        var departamentos = dbContext.Departamentos
            .FromSqlInterpolated($"SELECT * FROM Departamentos WITH (NOLOCK) WHERE Id > {id};")
            .ToList();

        foreach (var departamento in departamentos)
        {
            Console.WriteLine(departamento);
        }
    }
    
    public static void ConsultaParametrizada()
    {
        using var dbContext = new ApplicationDbContext();
        Utils.SetupData(dbContext);

        // var id = 0;
        var id = new SqlParameter
        {
            Value = 1,
            SqlDbType = SqlDbType.Int
        };
        var departamentos = dbContext.Departamentos
            .FromSqlRaw("SELECT * FROM Departamentos WITH (NOLOCK) WHERE Id > {0};", id)
            .ToList();

        foreach (var departamento in departamentos)
        {
            Console.WriteLine(departamento);
        }
    }

    public static void ConsultaProjetada()
    {
        using var dbContext = new ApplicationDbContext();
        Utils.SetupData(dbContext);
    
        var departamentos = dbContext.Departamentos
            .Where(d => d.Id > 0)
            .Select(d => new { d.Descricao, Funcionarios = d.Funcionarios.Select(f => f.Nome)})
            .ToList();

        foreach (var departamento in departamentos)
        {
            Console.WriteLine(departamento);
        
            foreach (var funcionario in departamento.Funcionarios)
            {
                Console.WriteLine(funcionario);
            }
        }
    }

    public static void IgnoreFiltroGlobal()
    {
        using var dbContext = new ApplicationDbContext();
        Utils.SetupData(dbContext);
    
        var departamentos = dbContext.Departamentos.IgnoreQueryFilters().Where(d => d.Id > 0).ToList();

        foreach (var departamento in departamentos)
        {
            Console.WriteLine(departamento);
        }
    }

    public static void FiltroGlobal()
    {
        using var dbContext = new ApplicationDbContext();
        Utils.SetupData(dbContext);
    
        var departamentos = dbContext.Departamentos.Where(d => d.Id > 0).ToList();

        foreach (var departamento in departamentos)
        {
            Console.WriteLine(departamento);
        }
    }
}