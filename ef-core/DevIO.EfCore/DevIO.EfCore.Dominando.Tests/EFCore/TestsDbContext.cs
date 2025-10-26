using DevIO.EfCore.Dominando.Data;
using Microsoft.EntityFrameworkCore;

namespace DevIO.EfCore.Dominando.Tests.EFCore;

public class TestsDbContext : ApplicationDbContext
{
    public TestsDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
}