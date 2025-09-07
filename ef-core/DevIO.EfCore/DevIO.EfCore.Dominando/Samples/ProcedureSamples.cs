using DevIO.EfCore.Dominando.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DevIO.EfCore.Dominando.Samples;

public static class ProcedureSamples
{
    public static void ConsultaViaProcedure()
    {
        using var dbContext = new ApplicationDbContext();
        
        var depFilter = new SqlParameter("@dep", "Departamento");
        var departamentos = dbContext.Departamentos
            .FromSqlRaw("EXECUTE GetDepartamentos @dep", depFilter)
            .ToList();
        
        foreach (var departamento in departamentos)
        {
            Console.WriteLine(departamento);
        }
    }
    
    public static void CriarStoredProcedureConsulta()
    {
        var consultarDepartamentos = """

                                        CREATE OR ALTER PROCEDURE GetDepartamentos
                                            @Descricao VARCHAR(100)
                                        AS
                                        BEGIN
                                            SELECT * FROM Departamentos WHERE Descricao LIKE @Descricao + '%'   
                                        END
                                """;
        
        using var dbContext = new ApplicationDbContext();
        dbContext.Database.ExecuteSqlRaw(consultarDepartamentos);
    }
    
    public static void InserirDadosProcedure()
    {
        using var dbContext = new ApplicationDbContext();
        dbContext.Database.ExecuteSqlRaw("EXECUTE CriarDepartamento @p0, @p1", "Departamento Via Procedure", true);
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