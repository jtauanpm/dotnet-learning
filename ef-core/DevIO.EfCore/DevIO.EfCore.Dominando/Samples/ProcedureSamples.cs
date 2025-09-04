using DevIO.EfCore.Dominando.Data;
using Microsoft.EntityFrameworkCore;

namespace DevIO.EfCore.Dominando.Samples;

public static class ProcedureSamples
{
    public static void InserirDadosProcedure()
    {
        using var dbContext = new ApplicationDbContext();
        dbContext.Database.ExecuteSqlRaw("execute CriarDepartamento @p0, @p1", "Departamento Via Procedure", true);
    }
    
    public static void CriarStoredProcedure()
    {
        var criarDepartamento = """

                                        CREATE OR ALTER PROCEDURE CriarDepartamento
                                            @Descricao VARCHAR(100),
                                            @Ativo bit
                                        AS
                                        BEGIN
                                            INSERT INTO
                                               Departamentos(Descricao, Ativo, Excluido)
                                            VALUES (@Descricao, @Ativo, 0)
                                        END
                                """;
        
        using var dbContext = new ApplicationDbContext();
        dbContext.Database.ExecuteSqlRaw(criarDepartamento);
    }
}