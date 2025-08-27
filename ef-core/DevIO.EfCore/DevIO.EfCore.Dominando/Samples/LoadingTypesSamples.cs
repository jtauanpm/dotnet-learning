using DevIO.EfCore.Dominando.Data;
using DevIO.EfCore.Dominando.Domain;
using Microsoft.EntityFrameworkCore;

namespace DevIO.EfCore.Dominando.Samples;

public class LoadingTypesSamples
{
    public static void CarregamentoAdiantado()
    {
        using var db = new ApplicationDbContext();
        SetupTiposCarregamentos(db);

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

    static void SetupTiposCarregamentos(ApplicationDbContext db)
    {
        if (db.Departamentos.Any()) 
            return;
    
        db.Departamentos.AddRange(
            new Departamento
            {
                Descricao = "Departamento 01",
                Funcionarios =
                [
                    new()
                    {
                        Nome = "Rafael Almeida",
                        CPF = "99999999911",
                        RG = "2100062"
                    }
                ]
            },
            new Departamento
            {
                Descricao = "Departamento 02",
                Funcionarios =
                [
                    new()
                    {
                        Nome = "Bruno Brito",
                        CPF = "88888888811",
                        RG = "3100062"
                    },

                    new()
                    {
                        Nome = "Eduardo Pires",
                        CPF = "77777777711",
                        RG = "1100062"
                    }
                ]
            });

        db.SaveChanges();
        db.ChangeTracker.Clear();
    }
}