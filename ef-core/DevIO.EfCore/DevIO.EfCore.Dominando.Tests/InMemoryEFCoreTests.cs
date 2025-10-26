using DevIO.EfCore.Dominando.Data;
using DevIO.EfCore.Dominando.Domain;
using DevIO.EfCore.Dominando.Tests.EFCore;
using Microsoft.EntityFrameworkCore;

namespace DevIO.EfCore.Dominando.Tests;

public class InMemoryEfCoreTests
{
    private TestsDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("InMemoryDatabase")
            .Options;

        return new TestsDbContext(options);
    }

    [Fact]
    public void DeveInserirEDepartamentoComSucesso()
    {
        // ARRANGE
        using var context = GetDbContext();

        var expectedDepartamento = "TI";
        var novoDepartamento = new Departamento
        {
            Descricao = expectedDepartamento
        };

        // ACT
        context.Departamentos.Add(novoDepartamento);
        var linhasAfetadas = context.SaveChanges();

        // ASSERT
        Assert.Equal(1, linhasAfetadas);
        
        var departamentoSalvo = context.Departamentos
            .FirstOrDefault(d => d.Descricao == expectedDepartamento);
        Assert.NotNull(departamentoSalvo);
        Assert.Equal(expectedDepartamento, departamentoSalvo.Descricao);
    }
}