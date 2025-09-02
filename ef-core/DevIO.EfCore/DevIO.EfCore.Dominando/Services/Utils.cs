using DevIO.EfCore.Dominando.Data;
using DevIO.EfCore.Dominando.Domain;

namespace DevIO.EfCore.Dominando.Services;

public static class Utils
{
    public static void SetupData(ApplicationDbContext db)
    {
        
        if (!db.Database.EnsureCreated())
            return;
        
        db.Departamentos.AddRange(
            new Departamento
            {
                Descricao = "Departamento 01",
                Excluido = true,
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