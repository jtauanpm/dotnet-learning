using DevIO.EfCore.Dominando.Data;
using DevIO.EfCore.Dominando.Domain;
using DevIO.EfCore.Dominando.Services;
using Microsoft.EntityFrameworkCore;

namespace DevIO.EfCore.Dominando.Samples;

public class LoadingTypesSamples
{
    public static void CarregamentoLento()
    {
        using var db = new ApplicationDbContext();
        Utils.SetupData(db);
        
        // db.ChangeTracker.LazyLoadingEnabled = false; // Desabilita LazyLoading iperativamente

        var departamentos = db
            .Departamentos;

        foreach (var departamento in departamentos)
        {
            Console.WriteLine("---------------------------------------");
            Console.WriteLine($"Departamento: {departamento.Descricao}");

            if (departamento.Funcionarios?.Any() ?? false)
            {
                foreach (var funcionario in departamento.Funcionarios)
                {
                    Console.WriteLine($"\tFuncionario: {funcionario.Nome}");
                }
            }
            else
            {
                Console.WriteLine($"\tNenhum funcionario encontrado!");
            }
        }
    }
    
    public static void CarregamentoExplicito()
    {
        using var db = new ApplicationDbContext();
        Utils.SetupData(db);

        var departamentos = db
            .Departamentos;

        foreach (var departamento in departamentos)
        {
            if (departamento.Id == 2)
            {
                // db.Entry(departamento).Collection(d => d.Funcionarios).Load();
                db.Entry(departamento).Collection(d => d.Funcionarios).Query().Where(p => p.Id > 2).Load();
            }
            
            Console.WriteLine("---------------------------------------");
            Console.WriteLine($"Departamento: {departamento.Descricao}");

            if (departamento.Funcionarios?.Any() ?? false)
            {
                foreach (var funcionario in departamento.Funcionarios)
                {
                    Console.WriteLine($"\tFuncionario: {funcionario.Nome}");
                }
            }
            else
            {
                Console.WriteLine($"\tNenhum funcionario encontrado!");
            }
        }
    }
    
    public static void CarregamentoAdiantado()
    {
        using var db = new ApplicationDbContext();
        Utils.SetupData(db);

        var departamentos = db
            .Departamentos
            .Include(p => p.Funcionarios);

        foreach (var departamento in departamentos)
        {
            Console.WriteLine("---------------------------------------");
            Console.WriteLine($"Departamento: {departamento.Descricao}");

            if (departamento.Funcionarios?.Any() ?? false)
            {
                foreach (var funcionario in departamento.Funcionarios)
                {
                    Console.WriteLine($"\tFuncionario: {funcionario.Nome}");
                }
            }
            else
            {
                Console.WriteLine($"\tNenhum funcionario encontrado!");
            }
        }
    }
}